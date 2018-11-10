using System;
using System.Buffers;
using System.IO;
using System.Text;

namespace GoodPractices.Benchmark.Lib.ReadFile
{
  public class FastReader : IDisposable
  {
    private bool disposed;
    private Stream stream;
    private bool leaveOpen;
    private StringBuilder sb;
    private ArrayPool<char> charPool;
    private ArrayPool<byte> bytePool;
    private char[] rentedCharBuffer;
    private byte[] byteBuff;
    private char[] charBuff;
    private byte[] preamble;
    private int buffIndex = 0;
    private int charsRead = 0;
    private Encoding enc = Encoding.UTF8;
    private bool checkBOM;


    /// <summary>
    /// It initialized IonReader
    /// </summary>
    /// <param name="stream">Stream to read from</param>
    /// <param name="currentLineVerifier">Object which verifies state based on current line</param>
    /// <param name="sectionHeaderReader">Object which reads sections</param>
    /// <param name="leaveOpen">True if the stream should be open after Dispose()</param>
    /// <param name="charPool">Provide own System.Buffers.ArrayPool<char> instance. If null then System.Buffers.ArrayPool<char>.Shared will be used</param>
    public FastReader(Stream stream, bool leaveOpen, ArrayPool<char> charPool = null, ArrayPool<byte> bytePool = null)
    {
      this.stream = stream;
      this.disposed = false;
      this.CurrentLineNumber = 0;
      this.leaveOpen = leaveOpen;
      this.sb = new StringBuilder();
      this.charPool = charPool ?? System.Buffers.ArrayPool<char>.Shared;
      this.bytePool = bytePool ?? System.Buffers.ArrayPool<byte>.Shared;
      this.rentedCharBuffer = null;
      this.byteBuff = this.bytePool.Rent(1024);
      this.charBuff = this.charPool.Rent(enc.GetMaxByteCount(byteBuff.Length));
      this.preamble = enc.GetPreamble();
      this.checkBOM = this.stream.CanSeek ? this.stream.Position == 0 : true;
    }

    /// <summary>
    /// Gets the current line value. It allocates string from CurrentRawLine.
    /// </summary>
    /// <value>The current line.</value>
    public string CurrentLine
    {
      get
      {
        return new String(CurrentRawLine.Array, CurrentRawLine.Offset, CurrentRawLine.Count);
      }
    }

    public ArraySegment<char> CurrentRawLine { get; private set; }

    public int CurrentLineNumber { get; private set; }

    public bool Read()
    {
      if (!this.stream.CanRead)
      {
        ResetFields();
        return false;
      }
      try
      {
        if (!ReadLine() && sb.Length == 0)
        {
          ResetFields();
          return false;
        }
        if (sb.Length == 0)
        {
          CurrentRawLine = ArraySegment<char>.Empty;
        }
        else
        {
          PrepareBuffer(sb.Length);
          sb.CopyTo(0, rentedCharBuffer, 0, sb.Length);
          CurrentRawLine = new ArraySegment<char>(rentedCharBuffer, 0, sb.Length);
        }
        CurrentLineNumber++;
      }
      catch (Exception exception)
      {
        throw new Exception("File could not be parsed", exception);
      }
      return true;
    }

    private bool ReadLine()
    {
      bool endOfFile = false;
      this.sb.Clear();
      Array.Clear(byteBuff, 0, byteBuff.Length);
      if (buffIndex > 0)
      {
        if (!CopyTillEOL())
        {
          Array.Clear(charBuff, 0, charBuff.Length);
          buffIndex = 0;
        }
        else
        {
          return !endOfFile;
        }
      }
      while (this.stream.CanRead && endOfFile == false)
      {
        int bytesRead = this.stream.Read(byteBuff, 0, byteBuff.Length);
        if (bytesRead == 0)
        {
          endOfFile = true;
          break;
        }
        var offset = GetIndex();
        this.charsRead = enc.GetChars(byteBuff, offset, bytesRead - offset, charBuff, 0);
        if (CopyTillEOL())
        {
          return !endOfFile;
        }
      }
      return !endOfFile;
    }

    private int GetIndex()
    {
      var retval = 0;
      if (!this.checkBOM)
      {
        retval = 0;
      }
      else
      {
        if (this.byteBuff[0] == this.preamble[0] &&
        this.byteBuff[1] == this.preamble[1] &&
        this.byteBuff[2] == this.preamble[2])
        {
          retval = this.preamble.Length;
        }
      }
      this.checkBOM = false;
      return retval;
    }

    private bool CopyTillEOL()
    {
      var tmp = buffIndex;
      for (int i = buffIndex; i < this.charsRead; i++)
      {
        var character = charBuff[i];
        if (character == '\n')
        {
          var x = 0;
          if (i > buffIndex)
          {
            x = charBuff[i - 1] == '\r' ? 1 : 0;
            this.sb.Append(charBuff, buffIndex - x, i - buffIndex - x);
          }
          buffIndex = i + 1;
          return true;
        }
      }
      this.sb.Append(charBuff, tmp, this.charsRead - tmp);
      return false;
    }

    private void PrepareBuffer(int length)
    {
      if (this.rentedCharBuffer == null)
      {
        this.rentedCharBuffer = charPool.Rent(length);
      }
      else if (this.rentedCharBuffer.Length >= length)
      {
        Array.Clear(this.rentedCharBuffer, 0, length);
      }
      else
      {
        charPool.Return(this.rentedCharBuffer, true);
        this.rentedCharBuffer = charPool.Rent(length);
      }
    }

    #region IDisposable members

    public void Dispose()
    {
      Dispose(true);
    }

    public void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (this.rentedCharBuffer != null)
        {
          charPool.Return(this.rentedCharBuffer, true);
          this.rentedCharBuffer = null;
        }
        if (this.charBuff != null)
        {
          charPool.Return(this.charBuff, true);
          this.charBuff = null;
        }
        if (this.byteBuff != null)
        {
          bytePool.Return(this.byteBuff, true);
          this.byteBuff = null;
        }

        if (!this.leaveOpen && this.stream != null)
        {
          this.stream.Dispose();
          this.stream = null;
        }

        disposed = true;
      }
    }

    #endregion

    #region Private methods

    public void ResetFields()
    {
      CurrentRawLine = default(ArraySegment<char>);
    }

    #endregion
  }
}
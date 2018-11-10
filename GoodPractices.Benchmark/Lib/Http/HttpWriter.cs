using System;
using System.Buffers;
using System.IO;
using System.Net;
using System.Net.Http;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class HttpWriter
  {
    private TextWriter writer;

    public HttpWriter(TextWriter tw)
    {
      this.writer = tw;
      this.writer.NewLine = "\r\n";
    }

    public void WriteRequestLine(HttpMethod verb, Version version, string path)
    {
      this.writer.Write(verb.Method);
      if(path.StartsWith('/'))
      {
        this.writer.Write(' ');
      }
      else
      {
        this.writer.Write(" /");
      }
      this.writer.Write(path);
      if (version == HttpVersion.Version10)
      {
        this.writer.WriteLine(" HTTP/1.0");
      }
      else if (version == HttpVersion.Version11)
      {
        this.writer.WriteLine(" HTTP/1.1");
      }
      else if (version == HttpVersion.Version20)
      {
        this.writer.WriteLine(" HTTP/2.0");
      }
    }

    public void WriteHost(string value)
    {
      WriteHeader("Host", value);
    }

    public void WriteContentLenght(string value)
    {
      WriteHeader("Content-Length", value);
    }

    public void WriteContentType(string value)
    {
      WriteHeader("Content-Type", value);
    }

    public void WriteUserAgent(string value)
    {
      WriteHeader("User-Agent", value);
    }

    public void WriteAccept(string value)
    {
      WriteHeader("Accept", value);
    }

    public void WriteContentEncoding(string value)
    {
      WriteHeader("Content-Encoding", value);
    }

    public void WriteConnectionKeepAlive()
    {
      WriteHeader("Connection", "keep-alive");
    }

    public void WriteConnectionClose()
    {
      WriteHeader("Connection", "close");
    }

    public void WriteHeader(string headerName, string headerValue)
    {
      writer.Write(headerName);
      writer.Write(":");
      writer.WriteLine(headerValue);
    }

    public void WriteHeader(string headerName, int headerValue)
    {
      writer.Write(headerName);
      writer.Write(":");
      writer.WriteLine(headerValue);
    }

    public void WriteHeader(string headerName, long headerValue)
    {
      writer.Write(headerName);
      writer.Write(":");
      writer.WriteLine(headerValue);
    }

    public void WriteBody(string body)
    {
      writer.WriteLine(body);
    }

    public void WriteBody(Action<TextWriter> action)
    {
      action(this.writer);
    }

    public void WriteEoH()
    {
      writer.WriteLine();
    }

    public void Flush()
    {
      this.writer.Flush();
    }
  }
}
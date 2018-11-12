using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Files
{
  public class ReadFileTest
  {
    [Benchmark]
    public int Use_Only_StreamReader()
    {
      int retval = 0;
      using(var reader = File.OpenText(FileLoader.GetStationsIonPath()))
      {
        while(!reader.EndOfStream)
        {
          var line = reader.ReadLine();
          if(line.Contains('x'))
          {
            retval++;
          }
        }
      }
      return retval;
    }

    [Benchmark]
    public int Use_MemoryMappedFile_And_Stream_Reader()
    {
      int retval = 0;
      using (var mmf = MemoryMappedFile.CreateFromFile(FileLoader.GetStationsIonPath()))
      using (var accessor = mmf.CreateViewStream())
      using (var reader = new StreamReader(accessor))
      {
        while (!reader.EndOfStream)
        {
          var line = reader.ReadLine();
          if (line.Contains('x'))
          {
            retval++;
          }
        }
      }
      return retval;
    }

    [Benchmark]
    public int Use_Custom_FastReader()
    {
      int retval = 0;
      using (var stream = File.OpenRead(FileLoader.GetStationsIonPath()))
      using (var reader = new Anixe.IO.FastStreamReader(stream))
      {
        while (reader.Read())
        {
          var line = reader.CurrentRawLine.AsSpan();
          if (line.IndexOf('x') != -1)
          {
            retval++;
          }
        }
      }
      return retval;
    }
  }
}
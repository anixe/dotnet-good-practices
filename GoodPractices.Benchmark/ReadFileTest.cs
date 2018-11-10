using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using BenchmarkDotNet.Attributes;
using GoodPractices.Benchmark.Lib.ReadFile;

namespace GoodPractices.Benchmark
{
  public class ReadFileTest
  {
    [GlobalSetup]
    public void Setup()
    {
    }


    [GlobalCleanup]
    public void Cleanup()
    {
    }

    // [Benchmark]
    // public int Use_Only_Stream()
    // {
    //   int retval = 0;
    //   using (var reader = File.OpenRead(FileLoader.GetStationsIonPath()))
    //   {
    //     if (!reader.CanRead)
    //     {
    //       var line = reader.ReadLine();
    //       if (line.Contains('x'))
    //       {
    //         retval++;
    //       }
    //     }
    //   }
    //   return retval;
    // }


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
      using (var reader = new FastReader(stream, true))
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
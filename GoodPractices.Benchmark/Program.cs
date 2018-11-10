using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using GoodPractices.Benchmark.Lib.Http;

namespace GoodPractices.Benchmark
{
  public class Config : ManualConfig
  {
    public Config()
    {
      Add(DefaultConfig.Instance);
      Add(MemoryDiagnoser.Default);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      // var summary = BenchmarkRunner.Run<StringConcatTest>(new Config { });
      // var summary = BenchmarkRunner.Run<StringBuilderAppendTest>(new Config { });
      // var summary = BenchmarkRunner.Run<PooledStringBuilderAppendTest>(new Config { });
      // var summary = BenchmarkRunner.Run<PooledStringBuilderCopyToTest>(new Config { });
      // var summary = BenchmarkRunner.Run<ReadFileTest>(new Config { });
      // var summary = BenchmarkRunner.Run<RegexVsStringMatchingTest>(new Config { });
      // var summary1 = BenchmarkRunner.Run<HttpTests>(new Config { });
      // var summary2 = BenchmarkRunner.Run<HttpPipelineTests>(new Config { });
      // var summary3 = BenchmarkRunner.Run<HttpBufferedPipelineTests>(new Config { });
      var summary4 = BenchmarkRunner.Run<ReturnYieldVsReturnList>(new Config { });
    }
  }
}
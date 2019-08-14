using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.AsyncIO
{
  public class AsyncTests
  {
    private static HttpClient cli;
    const int length = 5;
    [ThreadStatic]
    private static byte[] buff = new byte[1024];

    [GlobalSetup]
    public void Setup()
    {
      Cleanup();
      cli = new HttpClient(new HttpClientHandler
      {
        MaxConnectionsPerServer = 1000,
        AutomaticDecompression = System.Net.DecompressionMethods.None
      });
    }

    private async Task<long> ReadAsync(Stream stream)
    {
      long bytes = 0;
      var lenght = 0;
      while ((lenght = await stream.ReadAsync(buff)) > 0)
      {
        bytes += lenght;
      }
      return bytes;
    }


    [GlobalCleanup]
    public void Cleanup()
    {
      if (cli != null)
      {
        cli.Dispose();
      }
      cli = null;
    }

    [Benchmark]
    public async Task<long> TestAsync()
    {
      long bytes = 0;
      for (int i = 0; i < length; i++)
      {
        using (var stream = await cli.GetStreamAsync("http://localhost:8080/medium").ConfigureAwait(false))
        {
          bytes += await ReadAsync(stream);
        }
      }
      return bytes;
    }


  }
}
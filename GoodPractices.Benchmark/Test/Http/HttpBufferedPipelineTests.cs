using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using GoodPractices.Benchmark.Lib.Http;
using Microsoft.IO;

namespace GoodPractices.Benchmark.Test.Http
{
  public class HttpBufferedPipelineTests
  {
    private static HttpClient cli;
    const int length = 1;
    private readonly static RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager();


    [GlobalSetup]
    public void Setup()
    {
      Cleanup();
      HttpMessageHandler h = new HttpClientHandler
      {
        MaxConnectionsPerServer = 1000,
        AutomaticDecompression = System.Net.DecompressionMethods.None
      };
      cli = HttpClientFactory.Create(h, new BufferingHandler(manager), new DecompressionHandler());
    }


    [GlobalCleanup]
    public void Cleanup()
    {
      if(cli != null)
      {
        cli.Dispose();
      }
      cli = null;
    }

    private long Read(Stream stream)
    {
      long bytes = 0;
      Span<byte> tmp = stackalloc byte[1024];
      var lenght = 0;
      while ((lenght = stream.Read(tmp)) > 0)
      {
        bytes += lenght;
      }
      return bytes;
    }

    [Benchmark]
    public async Task<long> Shared_HttpClient_ReadAsStreamAsync_ResponseHeadersRead()
    {
      long bytes = 0;
      for (int i = 0; i < length; i++)
      {
        using (var response = cli.GetAsync("http://localhost:8080/medium", HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult())
        {
          using (var stream = await response.Content.ReadAsStreamAsync())
          {
            bytes += Read(stream);
            stream.Position = 0;
            bytes += Read(stream);
          }
          if (response.RequestMessage.Properties.TryGetValue("buffer", out var ms))
          {
            ((MemoryStream)ms).Dispose();
          }
        }
      }
      return bytes;
    }
  }
}
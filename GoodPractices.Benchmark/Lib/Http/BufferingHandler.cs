using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IO;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class BufferingHandler : DelegatingHandler
  {
    private RecyclableMemoryStreamManager manager;
    public BufferingHandler(RecyclableMemoryStreamManager manager)
    {
      this.manager = manager;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var response = await base.SendAsync(request, cancellationToken);
      response.Content = await PostProcessAsync(response);
      return response;
    }

    public async ValueTask<StreamContent> PostProcessAsync(HttpResponseMessage response)
    {
      var str = this.manager.GetStream();
      var str2 = this.manager.GetStream();
      if (response.Content != null)
      {
        var content = response.Content;
        using(var s = await content.ReadAsStreamAsync())
        {
          await s.CopyToAsync(str);
          str.Position = 0;
          str.CopyTo(str2);
          str2.Position = 0;
        }
      }
      response.RequestMessage.Properties.Add("buffer", str2);
      return new StreamContent(str, 1024);
    }
  }
}
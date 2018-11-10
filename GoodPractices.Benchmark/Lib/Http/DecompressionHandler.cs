using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class DecompressionHandler : DelegatingHandler
  {
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var response = await base.SendAsync(request, cancellationToken);

      await PostProcessAsync(response);

      return response;
    }

    public async Task PostProcessAsync(HttpResponseMessage response)
    {
      if (response.Content != null)
      {
        var responseContent = response.Content;

        if (responseContent.Headers.ContentEncoding.Remove("gzip"))
        {
          response.Content = await Unzip(responseContent);
        }
        else if (responseContent.Headers.ContentEncoding.Remove("deflate"))
        {
          response.Content = await Deflate(responseContent);
        }
      }
    }

    private async ValueTask<StreamContent> Unzip(HttpContent content)
    {
      var s = await content.ReadAsStreamAsync();
      if (s.CanSeek)
      {
        s.Seek(0, SeekOrigin.Begin);
      }
      var newContent = new StreamContent(new GZipStream(s, CompressionMode.Decompress));
      return newContent;
    }

    private async ValueTask<StreamContent> Deflate(HttpContent content)
    {
      var s = await content.ReadAsStreamAsync();
      if (s.CanSeek)
      {
        s.Seek(0, SeekOrigin.Begin);
      }
      var newContent = new StreamContent(new DeflateStream(s, CompressionMode.Decompress));
      return newContent;
    }
  }
}
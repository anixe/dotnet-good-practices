using System;
using System.Net.Http;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class HttpClientFactory
  {
    public static HttpClient Create(params DelegatingHandler[] handlers)
    {
      return Create(new HttpClientHandler(), handlers);
    }

    public static HttpClient Create(HttpMessageHandler innerHandler, params DelegatingHandler[] handlers)
    {
      HttpMessageHandler pipeline = CreatePipeline(innerHandler, handlers);
      return new HttpClient(pipeline);
    }

    public static HttpMessageHandler CreatePipeline(HttpMessageHandler innerHandler, params DelegatingHandler[] handlers)
    {
      if (innerHandler == null)
      {
        throw new ArgumentNullException(nameof(innerHandler));
      }

      if (handlers == null)
      {
        return innerHandler;
      }

      HttpMessageHandler pipeline = innerHandler;
      for (int i = handlers.Length - 1; i >= 0; i--)
      {
        var handler = handlers[i];
        if (handler == null)
        {
          throw new ArgumentNullException(nameof(DelegatingHandler));
        }

        if (handler.InnerHandler != null)
        {
          throw new ArgumentNullException(nameof(handler.InnerHandler), nameof(DelegatingHandler));
        }

        handler.InnerHandler = pipeline;
        pipeline = handler;
      }

      return pipeline;
    }
  }
}
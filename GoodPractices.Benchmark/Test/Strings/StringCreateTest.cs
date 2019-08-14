using System;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Strings
{
  public class StringCreateTest
  {
    private string type = "transfer_reqion";
    private string tenant = "test";
    private string prefix = "env.acceptance.tenant";
    private int maxLength;

    public StringCreateTest()
    {
      maxLength = prefix.Length + tenant.Length + ".type.".Length + type.Length;
    }

    [Benchmark]
    public string Contact()
    {
      return string.Concat(prefix, tenant, ".type.", type);
    }

    [Benchmark]
    public string Create()
    {
      return string.Create(maxLength, (prefix, tenant, type), CreteKeyStr);
    }

    private static void CreteKeyStr(Span<char> span, (string prefix, string tenant, string type) state)
    {
      (string prefix, string tenant, string type) = state;
      var position = CopyTo(prefix, span);
      position += CopyTo(tenant, span.Slice(position));
      position += CopyTo(".type.", span.Slice(position));
      position += CopyTo(type, span.Slice(position));
    }

    private static int CopyTo(ReadOnlySpan<char> source, Span<char> dest)
    {
      source.CopyTo(dest);
      return source.Length;
    }
  }
}
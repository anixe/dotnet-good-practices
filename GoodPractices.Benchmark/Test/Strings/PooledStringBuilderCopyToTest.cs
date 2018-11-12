

using System;
using System.Buffers;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Strings
{
  public class PooledStringBuilderCopyToTest
  {
    private const string str1 = "fdhfahakdfjaldfjhafldj";
    private const string str2 = "afhafjaflkdhjsfhla";
    private const string str3 = "dasdsdasdasd";
    private const string str4 = "afafdfsafdfd";
    private const string str5 = "afafdafdffd";
    private const string str6 = "adfadffaa";

    private StringBuilder sb;
    private char[] target;

    [GlobalSetup]
    public void Setup()
    {
      sb = Consts.StringBuilderPool.Get();
      target = ArrayPool<char>.Shared.Rent(1024);
    }


    [GlobalCleanup]
    public void Cleanup()
    {
      Consts.StringBuilderPool.Return(this.sb);
      ArrayPool<char>.Shared.Return(this.target);
    }

    [Benchmark]
    public char[] Append_2_Strings()
    {
      sb.Clear();
      sb.Append(str1).Append(str2);
      sb.CopyTo(0, this.target, 0, sb.Length);
      return target;
    }

    [Benchmark]
    public char[] Append_2_Objects()
    {
      sb.Clear();
      sb.Append(str1).Append(33534234.33);
      sb.CopyTo(0, this.target, 0, sb.Length);
      return target;
    }

    [Benchmark]
    public char[] Append_6_Strings()
    {
      sb.Clear();
      sb.Append(str1).Append(str2).Append(str3).Append(str4).Append(str5).Append(str6);
      sb.CopyTo(0, this.target, 0, sb.Length);
      return target;
    }

    [Benchmark]
    public char[] Append_6_Objects()
    {
      sb.Clear();
      sb.Append(str1).Append(33.3333).Append(str3).Append('c').Append(str5).Append(str6);
      sb.CopyTo(0, this.target, 0, sb.Length);
      return target;
    }


    [Benchmark]
    public char[] Append_6_Objects_Converted_To_String()
    {
      sb.Clear();
      sb.Append(str1).Append(33.3333.ToString()).Append(str3).Append('c'.ToString()).Append(str5).Append(str6);
      sb.CopyTo(0, this.target, 0, sb.Length);
      return target;
    }
  }
}
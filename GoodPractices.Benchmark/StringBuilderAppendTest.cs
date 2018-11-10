using System.Text;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark
{
  public class StringBuilderAppendTest
  {
    private const string str1 = "fdhfahakdfjaldfjhafldj";
    private const string str2 = "afhafjaflkdhjsfhla";
    private const string str3 = "dasdsdasdasd";
    private const string str4 = "afafdfsafdfd";
    private const string str5 = "afafdafdffd";
    private const string str6 = "adfadffaa";

    [Benchmark]
    public string Append_2_Strings()
    {
      var sb = new StringBuilder();
      sb.Append(str1).Append(str2);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_2_Strings_With_Initialize()
    {
      var sb = new StringBuilder(64);
      sb.Append(str1).Append(str2);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_2_Objects()
    {
      var sb = new StringBuilder();
      sb.Append(str1).Append(33534234.33);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_2_Objects_With_Initialize()
    {
      var sb = new StringBuilder(64);
      sb.Append(str1).Append(33534234.33);
      return sb.ToString();
    }


    [Benchmark]
    public string Append_5_Strings()
    {
      var sb = new StringBuilder();
      sb.Append(str1).Append(str2).Append(str3).Append(str4).Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_5_Strings_With_Initialie()
    {
      var sb = new StringBuilder(128);
      sb.Append(str1).Append(str2).Append(str3).Append(str4).Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_6_Objects()
    {
      var sb = new StringBuilder();
      sb.Append(str1).Append(33.3333).Append(str3).Append('c').Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_6_Objects_With_Initialize()
    {
      var sb = new StringBuilder(128);
      sb.Append(str1).Append(33.3333).Append(str3).Append('c').Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_6_Objects_Converted_To_String()
    {
      var sb = new StringBuilder();
      sb.Append(str1).Append(33.3333.ToString()).Append(str3).Append('c'.ToString()).Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string Append_6_Objects_Converted_To_String_With_Initialize()
    {
      var sb = new StringBuilder(128);
      sb.Append(str1).Append(33.3333.ToString()).Append(str3).Append('c'.ToString()).Append(str5).Append(str6);
      return sb.ToString();
    }

    [Benchmark]
    public string AppendFormat_6_Objects_Converted_To_String_With_Initialize()
    {
      var sb = new StringBuilder(128);
      sb.AppendFormat("{0}{1}{2}{3}{4}{5}", str1, 33.3333, str3, 'c', str5, str6);
      return sb.ToString();
    }

  }
}
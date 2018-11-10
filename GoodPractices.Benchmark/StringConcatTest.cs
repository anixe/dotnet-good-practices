using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark
{
  public class StringConcatTest
  {
    [Benchmark]
    public string Contact_2_Strings()
    {
      return string.Concat("fdhfahakdfjaldfjhafldj", "afhafjaflkdhjsfhla");
    }

    [Benchmark]
    public string Contact_2_Objects()
    {
      return string.Concat("fdhfahakdfjaldfjhafldj", 33534234.33);
    }

    [Benchmark]
    public string Contact_5_Strings()
    {
      return string.Concat("fdhfahakdfjaldfjhafldj", "afhafjaflkdhjsfhla", "dasdsdasdasd", "afafdfsafdfd", "afafdafdffd", "adfadffaa");
    }

    [Benchmark]
    public string Contact_6_Objects()
    {
      return string.Concat("fdhfahakdfjaldfjhafldj", 33.3333, "dasdsdasdasd", 'c', "afafdafdffd", "adfadffaa");
    }


    [Benchmark]
    public string Contact_6_Objects_Converted_To_String()
    {
      return string.Concat("fdhfahakdfjaldfjhafldj", 33.3333.ToString(), "dasdsdasdasd", 'c'.ToString(), "afafdafdffd", "adfadffaa");
    }

    [Benchmark]
    public string Contact_3_3_Objects_Converted_To_String()
    {
      return string.Concat(string.Concat("fdhfahakdfjaldfjhafldj", 33.3333.ToString(), "dasdsdasdasd"), 'c'.ToString(), "afafdafdffd", "adfadffaa");
    }
  }
}
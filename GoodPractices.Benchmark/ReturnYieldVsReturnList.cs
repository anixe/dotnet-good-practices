using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark
{
  public class ReturnYieldVsReturnList
  {
    [Params(1, 10, 100, 1000)]
    public int NumberOfItems;

    [Benchmark]
    public int Return_List()
    {
      return SumList(CreateList());
    }

    
    [Benchmark]
    public int Return_List_Known_Capacity()
    {
        return SumList(CreateListKnownCapacity());
    }

    [Benchmark]
    public int Yield()
    {
      return SumEnumerable(CreateYield());
    }

    private int SumEnumerable(IEnumerable<int> list)
    {
      int sum = 0;
      foreach (var item in list)
      {
        sum += item;
      }
      return sum;
    }

    private int SumList(List<int> list)
    {
      int sum = 0;
      foreach (var item in list)
      {
        sum += item;
      }
      return sum;
    }
    private List<int> CreateList()
    {
      var list = new List<int>();
      for (int i = 0; i < this.NumberOfItems; i++)
      {
        list.Add(i);
      }
      return list;
    }

    private List<int> CreateListKnownCapacity()
    {
      var list = new List<int>(this.NumberOfItems);
      for (int i = 0; i < this.NumberOfItems; i++)
      {
        list.Add(i);
      }
      return list;
    }

    public IEnumerable<int> CreateYield()
    {
      for (int i = 0; i < this.NumberOfItems; i++)
      {
        yield return i;
      }
    }
  }
}
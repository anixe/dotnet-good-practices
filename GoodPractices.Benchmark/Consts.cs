using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace GoodPractices.Benchmark
{
  public class Consts
  {
    public readonly static ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy { InitialCapacity = 1024 });
  }
}
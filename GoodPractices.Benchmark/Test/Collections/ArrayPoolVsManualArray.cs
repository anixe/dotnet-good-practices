using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Collections
{
    public class ArrayPoolVsManualArray
    {
        private readonly int _arraySize = 250;

        [Benchmark]
        public void CreateArrayPerExecution()
        {
            var array = new int[_arraySize];

            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = i;
            }
        }

        [Benchmark]
        public void RentArrayPerExecution_ForgetReturn()
        {
            var array = ArrayPool<int>.Shared.Rent(_arraySize);
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = i;
            }
        }

        [Benchmark]
        public void RentArrayPerExecution_WithReturn()
        {
            var array = ArrayPool<int>.Shared.Rent(_arraySize);
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = i;
            }

            ArrayPool<int>.Shared.Return(array);
        }
    }
}
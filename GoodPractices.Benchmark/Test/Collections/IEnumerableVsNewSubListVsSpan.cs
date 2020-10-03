using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Collections
{
    public class IEnumerableVsNewSubListVsSpan
    {
        private readonly int _arraySize = 100;
        private readonly int _calculatedItems = 25;
        private readonly int _number = 75;

        [Benchmark]
        public void ProcessWithNewSubList()
        {
            var array = ArrayPool<int>.Shared.Rent(_arraySize);
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _number;
            }

            var calculationItems = array.Take(_calculatedItems).ToList();

            Assert(_number * _calculatedItems, CalculateFromIEnumerable(calculationItems));

            ArrayPool<int>.Shared.Return(array);
        }

        [Benchmark]
        public void ProcessWithIEnumerable()
        {
            var array = ArrayPool<int>.Shared.Rent(_arraySize);
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _number;
            }

            var calculationItems = array.Take(_calculatedItems);

            Assert(_number * _calculatedItems, CalculateFromIEnumerable(calculationItems));

            ArrayPool<int>.Shared.Return(array);
        }

        [Benchmark]
        public void ProcessWithSpan()
        {
            var array = ArrayPool<int>.Shared.Rent(_arraySize);
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _number;
            }

            var calculationItems = array.AsSpan(0, _calculatedItems);

            Assert(_number * _calculatedItems, CalculateFromSpan(calculationItems));

            ArrayPool<int>.Shared.Return(array);
        }

        private int CalculateFromIEnumerable(IEnumerable<int> numbers)
        {
            var result = 0;

            foreach (var number in numbers)
            {
                result += number;
            }

            return result;
        }

        private int CalculateFromSpan(Span<int> numbers)
        {
            var result = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                result += numbers[i];
            }

            return result;
        }

        private void Assert(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new ArgumentException($"Expected: {expected} but got: {actual}");
            }
        }
    }
}
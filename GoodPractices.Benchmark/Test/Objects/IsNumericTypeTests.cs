using System;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Objects
{
    public class IsNumericTypeTests
    {
        [Benchmark]
        public bool IsNumericType_UsingGetTypeCode()
        {
            var res1 = IsNumericType1("5");
            var res2 = IsNumericType1(5);

            return res1 && res2;
        }
        
        [Benchmark]
        public bool IsNumericType_UsingTypeMatching()
        {
            var res1 = IsNumericType2("5");
            var res2 = IsNumericType2(5);

            return res1 && res2;
        }

        private static bool IsNumericType1(object o) => Type.GetTypeCode(o.GetType()) switch
        {
            TypeCode.Byte => true,
            TypeCode.SByte => true,
            TypeCode.UInt16 => true,
            TypeCode.UInt32 => true,
            TypeCode.UInt64 => true,
            TypeCode.Int16 => true,
            TypeCode.Int32 => true,
            TypeCode.Int64 => true,
            TypeCode.Decimal => true,
            TypeCode.Double => true,
            TypeCode.Single => true,
            _ => false,
        };
        
        private static bool IsNumericType2(object o) => o switch
        {
            byte => true,
            sbyte => true,
            ushort => true,
            uint => true,
            ulong => true,
            short => true,
            int => true,
            long => true,
            decimal => true,
            double => true,
            float => true,
            _ => false,
        };
    }
}
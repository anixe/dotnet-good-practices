using System;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace GoodPractices.Benchmark.Test.Strings
{
    public class StackallocVsStringBuilderVsDateFormat
    {
        private readonly DateTime _time = new DateTime(2010, 12, 31, 12, 15, 30);
        private StringBuilder _pooledStringBuilder;
        private const string _expectedFormattedTime = "2010-12-31T12:15:30";

        [Benchmark]
        public void DateFormat()
        {
            Assert(_expectedFormattedTime, _time.ToString("yyyy-MM-ddThh:mm:ss"));
        }

        [GlobalSetup]
        public void Setup()
        {
            _pooledStringBuilder = Consts.StringBuilderPool.Get();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
          Consts.StringBuilderPool.Return(_pooledStringBuilder);
        }

        [Benchmark]
        public void PooledStringBuilder()
        {
            _pooledStringBuilder.Clear();
            _pooledStringBuilder
                .Append(_time.Year)
                .Append("-")
                .Append(_time.Month)
                .Append("-")
                .Append(_time.Day)
                .Append("T")
                .Append(_time.Hour)
                .Append(":")
                .Append(_time.Minute)
                .Append(":")
                .Append(_time.Second);

            Assert(_expectedFormattedTime, _pooledStringBuilder.ToString());
        }

        [Benchmark]
        public void NonPooledStringBuilder()
        {
            var nonPooledStringBuilder = new StringBuilder();
            nonPooledStringBuilder
                .Append(_time.Year)
                .Append("-")
                .Append(_time.Month)
                .Append("-")
                .Append(_time.Day)
                .Append("T")
                .Append(_time.Hour)
                .Append(":")
                .Append(_time.Minute)
                .Append(":")
                .Append(_time.Second);

            Assert(_expectedFormattedTime, nonPooledStringBuilder.ToString());
        }

        [Benchmark]
        public void Stackalloc()
        {
            int hour = _time.Hour;
            int minute = _time.Minute;
            int second = _time.Second;
            int year = _time.Year;
            int month = _time.Month;
            int day = _time.Day;

            char hour1 = (char)('0' + (hour / 10));
            char hour2 = (char)('0' + (hour % 10));
            char minute1 = (char)('0' + (minute / 10));
            char minute2 = (char)('0' + (minute % 10));
            char seconds1 = (char)('0' + (second / 10));
            char seconds2 = (char)('0' + (second % 10));
            char month1 = (char)('0' + (month / 10));
            char month2 = (char)('0' + (month % 10));
            char day1 = (char)('0' + (day / 10));
            char day2 = (char)('0' + (day % 10));
            char year1 = (char)('0' + (year / 1000));
            char year2 = (char)('0' + (year / 100 % 10));
            char year3 = (char)('0' + (year / 10 % 10));
            char year4 = (char)('0' + (year % 10));

            Span<char> chars = stackalloc char[]
            {
                year1,
                year2,
                year3,
                year4, '-',
                month1,
                month2,
                '-',
                day1,
                day2,
                'T',
                hour1,
                hour2,
                ':',
                minute1,
                minute2,
                ':',
                seconds1,
                seconds2
            };

            Assert(_expectedFormattedTime, new string(chars));
        }

        private void Assert(string expected, string actual)
        {
            if (!expected.Equals(actual))
            {
                throw new ArgumentException($"Expected: {expected} but got: {actual}");
            }
        }
    }
}
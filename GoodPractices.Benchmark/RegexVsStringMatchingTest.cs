using BenchmarkDotNet.Attributes;
using GoodPractices.Benchmark.Lib.Match;
using System.Text.RegularExpressions;

namespace GoodPractices.Benchmark
{
    public class RegexVsStringMatchingTest
    {
        private const string pattern1 = "^test(\\.)apitester$";
        private const string pattern2 = "^test(\\.).*$";
        private const string pattern3 = "^test(\\.)api(\\.)tester$";
        private const string pattern4 = "^test(\\.)api.*$";
        private const string pattern5 = "^test(\\.).*(\\.)tester$";
        private const string pattern6 = "^TEST(\\.)APITESTER$";

        private const string contract = "test.apitester";
        private const string contract2 = "test.api.tester";

        private ContractMatcher matcher;
        private Regex compiledRegex;

        [GlobalSetup(Targets = new[] 
        {
            nameof(Matcher_No_Storage_Pattern_Same_As_Input),            
            nameof(Matcher_No_Storage_Pattern_With_Wildcard),            
            nameof(Matcher_No_Storage_Pattern_Has_More_Elements_Than_Input),
            nameof(Matcher_No_Storage_Pattern_With_Wildcard_At_The_End),
            nameof(Matcher_No_Storage_Pattern_With_Wildcard_In_The_Middle),
            nameof(Matcher_No_Storage_Pattern_Uppercase),
            nameof(Matcher_Storage_Pattern_Same_As_Input),
            nameof(Matcher_Storage_Pattern_With_Wildcard),
            nameof(Matcher_Storage_Pattern_Has_More_Elements_Than_Input),
            nameof(Matcher_Storage_Pattern_With_Wildcard_At_The_End),
            nameof(Matcher_Storage_Pattern_With_Wildcard_In_The_Middle),
            nameof(Matcher_Storage_Pattern_Uppercase)
        })]
        public void Matcher_Setup()
        {
            this.matcher = new ContractMatcher();
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_Same_As_Input))]
        public void Compiled_Pattern1_Setup()
        {
            this.compiledRegex = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_With_Wildcard))]
        public void Compiled_PAttern2_Setup()
        {
            this.compiledRegex = new Regex(pattern2, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_Has_More_Elements_Than_Input))]
        public void Compiled_Pattern3_Setup()
        {
            this.compiledRegex = new Regex(pattern3, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_With_Wildcard_At_The_End))]
        public void Compiled_Pattern4_Setup()
        {
            this.compiledRegex = new Regex(pattern4, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_With_Wildcard_In_The_Middle))]
        public void Compiled_Pattern5_Setup()
        {
            this.compiledRegex = new Regex(pattern5, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [GlobalSetup(Target = nameof(CompiledRegex_Pattern_Uppercase))]
        public void Compiled_Pattern6_Setup()
        {
            this.compiledRegex = new Regex(pattern6, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [Benchmark]
        public bool Regex_Pattern_Same_As_Input()
        {
            return Regex.IsMatch(contract, pattern1, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_Same_As_Input()
        {
            return this.compiledRegex.IsMatch(contract);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_Same_As_Input()
        {
            return this.matcher.IsMatch(pattern1, contract, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_Same_As_Input()
        {
            return this.matcher.IsMatch(pattern1, contract);
        }

        [Benchmark]
        public bool Regex_Pattern_With_Wildcard()
        {
            return Regex.IsMatch(pattern2, contract, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_With_Wildcard()
        {
            return this.compiledRegex.IsMatch(contract);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_With_Wildcard()
        {
            return this.matcher.IsMatch(pattern2, contract, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_With_Wildcard()
        {
            return this.matcher.IsMatch(pattern2, contract);
        }

        [Benchmark]
        public bool Regex_Pattern_Has_More_Elements_Than_Input()
        {
            return Regex.IsMatch(pattern3, contract, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_Has_More_Elements_Than_Input()
        {
            return this.compiledRegex.IsMatch(contract);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_Has_More_Elements_Than_Input()
        {
            return this.matcher.IsMatch(pattern3, contract, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_Has_More_Elements_Than_Input()
        {
            return this.matcher.IsMatch(pattern3, contract);
        }

        [Benchmark]
        public bool Regex_Pattern_With_Wildcard_At_The_End()
        {
            return Regex.IsMatch(pattern4, contract, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_With_Wildcard_At_The_End()
        {
            return this.compiledRegex.IsMatch(contract);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_With_Wildcard_At_The_End()
        {
            return this.matcher.IsMatch(pattern4, contract, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_With_Wildcard_At_The_End()
        {
            return this.matcher.IsMatch(pattern4, contract);
        }

        [Benchmark]
        public bool Regex_Pattern_With_Wildcard_In_The_Middle()
        {
            return Regex.IsMatch(pattern5, contract2, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_With_Wildcard_In_The_Middle()
        {
            return this.compiledRegex.IsMatch(contract2);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_With_Wildcard_In_The_Middle()
        {
            return this.matcher.IsMatch(pattern5, contract2, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_With_Wildcard_In_The_Middle()
        {
            return this.matcher.IsMatch(pattern5, contract2);
        }

        [Benchmark]
        public bool Regex_Pattern_Uppercase()
        {
            return Regex.IsMatch(pattern6, contract, RegexOptions.IgnoreCase);
        }

        [Benchmark]
        public bool CompiledRegex_Pattern_Uppercase()
        {
            return this.compiledRegex.IsMatch(contract);
        }

        [Benchmark]
        public bool Matcher_No_Storage_Pattern_Uppercase()
        {
            return this.matcher.IsMatch(pattern6, contract, false);
        }

        [Benchmark]
        public bool Matcher_Storage_Pattern_Uppercase()
        {
            return this.matcher.IsMatch(pattern6, contract);
        }
    }
}

using System;
using System.Collections.Generic;

namespace GoodPractices.Benchmark.Lib.Match
{
    internal class ContractMatcher
    {
        #region Private fields       

        private const char PatternStartCharacter = '^';
        private const char PatternEndCharacter = '$';
        private const char WildcardChar = '*';
        private const string Wildcard = ".*";

        private readonly static string[] patternElementsSeparator = new string[] { "(\\.)" };
        private readonly static char[] contractElementsSeparator = new char[] { '.' };
        private readonly static Dictionary<string, bool> alreadyProcessedResults = new Dictionary<string, bool>();

        #endregion

        #region Public methods

        public bool IsMatch(string contractPattern, string contractToMatch, bool useStorage = true)
        {
            if (useStorage && this.TryGetProcessedResult(contractPattern, contractToMatch, out bool result))
            {
                return result;
            }

            var pattern = this.TrimPattern(contractPattern);
            var splittedPattern = this.SplitPattern(pattern);
            var splittedContract = this.SplitContract(contractToMatch);

            if (splittedPattern.Length > splittedContract.Length)
            {
                result = false;
            }
            else if (splittedContract.Length == splittedPattern.Length)
            {
                result = this.AreMatching(splittedPattern, splittedContract);
            }
            else
            {
                result = this.AreMatching(splittedPattern, splittedContract)
                    && splittedPattern[splittedPattern.Length - 1].Equals(Wildcard);
            }

            if (useStorage)
            {
                this.SaveResult(contractPattern, contractToMatch, result);
            }

            return result;
        }

        #endregion

        #region Private methods

        private bool TryGetProcessedResult(string pattern, string contract, out bool result)
        {
            if (alreadyProcessedResults.TryGetValue($"{pattern}-{contract}", out result))
            {
                return true;
            }

            return false;
        }

        private void SaveResult(string pattern, string contract, bool result)
        {
            alreadyProcessedResults.TryAdd($"{pattern}-{contract}", result);
        }

        private string TrimPattern(string contractPattern)
        {
            return contractPattern.Trim(PatternStartCharacter, PatternEndCharacter);
        }

        private string[] SplitContract(string contractToSplit)
        {
            return contractToSplit.Split(contractElementsSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitPattern(string patternToSplit)
        {
            return patternToSplit.Split(patternElementsSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool AreMatching(string[] splittedPattern, string[] splittedContract)
        {
            for (var i = 0; i < splittedPattern.Length; i++)
            {
                if (!this.AreMatching(splittedPattern[i], splittedContract[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreMatching(string patternElement, string contractElement)
        {
            return patternElement.Equals(Wildcard)
                || patternElement.Equals(contractElement, StringComparison.OrdinalIgnoreCase)
                || (patternElement.EndsWith(Wildcard)
                    && contractElement.StartsWith(patternElement.TrimEnd(WildcardChar, '.')));
        }

        #endregion

    }
}

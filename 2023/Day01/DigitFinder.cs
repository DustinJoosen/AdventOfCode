using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day01
{
    public record FirstLastDigit(int first, int last);

    public class DigitFinder(string sentence)
    {
        private Dictionary<string, int> _digitIntMap = new()
        {
            {"zero", 0 },
            {"one", 1 },
            {"two", 2 },
            {"three", 3 },
            {"four", 4 },
            {"five", 5 },
            {"six", 6 },
            {"seven", 7 },
            {"eight", 8 },
            {"nine", 9 },
        };

        public FirstLastDigit FindFirstLastDigits()
        {
            this.ReplaceSpelledWithInts();

            // Regex that finds all digits.
            string pattern = @"\d";

            var regex = new Regex(pattern);
            var matches = regex.Matches(sentence);

            int first = this.ParseDigit(matches.First().Value);
            int last = this.ParseDigit(matches.Last().Value);

            return new FirstLastDigit(first, last);
        }

        public void ReplaceSpelledWithInts()
        {
            // make a buffer sentence to write to.
            string buffer = "" + sentence;

            foreach (KeyValuePair<string, int> kvp in this._digitIntMap)
            {
                if (!sentence.Contains(kvp.Key))
                {
                    continue;
                }

                int idx = sentence.IndexOf(kvp.Key);
                sentence = buffer.Insert(idx + 1, kvp.Value.ToString());

                this.ReplaceSpelledWithInts();
                return;
            }

        }

        public int ParseDigit(string digit)
        {
            if (digit.Count() == 1 && char.IsDigit(digit, 0))
            {
                return int.Parse(digit);
            }

            if (this._digitIntMap.ContainsKey(digit))
            {
                return this._digitIntMap[digit];
            }

            return -1;
        }
    }
}

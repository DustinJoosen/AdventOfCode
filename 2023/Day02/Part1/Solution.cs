using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day02.Part1
{
    [Display(Name = "Day 02 A")]
    public class Solution : BaseSolution
    {
        Dictionary<string, int> maxAllowedValues = new()
        {
            { "red", 12},
            { "green", 13},
            { "blue", 14},
        };

        public override void Run()
        {
            string pattern = @"^Game (\d+): (?:(\d+ \w+).? *)+$";
            Regex regex = new Regex(pattern);

            int total = 0;

            foreach (var line in Lines)
            {
                Match match = regex.Match(line);

                if (!match.Success)
                {
                    throw new Exception("Regex failed.");
                }

                int id = int.Parse(match.Groups[1].Value);

                // Loop through all key-value pair (2 red)
                bool isValid = true;
                foreach (Capture combo in match.Groups[2].Captures)
                {
                    string[] kv = combo.Value.Split(" ");

                    // If the max of the color is less then the new value, let the user know.
                    if (maxAllowedValues[kv[1]] < int.Parse(kv[0]))
                    {
                        isValid = false;
                        break;
                    }
                }

                // Not in the foreach loop to ensure it's not accidentally done twice.
                if (isValid)
                {
                    total += id;
                }
            }

            Console.WriteLine(total);
        }
    }
}

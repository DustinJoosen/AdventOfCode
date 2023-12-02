using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day02.Part2
{
    [Display(Name = "Day 02 B")]
    public class Solution : BaseSolution
    {
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

                // Reset minimal needed values to 0.
                Dictionary<string, int> minUsedValues = new()
                {
                    { "red", 0},
                    { "green", 0},
                    { "blue", 0},
                };

                foreach (Capture combo in match.Groups[2].Captures)
                {
                    string[] kv = combo.Value.Split(" ");

                    // If the number is bigger, replace it.
                    if (minUsedValues[kv[1]] < int.Parse(kv[0]))
                    {
                        minUsedValues[kv[1]] = int.Parse(kv[0]);
                    }
                }

                // Calculate power
                int red = minUsedValues["red"];
                int green = minUsedValues["green"];
                int blue = minUsedValues["blue"];

                total += red * green * blue;
            }

            Console.WriteLine(total);

        }
    }
}

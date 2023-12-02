using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day02
{
    [Display(Name = "Day 02")]
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
            bool part1 = false;

            if (part1)
            {
                this.RunPart1();
            } 
            else
            {
                this.RunPart2();
            }

        }

        public void RunPart2()
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

        public void RunPart1()
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

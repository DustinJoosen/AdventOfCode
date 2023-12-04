using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day04.Part1
{
    [Display (Name = "Day 04 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            Regex regex = new Regex(@"^Card\s+(\d+):([\d\s|]+)$");

            int total = 0;
            foreach (string line in Lines)
            {
                Match match = regex.Match(line);

                string[] numbers = match.Groups[2].Value.Split("|");
                string[] winning = numbers[0].Split(" ").Where(num => num != "").ToArray();
                string[] bought = numbers[1].Split(" ").Where(num => num != "").ToArray();

                int count = 0;
                foreach (string num in bought)
                {
                    if (winning.Contains(num))
                        count++;
                }

                total += this.GetPoints(count);
            }
            Console.WriteLine(total);
        }

        private int GetPoints(int count)
        {
            if (count == 0)
                return 0;

            return (int)Math.Pow(2, count - 1);
        }
    }
}

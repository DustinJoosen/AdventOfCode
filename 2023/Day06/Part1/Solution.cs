using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day06.Part1
{
    [Display (Name = "Day 06 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            var pairs = this.GetPairs();
            List<int> values = [];

            foreach (var pair in pairs)
            {
                int total_combinations = 0;
                for (int i = 1; i <= pair.Item1; i++)
                {
                    int dist = this.GetDistanceTraveled(i, pair.Item1);
                    if (dist > pair.Item2)
                        total_combinations++;
                }
                Console.WriteLine(total_combinations);
                values.Add(total_combinations);
            }

            Console.WriteLine(values.Aggregate((acc, x) => acc * x));
        }

        private int GetDistanceTraveled(int pressed_timing, int time) =>
            (time - pressed_timing) * pressed_timing;

        private List<(int, int)> GetPairs()
        {
            List<int> times = Lines[0]
                .Replace("Time: ", string.Empty)
                .Split(" ")
                .Where(t => t != "")
                .Select(t => int.Parse(t))
                .ToList();

            List<int> distances = Lines[1]
                .Replace("Distance: ", string.Empty)
                .Split(" ")
                .Where(t => t != "")
                .Select(t => int.Parse(t))
                .ToList();

            List<(int, int)> pairs = [];
            for (int i = 0; i < times.Count; i++)
            {
                pairs.Add((times[i], distances[i]));
            }

            return pairs;

        }
    }
}

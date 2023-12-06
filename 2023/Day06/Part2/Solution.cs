using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day06.Part2
{
    [Display (Name = "Day 06 B")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            (long, long) pair = this.GetPairs();
            List<int> values = [];
            int total_combinations = 0;
            
            for (long i = 1; i <= pair.Item1; i++)
            {
                long dist = this.GetDistanceTraveled(i, pair.Item1);
                if (dist > pair.Item2)
                    total_combinations++;
            }

            Console.WriteLine(total_combinations);
            values.Add(total_combinations);

            Console.WriteLine(values.Aggregate((acc, x) => acc * x));
        }

        private long GetDistanceTraveled(long pressed_timing, long time) =>
            (time - pressed_timing) * pressed_timing;

        private (long, long) GetPairs()
        {
            long times = long.Parse(Lines[0]
                .Replace("Time: ", string.Empty)
                .Replace(" ", string.Empty)
                .ToString());

            long distances = long.Parse(Lines[1]                
                .Replace("Distance: ", string.Empty)
                .Replace(" ", string.Empty)
                .ToString());

            return (times, distances);
        }
    }
}

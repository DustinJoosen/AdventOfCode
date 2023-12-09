using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023.Day05.Part2
{
    record MapLine (long destinationRangeStart, long sourceRangeStart, long rangeLength);

    [Display(Name = "Day 05 B")]
    public class Solution : BaseSolution
    {
        private Dictionary<string, List<MapLine>> _maps;

        public override void Run()
        {
            // Warning: this solution takes ~2 hours.
            // The solution, with the given input, is 63179500.
            Console.WriteLine("Warning: this solution takes ~2Ø hours.");
            Console.WriteLine("The solution, with the given input, is 63179500");

            this._maps = this.GetMapData();

            bool done = false;
            int i = 0;

            while (!done)
            {
                long seed = LocationToSeed(i++);
                if (this.IsSeed(seed))
                    done = true;

                if (i % 10000 == 0)
                    Console.WriteLine(i);
            }

            Console.WriteLine(i - 1);
        }

        private long LocationToSeed(long location)
        {
            var map_keys_reversed = this._maps.Keys.Reverse();
            long value = location;
            
            foreach (string map_key in map_keys_reversed)
            {
                value = NextSession(value, map_key);
            }

            return value;
        }

        private long NextSession(long value, string map)
        {
            foreach (MapLine ml in _maps[map])
            {
                // If map is inside range.                
                if (ml.destinationRangeStart <= value && ml.destinationRangeStart + ml.rangeLength > value) 
                    return value - (ml.destinationRangeStart - ml.sourceRangeStart);
            }

            return value;
        }

        private Dictionary<string, List<MapLine>> GetMapData()
        {
            Dictionary<string, List<MapLine>> data = new() { };
            List<MapLine> current = [];

            string prevMap = GetMapName(Lines[2]);
            for (int i = 3; i < Lines.Count; i++)
            {
                if (Lines[i] == string.Empty && i != Lines.Count - 1)
                {
                    continue;
                }

                if (Lines[i].Contains("map:") || i == Lines.Count - 1)
                {
                    data.Add(prevMap, current);
                    current = [];

                    prevMap = GetMapName(Lines[i]);
                    continue;
                }

                var nums = ConvertStringOfNumbersToNumberList(Lines[i]);
                current.Add(new(nums[0], nums[1], nums[2]));
            }

            return data;
        }

        private string GetMapName(string line) =>
            line.Replace(" map:", string.Empty);

        private bool IsSeed(long seed)
        {
            var nums = this.ConvertStringOfNumbersToNumberList(Lines[0].Replace("seeds: ", string.Empty));

            for (int i = 0; i < nums.Count; i += 2)
            {
                if (seed >= nums[i] && seed <= nums[i] + nums[i + 1])
                    return true;
            }

            return false;
        }

        private List<long> ConvertStringOfNumbersToNumberList(string str)
        {
            return str
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
        }

    }
}

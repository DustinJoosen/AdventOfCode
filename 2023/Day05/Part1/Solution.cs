using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day05.Part1
{
    [Display (Name = "Day 05 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            var seeds = this.GetSeeds();
            var maps = this.GetMapData();

            List<long> factors = [..seeds];

            foreach (string map in maps.Keys)
            {
                for (int i = 0; i < seeds.Count(); i++)
                {
                    var current = factors[i];
                    factors[i] = GetValue(maps[map], current);
                }
            }

            Console.WriteLine(factors.Min());
        }

        private long GetValue(List<List<long>> map, long seed)
        {
            foreach (var line in map)
            {
                if (this.IsInRange(seed, line[1], line[2]))
                    return line[0] + (seed - line[1]);
            }

            return seed;

        }

        private bool IsInRange(long seed, long start, long count)
        {
            long end = start + count;
            return seed >= start && seed < end;
        }

        private Dictionary<string, List<List<long>>> GetMapData()
        {
            Dictionary<string, List<List<long>>> data = new() { };
            List<List<long>> current = [];

            string prevMap = this.GetMapName(Lines[2]);
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

                    prevMap = this.GetMapName(Lines[i]);
                    continue;
                }

                current.Add(this.ConvertStringOfNumbersToNumberList(Lines[i]));
            }
            
            return data;
        }

        private string GetMapName(string line) =>
            line.Replace(" map:", string.Empty);

        private List<long> GetSeeds()
        {
            Regex regex = new Regex(@"^seeds:((?:\s+\d+)+)");
            Match match = regex.Match(Lines[0]);

            return this.ConvertStringOfNumbersToNumberList(match.Groups[1].Value);
        }

        private List<long> ConvertStringOfNumbersToNumberList(string str)
        {
            string[] numberstr = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            List<long> nums = [];
            foreach (string ns in numberstr)
            {
                nums.Add(long.Parse(ns));
            }
            return nums;
        }
    }
}

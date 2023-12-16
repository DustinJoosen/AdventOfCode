using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day15.Part1
{
    [Display(Name = "Day 15 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            int total = 0;
            foreach (string sequence in Lines.First().Split(","))
            {
                int value = GetHashCode(sequence);
                Console.WriteLine($"{sequence} - {value}");
                total += value;
            }
            Console.WriteLine(total);
        }

        private int GetHashCode(string text)
        {
            int code = 0;
            foreach (char c in text)
            {
                code += (int)c;
                code *= 17;
                code = code % 256;
            }

            return code;
        }
    }
}
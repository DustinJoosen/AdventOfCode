using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023.Day01
{
    [Display (Name = "Day 01")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            int total = 0;

            foreach (var line in Lines)
            {
                DigitFinder finder = new(line);
                FirstLastDigit digits = finder.FindFirstLastDigits();

                total += int.Parse($"{digits.first}{digits.last}");
            }

            Console.WriteLine(total);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day09.Part2
{
    [Display (Name = "Day 09 B")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            int total = 0;
            foreach (string line in Lines)
            {
                List<int> parsed = line.Split(" ").Select(s => int.Parse(s)).ToList();

                // Going down.
                List<List<int>> differences = [[.. parsed]];
                while (!differences.Last().All(d => d == 0))
                {
                    List<int> temp = [];

                    for (int i = 0; i < differences.Last().Count() - 1; i++)
                    {
                        temp.Add(differences.Last()[i + 1] - differences.Last()[i]);
                    }

                    differences.Add(temp);
                }

                // Going back up.
                for (int i = differences.Count() - 1; i >= 0; i--)
                {
                    if (i == differences.Count() - 1)
                    {
                        differences[i].Insert(0, 0);
                        continue;
                    }

                    differences[i].Insert(0, differences[i].First() - differences[i + 1].First());
                }

                // Calculate new.
                int pred = differences.First().First();
                Console.WriteLine(pred);
                total += pred;
            }

            Console.WriteLine(total);
        }
    }
}

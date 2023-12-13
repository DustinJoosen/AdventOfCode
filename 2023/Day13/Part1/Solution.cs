using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day13.Part1
{
    [Display(Name = "Day 13 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            int total = 0;
            var groups = GetGroups();
            foreach (List<string> group in groups)
            {
                int biggest_horizontal_symetry_idx = 0;
                int biggest_vertical_symetry_idx = 0;

                for (int i = 1; i < group.Count() - 1; i++)
                {
                    int horizontal_result = GetHorizontalSymteryLine(group, i);
                    int vertical_result = GetVerticalSymteryLine(group, i);
                    Console.WriteLine("h:" + horizontal_result);
                    Console.WriteLine("v:" + vertical_result);

                    if (horizontal_result != 0)
                    {
                        biggest_horizontal_symetry_idx = Math.Max(i, biggest_horizontal_symetry_idx);
                    }
                    if (vertical_result != 0)
                    {
                        biggest_vertical_symetry_idx = Math.Max(i, biggest_vertical_symetry_idx);
                    }
                }

                if (biggest_horizontal_symetry_idx > biggest_vertical_symetry_idx)
                {
                    total += 100 * (biggest_horizontal_symetry_idx + 1);
                    Console.WriteLine(biggest_horizontal_symetry_idx + 1);
                }
                else
                {
                    total += biggest_vertical_symetry_idx + 1;
                    Console.WriteLine(biggest_vertical_symetry_idx + 1);
                }

                Console.WriteLine("\n========\n");
            }

            Console.WriteLine("Result: " + (total + 1));
        }

        private List<List<string>> GetGroups()
        {
            List<List<string>> groups = [];
            List<string> group = [];

            for (int i = 0; i < Lines.Count(); i++)
            {
                if (Lines[i] == "")
                {
                    groups.Add(group);
                    group = [];
                }
                else
                {
                    group.Add(Lines[i]);
                }
            }
            groups.Add(group);
            return groups;
        }

        private List<string> RotateStringList(List<string> matrix)
        {
            List<List<char>> rotated = [];

            // Add empty nested lists.
            for (int _ = 0; _ < matrix.First().Count(); _++)
                rotated.Add(new());

            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j= 0; j < matrix[i].Count(); j++)
                {
                    char c = matrix[i][j];
                    rotated[j].Add(c);
                }
            }

            // Convert List<List<char>> into List<string>
            List<string> output = [];
            
            foreach (var line in rotated)
            {
                output.Add(new string(line.ToArray()));
            }

            return output;
        }

        private int GetVerticalSymteryLine(List<string> matrix, int symtetry_idx, bool rotate=true)
        {
            var rotated = this.RotateStringList(matrix);
            return this.GetHorizontalSymteryLine(rotated, symtetry_idx);
        }

        private int GetHorizontalSymteryLine(List<string> matrix, int symetry_idx)
        {
            int result = 0;
            int mimial_recursions = Math.Min(symetry_idx, matrix.Count() - symetry_idx);

            // Compensate when it's a double symetry.
            int compesation_n = 0;
            try
            {
                compesation_n = (matrix[symetry_idx] == matrix[symetry_idx + 1]) ? 0 : 1;
            }
            catch (ArgumentException ex)
            {
                compesation_n = 0;
            }

            for (int j = 0; j < mimial_recursions; j++)
            {
                string above = matrix[symetry_idx - j];
                string under = matrix[symetry_idx + j + compesation_n];

                if (under == above)
                {
                    Console.WriteLine($"under: {under}");
                    Console.WriteLine($"above: {above}");
                    result++;
                }
                else
                {
                    return result;
                }
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day14.Part1
{
    [Display(Name = "Day 14 A")]
    public class Solution : BaseSolution
    {
        private char _o = 'O';
        private char _d = '.';
        private char _h = '#';

        public override void Run()
        {
            var matrix = this.GetTiltedMatrix(Lines);
            var points = this.GetPoints(matrix);

            Console.WriteLine(points);
        }

        private int GetPoints(List<char[]> matrix)
        {
            int points = 0;
            for (int i = 0; i < matrix.Count(); i++)
            {
                int row = matrix.Count() - i;
                points += row * (matrix[i].Count(m => m == _o));
            }

            return points;
        }

        private List<char[]> GetTiltedMatrix(List<string> input) 
        {
            List<char[]> matrix = [];
            foreach (string line in input)
                matrix.Add(line.ToCharArray());

            bool done = false;
            while (!done)
            {
                done = true;

                for (int i = 0; i < matrix.Count() - 1; i++)
                {
                    for (int j = 0; j < matrix[i].Count(); j++)
                    {
                        if (!(matrix[i][j] == _d && matrix[i + 1][j] == _o))
                            continue;

                        this.Swap(ref matrix[i][j], ref matrix[i + 1][j]);
                        done = false;
                    }
                }
            }

            return matrix;
        }

        private void Swap(ref char a, ref char b)
        {
            char temp = a;
            a = b;
            b = temp;
        }
    }
}
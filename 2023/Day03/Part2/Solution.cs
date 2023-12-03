    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace _2023.Day03.Part2
    {
        [Display (Name = "Day 03 B")]
        public class Solution : BaseSolution
        {
            private List<(int, int)> _usedIndicies = new();
            private int _total;

            public override void Run()
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    var charline = Lines[i].ToCharArray();

                    for (int j = 0; j < charline.Length; j++)
                    {
                        if (charline[j] != '*') 
                            continue;

                        // Look at the numbers all around the gear.
                        List<int> gear_conns =
                        [
                            this.ProcessNumber(i + 1, j - 1),                       // Bottom left
                            this.ProcessNumber(i + 1, j),                           // Bottom middle
                            this.ProcessNumber(i + 1, j + 1),                       // Bottom right
                            this.ProcessNumber(i - 1, j - 1),                       // Top left
                            this.ProcessNumber(i - 1, j),                           // Top middle
                            this.ProcessNumber(i - 1, j + 1),                       // Top right
                            this.ProcessNumber(i, j - 1),                           // Center left
                            this.ProcessNumber(i, j + 1),                           // Center right
                        ];

                        // Remove all the zeroes.
                        gear_conns.RemoveAll(num => num == 0);

                        // If gear has EXACTLY 2 connections, make a product for it and add it.
                        if (gear_conns.Count() == 2)
                        {
                            int product = gear_conns[0] * gear_conns[1];
                            Console.WriteLine(product);
                            this._total += product;
                        }
                    }
                }

                Console.WriteLine(this._total);
            }


            // Returns a 0 if no numbers are adjecent. otherwise return the number
            public int ProcessNumber(int x, int y)
            {
                if (!char.IsDigit(Lines[x][y]))
                    return 0;

                var result = this.GetAdjacentNumbers(x, y);
                if (result == -1)
                    return 0;

                this._usedIndicies.Add((x, y));
                return result;
            }

            public int GetAdjacentNumbers(int row, int column)
            {
                char[] charline = Lines[row].ToCharArray();

                if (!char.IsDigit(charline[column]))
                    return -1;

                StringBuilder sb = new();
                sb.Append(charline[column]);

                // Check left.
                for (int i = column - 1; i >= 0; i--)
                {
                    if (!char.IsDigit(charline[i]))
                        break;

                    if (_usedIndicies.Contains((row, i)))
                        return -1;

                    sb.Insert(0, charline[i]);
                }

                for (int i = column + 1; i < charline.Length; i++)
                {
                    if (!char.IsDigit(charline[i]))
                        break;

                    if (_usedIndicies.Contains((row, i)))
                        return -1;

                    sb.Append(charline[i]);
                }

                return int.Parse(sb.ToString());
            }
        }
    }

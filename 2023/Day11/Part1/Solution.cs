using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day11.Part1
{
    record Coord(int x, int y);

    [Display(Name = "Day 11 A")]
    public class Solution : BaseSolution
    {
        private List<List<char>> _universe = [];

        private char _u = '#';
        public override void Run()
        {
            this._universe = this.GetExpandedUniverse();
            
            var galaxies = this.GetGalaxies();
            List<(Coord, Coord)> pairs = [];

            int total = 0;

            for (int i = 0; i < galaxies.Count(); i++)
            {
                for (int j = 0; j < galaxies.Count(); j++)
                {
                    if (i == j)
                        continue;

                    if (pairs.Contains((galaxies[i], galaxies[j])) || pairs.Contains((galaxies[j], galaxies[i])))
                        continue;

                    pairs.Add((galaxies[i], galaxies[j]));
                    int distance = this.GetDistance(galaxies[i], galaxies[j]);
                    total += distance;
                    Console.WriteLine($"{i}/{j}: {distance}");
                }
            }
            Console.WriteLine($"Total: {total}");
        }

        private List<Coord> GetGalaxies()
        {
            List<Coord> coords = [];
            for (int i = 0; i < _universe.Count; i++)
            {
                for (int j = 0; j < _universe[i].Count; j++)
                {
                    if (_universe[i][j] == _u)
                    coords.Add(new(i, j));
                }
            }
            return coords;
        }

        private int GetDistance(Coord from, Coord to)
        {
            // Item 1 is row. Item 2 is col.
            int distance = 0;

            // Horizontal
            distance += Math.Abs(to.x - from.x);
            // Vertical
            distance += Math.Abs(to.y - from.y);

            return distance;
        }

        private List<List<char>> GetExpandedUniverse()
        {
            List<List<char>> universe = [];

            var rows = this.GetEmptyRows();
            var cols = this.GetEmptyCols();

            // Add rows.
            for (int i = 0; i < Lines.Count; i++)
            {
                universe.Add(Lines[i].ToCharArray().ToList());
                
                // Add extra rows.
                if (rows.Contains(i))
                {
                    universe.Add(Lines[i].ToCharArray().ToList());
                }
            }

            // Add cols.
            List<List<char>> buffer = [.. universe];
            int c = 0;
            for (int i = 0; i < universe.First().Count(); i++)
            {
                if (cols.Contains(i))
                {
                    // Add extra cols
                    for (int j = 0; j < universe.Count(); j++)
                    {
                        buffer[j].Insert(i + c, '.');
                    }
                    c++;
                }
            }

            return buffer;
        }

        private List<int> GetEmptyCols()
        {
            List<int> empty = [];
            for (int i = 0; i < Lines.First().Count(); i++)
            {
                bool hasNoGalaxies = true;
                foreach (string line in Lines)
                {
                    if (line[i] == _u)
                        hasNoGalaxies = false;
                }
                if (hasNoGalaxies)
                    empty.Add(i);
            }
            return empty;
        }

        private List<int> GetEmptyRows()
        {
            int i = 0;
            return Lines
                .Where(s => (i++ != -1) && !s.Contains(_u))
                .Select(s => i - 1)
                .ToList();
        }
    }
}
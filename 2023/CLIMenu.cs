using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
    public class CLIMenu
    {
        private static string StringRepeat(char c, int n)
        {
            string result = string.Empty;
            for (var i = 0; i < n; i++)
            {
                result += c;
            }

            return result;
        }

        public static void Run(List<BaseSolution> solutions, bool reverse=true)
        {
            // Reverse to get the newest on top.
            if (reverse)
            {
                solutions.Reverse();
            }

            // Select the item
            int idx = 0;
            bool running = true;

            while (running)
            {
                // Clear and set position
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                // Get char size
                int longest_name_count = solutions.OrderBy(solution => solution.GetDisplayableName()).Last().GetDisplayableName().Length;
                int using_size = ((longest_name_count % 2 == 0) ? longest_name_count : longest_name_count + 1) + 2;

                // Display all items
                Console.WriteLine("Advent of code: 2023\n");
                Console.WriteLine(StringRepeat('=', using_size + 1));

                // Display all current list items
                for (var i = 0; i < solutions.Count(); i++)
                {
                    int current_item_size = solutions[i].GetDisplayableName().Length;
                    int chars_left_over = using_size - current_item_size;

                    if (i == idx)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                        Console.Write("|");
                        Console.Write(solutions[i].GetDisplayableName());
                        Console.Write(StringRepeat(' ', chars_left_over - 1));
                        Console.Write("|");

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("|");
                        Console.Write(solutions[i].GetDisplayableName());
                        Console.Write(StringRepeat(' ', chars_left_over - 1));
                        Console.Write("|");
                    }

                    Console.Write("\n");
                }

                Console.WriteLine(StringRepeat('=', using_size + 1));

                // Handle input
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        idx = (idx == 0) ? solutions.Count() : idx;
                        idx--;
                        break;
                    case ConsoleKey.DownArrow:
                        idx = (idx == solutions.Count() - 1) ? -1 : idx;
                        idx++;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.CursorVisible = true;

                        solutions[idx].RunWithTimer();

                        Console.CursorVisible = false;

                        Console.Write("\nPress a key to return to the home menu");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }
        }
    }
}

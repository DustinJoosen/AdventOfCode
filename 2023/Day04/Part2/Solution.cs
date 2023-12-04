using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day04.Part2
{
    [Display (Name = "Day 04 B")]
    public class Solution : BaseSolution
    {
        private int _total = 0;
        private Dictionary<int, int> _cardNumberOfLoops = new() {};

        public override void Run()
        {
            // Load initial cards. every card number needs to have 1.
            for (int i = 0; i < Lines.Count; i++)
            {
                this._cardNumberOfLoops[i + 1] = 1;
            }

            for (int i = 0; i < Lines.Count; i++)
            {
                Regex regex = new Regex(@"^Card\s+(\d+):([\d\s|]+)$");
                Match match = regex.Match(Lines[i]);

                int cardNum = int.Parse(match.Groups[1].Value);
                string[] numbers = match.Groups[2].Value.Split("|");

                // Do it the correct number of times, for the current card.
                for (int _ = 0; _ < _cardNumberOfLoops[cardNum]; _++)
                {
                    int won = this.HandleCard(numbers);

                    // Add extra cards based on the number of cards won.
                    for (int j = 0; j < won; j++)
                    {
                        int num = cardNum + j + 1;
                        this._cardNumberOfLoops[num]++;
                    }
                }
            }

            Console.WriteLine(_total);
        }

        /// <returns>Amount of winning numbers</returns>
        private int HandleCard(string[] numbers)
        {
            _total++;

            string[] winning = numbers[0].Split(" ").Where(num => num != "").ToArray();
            string[] bought = numbers[1].Split(" ").Where(num => num != "").ToArray();

            int count = 0;
            foreach (string num in bought)
            {
                if (winning.Contains(num))
                    count++;
            }

            return count;
        }

    }
}

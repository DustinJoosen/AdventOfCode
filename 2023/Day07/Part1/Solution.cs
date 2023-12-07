using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day07.Part1
{
    public enum HandType
    {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighHand = 1
    }

    [Display (Name = "Day 07 A")]
    public class Solution : BaseSolution
    {
        public override void Run()
        {
            List<(string, int, HandType)> games = []; 
            foreach (string line in Lines)
            {
                var split_line = line.Split(" ");
                string game = split_line[0];
                int bet = int.Parse(split_line[1]);

                HandType handtype = GetHandType(game);
                games.Add((game, bet, handtype));
            }

            int total = 0;
            games = this.OrderGames(games);
            for (int i = 0; i < games.Count; i++)
            {
                var game = games[i];
                int won = game.Item2 * (i + 1);
                total += won;
                Console.WriteLine($"{game.Item1}: {won}");
            }

            Console.WriteLine(total);
        }

        private List<(string, int, HandType)> OrderGames(List<(string, int, HandType)> games)
        {
            return games
                .OrderBy(game => (int)game.Item3)
                .ThenByDescending(game => game.Item1, Comparer<string>.Create((hand1, hand2) =>
                {
                    var cardOrder = "AKQJT98765432";

                    for (int i = 0; i < Math.Min(hand1.Length, hand2.Length); i++)
                    {
                        int comparison = cardOrder.IndexOf(hand1[i]) - cardOrder.IndexOf(hand2[i]);
                        if (comparison != 0)
                        {
                            return comparison;
                        }
                    }
                    return hand1.Length - hand2.Length;
                }))
                .ToList();
        }

        private HandType GetHandType(string hand)
        {
            char[] cards = hand.ToCharArray();

            Dictionary<char, int> group = [];
            foreach (char c in hand)
            {
                group[c] = (group.ContainsKey(c)) ? group[c] + 1 : 1;
            }

            if (IsFiveOfAKind(cards))
                return HandType.FiveOfAKind;
            if (IsFourOfAKind(group))
                return HandType.FourOfAKind;
            if (IsFullHouse(group))
                return HandType.FullHouse;
            if (IsThreeOfAKind(group))
                return HandType.ThreeOfAKind;
            if (IsTwoPair(group))
                return HandType.TwoPair;
            if (IsOnePair(group))
                return HandType.OnePair;
            if (IsHighCard(cards))
                return HandType.HighHand;

            throw new Exception("hand does not match any hand type");
        }

        // Independent functions that check if a hand is of a certain type.
        private bool IsFiveOfAKind(char[] hand) =>
            hand.Distinct().Count() == 1;
        private bool IsFourOfAKind(Dictionary<char, int> group) =>
            group.ContainsValue(4) && group.Count() == 2;
        private bool IsFullHouse(Dictionary<char, int> group) =>
            group.ContainsValue(2) && group.ContainsValue(3);
        private bool IsThreeOfAKind(Dictionary<char, int> group) =>
            group.ContainsValue(3) && group.Count() == 3;
        private bool IsTwoPair(Dictionary<char, int> group) =>
            group.ContainsValue(1) && group.Count(kv => kv.Value == 2) == 2;
        private bool IsOnePair(Dictionary<char, int> group) =>
            group.ContainsValue(2) && group.Count() == 4;
        private bool IsHighCard(char[] hand) =>
            hand.Distinct().Count() == hand.Count();
    }
}

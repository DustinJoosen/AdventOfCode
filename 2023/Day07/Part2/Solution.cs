using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day07.Part2
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

    [Display (Name = "Day 07 B")]
    public class Solution : BaseSolution
    {
        private static char _j = 'J';

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
                
                Console.WriteLine($"{game.Item1}:{game.Item3} ({won})");
            }

            Console.WriteLine(total);
        }

        private List<(string, int, HandType)> OrderGames(List<(string, int, HandType)> games)
        {
            return games
                .OrderBy(s => (int)s.Item3)
                .ThenByDescending(game => game.Item1, Comparer<string>.Create((hand1, hand2) => CompareHands(hand1, hand2)))
                .ToList();
        }

        private int CompareHands(string hand1, string hand2)
        {
            if (hand1 == hand2)
                return 0;

            var cardOrder = "AKQT98765432J";

            for (int i = 0; i < Math.Min(hand1.Length, hand2.Length); i++)
            {
                if (hand1[i] == hand2[i])
                    continue;

                int h1 = cardOrder.IndexOf(hand1[i]);
                int h2 = cardOrder.IndexOf(hand2[i]);
                return (h1 > h2) ? 1 : -1;
            }
            return 0;
        }

        private HandType GetHandType(string hand)
        {
            // Stupid edge case
            if (hand == "JJJJJ")
                return HandType.FiveOfAKind;

            char[] cards = hand.ToCharArray();

            Dictionary<char, int> group = [];
            foreach (char c in hand)
            {
                group[c] = (group.ContainsKey(c)) ? group[c] + 1 : 1;
            }

            if (IsFiveOfAKind(new Dictionary<char, int>(group)))
                return HandType.FiveOfAKind;
            if (IsFourOfAKind(new Dictionary<char, int>(group)))
                return HandType.FourOfAKind;
            if (IsFullHouse(new Dictionary<char, int>(group)))
                return HandType.FullHouse;
            if (IsThreeOfAKind(new Dictionary<char, int>(group)))
                return HandType.ThreeOfAKind;
            if (IsTwoPair(new Dictionary<char, int>(group)))
                return HandType.TwoPair;
            if (IsOnePair(new Dictionary<char, int>(group)))
                return HandType.OnePair;
            if (IsHighCard(new Dictionary<char, int>(group)))
                return HandType.HighHand;

            throw new Exception("hand does not match any hand type");
        }

        private Dictionary<char, int> ApplyJoker(ref Dictionary<char, int> group)
        {
            if (!group.ContainsKey(_j))
            {
                return group;
            }

            char mostCommon = group.Where(x => x.Key != _j).Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            group[mostCommon] += group[_j];
            group.Remove(_j);

            return group;
        }

        private bool IsFiveOfAKind(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(5);
        }
        private bool IsFourOfAKind(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(4) && group.Count() == 2;
        }
        private bool IsFullHouse(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(2) && group.ContainsValue(3);
        }
        private bool IsThreeOfAKind(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(3) && group.Count() == 3 && !IsFullHouse(group);
        }
        private bool IsTwoPair(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(1) && group.Count(kv => kv.Value == 2) == 2;
        }
        private bool IsOnePair(Dictionary<char, int> group)
        {
            this.ApplyJoker(ref group);
            return group.ContainsValue(2) && group.Count() == 4;
        }
        private bool IsHighCard(Dictionary<char, int> group)
        {
            return group.Count() == 5;
        }
    }
}
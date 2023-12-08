using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day08.Part2
{
    public record Node(string nodeName, string leftNode, string rightNode);

    [Display (Name = "Day 08 B")]
    public class Solution : BaseSolution
    {
        private string _directions;
        private char _currentDirection;
        private int _directionIdx = 0;

        public override void Run()
        {
            this._directions = Lines[0];
            this._currentDirection = this._directions[0];

            List<string> lines = [];
            for (int i = 2; i < Lines.Count; i++)
            {
                lines.Add(Lines[i]);
            }

            var exception = new KeyNotFoundException("This node could not be found");

            List<Node> nodes = GetNodes(lines);
            List<Node> usedNodes = nodes
                .Where(node => node.nodeName.EndsWith('A'))
                .ToList();

            // Get the earliest encounters with a Z.
            List<int> usedForLCM = [];
            for (int i = 0; i < usedNodes.Count; i++)
            {
                int value = 0;
                Node current = usedNodes[i];
                do
                {
                    string next = "";
                    if (_currentDirection == 'L')
                        next = current.leftNode;
                    else
                        next = current.rightNode;

                    value++;
                    Console.WriteLine(next);
                    current = nodes.SingleOrDefault(n => n.nodeName == next) ?? throw exception;

                    this.GoToNextDirection();
                } while (!current.nodeName.EndsWith("Z"));

                usedForLCM.Add(value);
            }

            // Calculate the LCM
            long lcmResult = CalculateLCM(usedForLCM);

            // Display.
            Console.WriteLine("result:  " + lcmResult);
        }

        private long CalculateLCM(List<int> numbers)
        {
            if (numbers.Count() == 0)
                return 0;

            long lcm = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                lcm = CalculateLCM(lcm, numbers[i]);
            }

            return lcm;
        }

        private long CalculateLCM(long a, long b)
        {
            return Math.Abs(a * b) / this.GCD(a, b);
        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }


        private void GoToNextDirection()
        {
            _directionIdx++;
            _currentDirection = this._directions[_directionIdx % _directions.Length];
        }

        private List<Node> GetNodes(List<string> lines)
        {
            List<Node> nodes = [];
            Regex regex = new Regex(@"^(\w{3}) = \((\w{3}), (\w{3})\)$");

            foreach (string line in lines)
            {
                Match match = regex.Match(line);
                var node = new Node(
                    nodeName: match.Groups[1].Value,
                    leftNode: match.Groups[2].Value,
                    rightNode: match.Groups[3].Value);

                nodes.Add(node);
            }

            return nodes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023.Day08.Part1
{
    public record Node(string nodeName, string leftNode, string rightNode);

    [Display (Name = "Day 08 A")]
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
            var value = 0;

            List<Node> nodes = GetNodes(lines);
            Node current = nodes
                .SingleOrDefault(n => n.nodeName == "AAA") ?? throw exception;
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
            } while (current.nodeName != "ZZZ");

            Console.WriteLine($"value: {value}");
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

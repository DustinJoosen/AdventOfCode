
using _2023;

List<BaseSolution> solutions = [
    new _2023.Day01.Solution(),
    new _2023.Day02.Part1.Solution(),
    new _2023.Day02.Part2.Solution(),
    new _2023.Day03.Part1.Solution(),
    new _2023.Day03.Part2.Solution(),
    new _2023.Day04.Part1.Solution(),
    new _2023.Day04.Part2.Solution(),
];

CLIMenu.Run(solutions, reverse: true);

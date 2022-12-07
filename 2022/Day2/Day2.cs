namespace _2022.Day2;

internal static class Second {
    private static void Main() {
      
        var lines = File.ReadAllLines("../../../input.txt");

        var handShapesPoints = new Dictionary<string, int> { { "X", 1 }, { "Y", 2 }, { "Z", 3 } };
        var stagesPoints = new Dictionary<string, int>
        {
            { "A X", 3 },
            { "A Y", 6 },
            { "A Z", 0 },
            { "B X", 0 },
            { "B Y", 3 },
            { "B Z", 6 },
            { "C X", 6 },
            { "C Y", 0 },
            { "C Z", 3 },
        };
        var draws = new Dictionary<string, string> { { "A", "X" }, { "B", "Y" }, { "C", "Z" } };
        var wins = new Dictionary<string, string> { { "A", "Y" }, { "B", "Z" }, { "C", "X" } };
        var loses = new Dictionary<string, string> { { "A", "Z" }, { "B", "X" }, { "C", "Y" } };
        var totalScore = 0;

        foreach (var line in lines)
        {
            var opponentSelection = line[0].ToString();
            var typeOfGame = line[2];

            var mySelection = typeOfGame switch
            {
                'Y' => draws[opponentSelection],
                'X' => loses[opponentSelection],
                'Z' => wins[opponentSelection],
                _ => throw new ArgumentOutOfRangeException()
            };

            totalScore += handShapesPoints[mySelection];
            totalScore += stagesPoints[opponentSelection + " " + mySelection];
        }

        Console.WriteLine(totalScore);
    }
}
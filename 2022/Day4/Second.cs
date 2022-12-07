namespace _2022.Day4;

internal static class Second {
    private static void Main() {
        
        var lines = File.ReadAllLines("../../../Day4/input.txt");

        bool IsOverlappedPair(int firstStar, int firstEnd, int secondStart, int secondEnd)
        {
            return firstStar <= secondEnd && secondStart <= firstEnd;
        }

        var totalOverlappedPairs = 0;
        foreach (var pairs in lines) {
            
            var splitPairs  = pairs.Split(",");
            
            var firstPairs = splitPairs[0].Split("-");
            var secondPairs = splitPairs[1].Split("-");
            
            var firstStar = int.Parse(firstPairs[0]);
            var firstEnd = int.Parse(firstPairs[1]);
            
            var secondStart = int.Parse(secondPairs[0]);
            var secondEnd = int.Parse(secondPairs[1]);

            if (IsOverlappedPair(firstStar, firstEnd, secondStart, secondEnd))
            {
                totalOverlappedPairs++;
            }
        }
        
        Console.WriteLine(totalOverlappedPairs);
    }
}
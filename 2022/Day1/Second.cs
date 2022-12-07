namespace _2022.Day1;

internal static class Second {
    private static void Main() {
        
        var lines = File.ReadAllLines("../../../Day1/input.txt");

        var sum = 0;
        var allSums = new SortedSet<int>();

        foreach (var line in lines) {
            if (string.IsNullOrEmpty(line)) {
                allSums.Add(sum);
                sum = 0;
            }
            else {
                sum += int.Parse(line);
            }
        }

        var topThreeSum = allSums.TakeLast(3).Sum();

        Console.WriteLine(topThreeSum);
    }
}
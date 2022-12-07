namespace _2022.Day3;

internal static class Second {
    private static void Main() {
        
        var lines = File.ReadAllLines("../../../Day3/input.txt");

        int FindItemPriority(char item) {
            const string items = "abcdefghijklmnopqrstuvwxyz";
            var offset = char.IsLower(item) ? 1 : 27;
    
            return items.IndexOf(char.ToLower(item), StringComparison.Ordinal) + offset;
        }

        int FindGroupSum(string[] group) {
            var repeatedElement = new char();
            foreach (var value in group[0]) {
                if (group[1].IndexOf(value) is not -1 && group[2].IndexOf(value) is not -1) {
                    repeatedElement = value;
                    break;
                }
            }
            return FindItemPriority(repeatedElement);
        }

        var sum = 0;
        for (var i = 2; i < lines.Length; i += 3)
        {
            var currentGroup = new[] {
                lines[i - 2],
                lines[i - 1],
                lines[i]
            };

            sum += FindGroupSum(currentGroup);
        }

        Console.WriteLine(sum);
    }
}
namespace _2022.Day3;

internal static class First {
    private static void Main() {
        
        var lines = File.ReadAllLines("../../../Day3/input.txt");

        int FindItemPriority(char item) {
            const string items = "abcdefghijklmnopqrstuvwxyz";
            var offset = char.IsLower(item) ? 1 : 27;
            
            return items.IndexOf(char.ToLower(item), StringComparison.Ordinal) + offset;
        }

        int FindItemSum(string item) {
            var halfSize = item.Length / 2;
            var firstHalf = item[..halfSize];
            var secondHalf = item[halfSize..];
            var repeatedElements = new HashSet<char>();
    
            foreach (var value in firstHalf) {
                if (secondHalf.IndexOf(value) != -1) {
                    repeatedElements.Add(value);
                }
            }

            return repeatedElements.Sum(FindItemPriority);
        }

        var sum = lines.Sum(FindItemSum);
        
        Console.WriteLine(sum);
    }
}
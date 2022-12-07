namespace _2022.Day5;

internal class First {
    private static async Task Main() {
        var lines = await File.ReadAllLinesAsync("../../../Day5/input.txt");
        var separatorIndex = lines.ToList().FindIndex(x => x == "");
        var stacksLines = lines.Take(separatorIndex).ToList();
        var rearrangementLines = lines.Skip(stacksLines.Count + 1);

        List<Stack<string>> GetStacksFromLines(List<string> stackLines) {
            stackLines = stackLines.SkipLast(1).ToList();
            var stacks = Enumerable
                .Range(0, stackLines[0].Length / 3)
                .Select(x => new Stack<string>()).ToList();
            
            foreach (var line in stackLines) {
                var i = 0;
                var j = 0;
                while (i < line.Length) {
                    var crate = line.Substring(i, 3);
                    if (crate != "   ") stacks[j].Push(crate);

                    i += 4;
                    j++;
                }
            }

            return stacks.Select(x => new Stack<string>(x)).ToList();
        }

        var stacks = GetStacksFromLines(stacksLines);
        
        foreach (var rearrangementLine in rearrangementLines) {
            var replaced = rearrangementLine
                .Replace("move ", "")
                .Replace(" from ", ",")
                .Replace(" to ", ",");
            
            var actions = replaced.Split(',');
            var movements = int.Parse(actions[0]);
            var source = int.Parse(actions[1]) - 1;
            var target = int.Parse(actions[2]) - 1;
            
            while (movements > 0) {
                var crate = stacks[source].Pop();
                stacks[target].Push(crate);
                movements--;
            }
        }

        var topCrates = string.Empty;
        foreach (var stack in stacks) {
             if (stack.Count > 0) {
                 var crate = stack.Pop();
                 topCrates += crate[1];
             }
        }
           

        Console.WriteLine(topCrates);
    }
}
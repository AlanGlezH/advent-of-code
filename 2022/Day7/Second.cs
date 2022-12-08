namespace _2022.Day7;

class Second {
    static async Task Main() {
        var commands = await File.ReadAllLinesAsync("../../../Day7/input.txt");

        var stack = new Stack<Dir>();
        stack.Push(new Dir("/", 0));
        var total = 0;
        var dirSizes = new SortedSet<int>();
        const int diskSpace = 70_000_000;
        const int spaceNeeded = 30_000_000;

        void CalculateSizeBasedOnPreviousDir() {
            var previousDir = stack.Pop();
            var currentDir = stack.Peek();
            if (previousDir.Size <= 100_000) {
                total += previousDir.Size;
            }
            currentDir.Size += previousDir.Size;
            dirSizes.Add(previousDir.Size);
        }
        
        void HandleFileOrDirInformation(string info) {
            var fileInformation = info.Split(" ");
            if (fileInformation[0] != "dir") {
                var currentDir = stack.Peek();
                currentDir.Size += int.Parse(fileInformation[0]);
            }
        }
        
        foreach (var command in commands) {
            if (command.StartsWith("$ ls") || command.StartsWith("$ cd /")) {
                continue;
            }

            if (command.StartsWith("$ cd")) {
                var name = command[5..];
                if (name == "..") {
                    CalculateSizeBasedOnPreviousDir();
                } else {
                    stack.Push(new Dir(name, 0));
                }
                continue;
            }
            HandleFileOrDirInformation(command);
        }
        
        while (stack.Count > 1) {
            CalculateSizeBasedOnPreviousDir();
        }

        var rootDirectory = stack.Pop();
        dirSizes.Add(rootDirectory.Size);
        var freeSpace = diskSpace - rootDirectory.Size;
        var sizeToBeDeleted  = dirSizes.First(size => freeSpace + size >= spaceNeeded);
        
        Console.WriteLine(sizeToBeDeleted);
    }
}
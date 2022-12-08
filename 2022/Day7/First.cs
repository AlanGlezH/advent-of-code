namespace _2022.Day7;

class Dir {
    public string Name { get; }
    public int Size { get; set; }

    public Dir(string name, int size) {
        Name = name;
        Size = size;
    }
}

class First {
    static async Task Main() {
        var commands = await File.ReadAllLinesAsync("../../../Day7/input.txt");

        var stack = new Stack<Dir>();
        stack.Push(new Dir("/", 0));
        var total = 0;
        
        void CalculateSizeBasedOnPreviousDir() {
             var previousDir = stack.Pop();
             var currentDir = stack.Peek();
             if (previousDir.Size <= 100_000) {
                 total += previousDir.Size;
             }
             currentDir.Size += previousDir.Size;
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
        
        Console.WriteLine(total);
    }
}
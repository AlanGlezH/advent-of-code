namespace _2022.Day10;

public class Second {
    static async Task Main() {
        var lines = (await File.ReadAllLinesAsync("../../../Day10/input.txt")).ToList();
        var instructions = new Dictionary<string, int> { { "addx", 2 }, { "noop", 1 } };

        string PrintImage() {
            var (xRegister, totalCycles) = (1, 1);
            var crtScreen = string.Empty;

            foreach (var line in lines) {
                var name = line[..4];
                var value = name == "addx" ? int.Parse(line[5..]) : 0;
                var cycles = instructions[name];
                
                for (var i = 0; i < cycles; i++) {
                    var screenColumn = (totalCycles - 1) % 40;
                    crtScreen += Math.Abs(xRegister - screenColumn) < 2 ? '#' : '.';
                    if (screenColumn == 39) {
                        crtScreen += "\n";
                    }
                    totalCycles++;
                }
                xRegister += value;
            }
            return crtScreen;
        }
        
        Console.WriteLine(PrintImage());
    }
}
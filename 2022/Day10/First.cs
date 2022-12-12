namespace _2022.Day10;

public class First {
    static async Task Main() {
        var lines = (await File.ReadAllLinesAsync("../../../Day10/input.txt")).ToList();
        var instructions = new Dictionary<string, int> { { "addx", 2 }, { "noop", 1 } };
        var checkPoints = new HashSet<int>{ 20, 60, 100, 140, 180, 220};

        int CalculateSignalSum() {
            var (xRegister, totalCycles) = (1, 1);
            var signalStrengthsTotal = 0;

            foreach (var line in lines) {
                var name = line[..4];
                var value = name == "addx" ? int.Parse(line[5..]) : 0;
                var cycles = instructions[name];
                
                for (var i = 0; i < cycles; i++) {
                    if (checkPoints.Contains(totalCycles)) {
                        signalStrengthsTotal += (xRegister * totalCycles);
                    }
                    totalCycles++;
                }
                xRegister += value;
            }

            return signalStrengthsTotal;
        }
        
        Console.WriteLine(CalculateSignalSum());
    }
}
namespace _2022.Day11;

class Monkey {
    public long TotalItemsCount { get; private set; }
    private Queue<long> Items { get; }
    private Operation Operation { get; }
    public int TestValue { get; }
    private (int onTrue, int onFalse) NextMonkey { get; }

    public Monkey(IEnumerable<long> items, Operation operation, int testValue,
        (int monkeyTrue, int monkeyFalse) nextMonkey) {
        Items = new Queue<long>(items);
        Operation = operation;
        TestValue = testValue;
        NextMonkey = nextMonkey;
    }

    private bool TestItem(long item) {
        return item % TestValue == 0;
    }

    private void ThrowItem(long item, List<Monkey> monkeys) {
        if (TestItem(item)) {
            monkeys[NextMonkey.onTrue].Items.Enqueue(item);
        }else {
            monkeys[NextMonkey.onFalse].Items.Enqueue(item);
        }
    }

    private long InspectItem(long item) {
        var value = Operation.Value == "old" ? item : long.Parse(Operation.Value);
        var newWorryLevel = Operation.Operator switch {
            "*" => item * value,
            "+" => item + value,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return newWorryLevel;
    }
        
    public void ExecuteTurn(List<Monkey> monkeys, int factor) {
        while (Items.Any()) {
            var currentItem = Items.Dequeue();
            var item = InspectItem(currentItem);
            item %= factor;
            ThrowItem(item, monkeys);
            TotalItemsCount++;
        }
    }
}

class Second {
    static async Task Main() {
        var lines = await File.ReadAllTextAsync("../../../Day11/input.txt");

        List<Monkey> GetMonkeys() {
            var monkeys = lines.Split("\n\n").Select(line => {
                var splitLine = line.Split("\n");
                var startingItems = splitLine[1][18..].Split(", ").Select(long.Parse);
                var operation = splitLine[2].Split(" ").TakeLast(2).ToArray();
                var test = splitLine[3].Split(" ").TakeLast(1).Select(int.Parse).FirstOrDefault();
                var monkeyOnTrue = int.Parse(splitLine[4].Split(" ").Last());
                var monkeyOnFalse = int.Parse(splitLine[5].Split(" ").Last());
                
                return new Monkey(
                    startingItems, 
                    new Operation(operation[0], operation[1]), 
                    test,
                    (monkeyOnTrue, monkeyOnFalse)
                );
            });
            return monkeys.ToList();
        }

        long CalculateBusinessLevel(IEnumerable<Monkey> monkeys) {
            var businessLevel = monkeys
                .OrderByDescending(x => x.TotalItemsCount)
                .Take(2)
                .Aggregate(1L,(i, x) => i * x.TotalItemsCount);
            
            return businessLevel;
        }

        long GetMonkeyBusinessLevelInRounds(long rounds) {
            var monkeys = GetMonkeys();
            var factor = monkeys.Aggregate(1, (i, monkey) => i * monkey.TestValue);
            for (var i = 0; i < rounds; i++) {
                monkeys.ForEach(m => m.ExecuteTurn(monkeys, factor));
            }
            return CalculateBusinessLevel(monkeys);
        }
        
        Console.WriteLine(GetMonkeyBusinessLevelInRounds(10_000));
    }
}
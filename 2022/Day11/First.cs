namespace _2022.Day11;
public record Operation (string Operator, string Value);

class First {
    class Monkey {
        public int TotalItemsCount { get; private set; }
        private Queue<int> Items { get; }
        private Operation Operation { get; }
        private int TestValue { get; }
        private (int onTrue, int onFalse) NextMonkey { get; }
        
        public Monkey(IEnumerable<int> items, Operation operation, int testValue, (int monkeyTrue, int monkeyFalse) nextMonkey) {
            Items = new Queue<int>(items);
            Operation = operation; 
            TestValue = testValue;
            NextMonkey = nextMonkey;
        }
    
        private bool TestItem(int item) {
            return item % TestValue == 0;
        }
    
        private void ThrowItem(int item, List<Monkey> monkeys) {
            if(TestItem(item)){
                monkeys[NextMonkey.onTrue].Items.Enqueue(item);
            }else{
                monkeys[NextMonkey.onFalse].Items.Enqueue(item);
            }
        }
    
        private int InspectItem(int item) {
            var value = Operation.Value == "old" ? item : int.Parse(Operation.Value);
            var newWorryLevel = Operation.Operator switch {
                "*" => item * value,
                "+" => item + value,
                _ => throw new ArgumentOutOfRangeException()
            };
        
            return newWorryLevel;
        }
        
        public void ExecuteTurn(List<Monkey> monkeys, int factor) {
            while (Items.Count > 0) {
                var currentItem = Items.Dequeue();
                var item = InspectItem(currentItem);
                item %= factor;
                ThrowItem(item, monkeys);
                TotalItemsCount++;
            }
        }
    }
    
    static async Task Main() {
        var lines = await File.ReadAllTextAsync("../../../Day11/input.txt");

        List<Monkey> GetMonkeys() {
            var monkeys = lines.Split("\n\n").Select(line => {
                var splitLine = line.Split("\n");
                var startingItems = splitLine[1][18..].Split(", ").Select(int.Parse);
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

        int CalculateBusinessLevel(IEnumerable<Monkey> monkeys) {
            var businessLevel = 1; 
            monkeys
                .OrderByDescending(x => x.TotalItemsCount)
                .Take(2)
                .ToList()
                .ForEach(x => businessLevel *= x.TotalItemsCount);
            
            return businessLevel;
        }

        int GetMonkeyBusinessLevelInRounds(int rounds) {
            const int reliefDivider = 3;
            var monkeys = GetMonkeys();
            for (var i = 0; i < rounds; i++) {
                monkeys.ForEach(m => m.ExecuteTurn(monkeys, reliefDivider));
            }
            return CalculateBusinessLevel(monkeys);
        }
        Console.WriteLine(GetMonkeyBusinessLevelInRounds(20));
    }
}
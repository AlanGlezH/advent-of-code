namespace _2022.Day9;

class Knot {
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsTail { get; }

    public Knot(int x, int y, bool isTail = false) {
        X = x;
        Y = y;
        IsTail = isTail;
    }
}

public class First {
    static async Task Main() {
        var lines = (await File.ReadAllLinesAsync("../../../Day9/input.txt")).ToList();
        var tail = new Knot(0, 0);
        var head = new Knot(0, 0);
        var visited = new HashSet<string>{"0,0"};
        
        void MoveTail() {
            var tailPositionX = tail.X;
            var tailPositionY = tail.Y;
            
            while (tailPositionX != head.X || tailPositionY != head.Y) {
                tail.X = tailPositionX;
                tail.Y = tailPositionY;
                visited.Add(tailPositionX + "," + tailPositionY);

                if (tailPositionX < head.X) {
                    tailPositionX++;
                } else if (tailPositionX > head.X) {
                    tailPositionX--;
                }

                if (tailPositionY < head.Y) {
                    tailPositionY++;
                } else if (tailPositionY > head.Y) {
                    tailPositionY--;
                }
            }
            
        }

        void MoveHead(char typeOfMotion, int movementsNumber) {
            switch (typeOfMotion) {
                case 'R':
                    head.X += movementsNumber;
                    break;
                case 'L':
                    head.X -= movementsNumber;
                    break;
                case 'U':
                    head.Y += movementsNumber;
                    break;
                case 'D':
                    head.Y -= movementsNumber;
                    break;
            }
        }
        
        void MoveRope() {
            foreach (var motion in lines) {
                var typeOfMotion = motion[0];
                var motionNumbers = int.Parse(motion[2..]);
                MoveHead(typeOfMotion, motionNumbers);
                MoveTail();
            } 
        }
        
        MoveRope();

        Console.WriteLine(visited.Count);
    }
}
namespace _2022.Day9;

public class Second {
    static async Task Main() {
        var lines = (await File.ReadAllLinesAsync("../../../Day9/input.txt")).ToList();
        var tail = new Knot(0, 0, true);
        var head = new Knot(0, 0);
        var visited = new HashSet<string>{"0,0"};
        var knots = new List<Knot>{ head };
        knots.AddRange(Enumerable.Range(0,8).Select( x => new Knot(0,0)));
        knots.Add(tail);
        
        void MoveBackKnot(Knot frontKnot, Knot backKnot) {
            var backKnotX = backKnot.X;
            var backKnotY = backKnot.Y;

            while (backKnotX != frontKnot.X || backKnotY != frontKnot.Y) {
                backKnot.X = backKnotX;  
                backKnot.Y = backKnotY;

                if (backKnot.IsTail) {
                    visited.Add(backKnotX + "," + backKnotY);
                }

                if (backKnotX < frontKnot.X) {
                    backKnotX++;
                } else if (backKnotX > frontKnot.X) {
                    backKnotX--;
                }

                if (backKnotY < frontKnot.Y) {
                    backKnotY++;
                } else if (backKnotY > frontKnot.Y) {
                    backKnotY--;
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
                
                for (var i = 0; i < motionNumbers; i++) {
                    MoveHead(typeOfMotion, 1);
                    
                    for (var knot = 0; knot < knots.Count - 1 ; knot++) {
                        MoveBackKnot(knots[knot], knots[knot + 1]);
                    }
                }
            } 
        }
        
        MoveRope();

        Console.WriteLine(visited.Count);
    }
}
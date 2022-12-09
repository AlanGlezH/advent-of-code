namespace _2022.Day8;

class First {
    static async Task Main() {
        var lines = (await File.ReadAllLinesAsync("../../../Day8/input.txt")).ToList();
        var treeMap = new List<List<int>>();
        
        lines.ForEach(line => {
            var array = line.ToList().Select(c => int.Parse(c.ToString()));
            treeMap.Add(array.ToList());
        });

        bool OnRight(int positionY, int positionX) {
            for(var i = positionX + 1; i < treeMap[positionY].Count; i++) {
                if (treeMap[positionY][i] >= treeMap[positionY][positionX]) {
                    return false;
                }
            }
            return true;
        }

        bool OnLeft(int positionY, int positionX) {
            for(var i = positionX - 1; i >=0; i--) {
                if (treeMap[positionY][i] >= treeMap[positionY][positionX]) {
                    return false;
                }
            }
            return true;
        }

        bool LookDown(int positionY, int positionX) {
            for(var i = positionY + 1; i < treeMap[positionY].Count; i++) {
                if (treeMap[i][positionX] >= treeMap[positionY][positionX]) {
                    return false;
                }
            }
            return true;
        }

        bool LookUp(int positionY, int positionX) {
            for(var i = positionY - 1; i >=0; i--) {
                if (treeMap[i][positionX] >= treeMap[positionY][positionX]) {
                    return false;
                }
            }
            return true;
        }

        bool CheckVisibility(int positionY, int positionX) {
            return OnLeft(positionY, positionX) || OnRight(positionY, positionX) || LookUp(positionY, positionX) ||
                   LookDown(positionY, positionX);
        }

        int GetVisibleTrees() {
            var visibleTrees = (treeMap.Count + treeMap[0].Count) * 2 - 4;
            for (int i = 1; i < treeMap.Count-1; i++) {
                for (int j = 1; j < treeMap[i].Count-1; j++) {
                    if (CheckVisibility(i, j)) {
                        visibleTrees++;
                    }
                }
            }
            return visibleTrees;
        }
        
        Console.WriteLine(GetVisibleTrees());
    }
}
namespace _2022.Day8;

class Second {
    static async Task Main()
    {
        var lines = (await File.ReadAllLinesAsync("../../../Day8/input.txt")).ToList();
        var treeMap = new List<List<int>>();
        
        lines.ForEach(line => {
            var array = line.ToList().Select(c => int.Parse(c.ToString()));
            treeMap.Add(array.ToList());
        });

        int OnRight(int positionY, int positionX) {
            var viewedTrees = 0;
            for(var i = positionX + 1; i < treeMap[positionY].Count; i++) {
                if (treeMap[positionY][i] < treeMap[positionY][positionX]) {
                    viewedTrees++;
                }
                
                if(treeMap[positionY][i] >= treeMap[positionY][positionX]){
                    viewedTrees++;
                    return viewedTrees;
                }
            }
            return viewedTrees;
        }

        int OnLeft(int positionY, int positionX) {
            var viewedTrees = 0;
            for(var i = positionX - 1; i >=0; i--) {
                if (treeMap[positionY][i] < treeMap[positionY][positionX]) {
                    viewedTrees++;
                }
                if(treeMap[positionY][i] >= treeMap[positionY][positionX]){
                    viewedTrees++;
                    return viewedTrees;
                }
            }
            return viewedTrees;
        }

        int LookDown(int positionY, int positionX)
        {
            var viewedTrees = 0;
            for(var i = positionY + 1; i < treeMap[positionY].Count; i++) {
                if (treeMap[i][positionX] < treeMap[positionY][positionX]) {
                    viewedTrees++;
                }
                if(treeMap[i][positionX] >= treeMap[positionY][positionX]){
                    viewedTrees++;
                    return viewedTrees;
                }
            }
            return viewedTrees;
        }

        int LookUp(int positionY, int positionX) {
            var viewedTrees = 0;
            for(var i = positionY - 1; i >=0; i--) {
                if (treeMap[i][positionX] < treeMap[positionY][positionX]) {
                    viewedTrees++;
                }
                if(treeMap[i][positionX] >= treeMap[positionY][positionX]){
                    viewedTrees++;
                    return viewedTrees;
                }
            }
            return viewedTrees;
        }
        
        int GetScenicScore(int positionY, int positionX) {
            return OnLeft(positionY, positionX)  * OnRight(positionY, positionX) *
                   LookUp(positionY, positionX) * LookDown(positionY, positionX);
        }

        int GetHighestScenicScore() {
            var scenicScores = new SortedSet<int>();
            for (int i = 1; i < treeMap.Count-1; i++) {
                for (int j = 1; j < treeMap[i].Count-1; j++) {
                    var score = GetScenicScore(i, j);
                    scenicScores.Add(score);
                }
            }
            return scenicScores.Last();
        }

        
        Console.WriteLine(GetHighestScenicScore());
    }
}
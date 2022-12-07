namespace _2022.Day6;

class Second {
    static async Task Main() {

        var dataStream = await File.ReadAllTextAsync("../../../Day6/input.txt");
        var markerPosition = 0;
       
        for (var i = 0; i < dataStream.Length-13; i++) {
            var sequence = Enumerable.Range(0, 14).Select(j => dataStream[i + j]).ToList();
            var characters = new HashSet<char>(sequence);

            if (characters.Count != 14) continue;
           
            markerPosition = i + 14;
            break;
        }
       
        Console.WriteLine(markerPosition);
    }
}
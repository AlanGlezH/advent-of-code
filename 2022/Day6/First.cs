namespace _2022.Day6;

class First {
   static async Task Main() {

       var dataStream = await File.ReadAllTextAsync("../../../Day6/input.txt");
       var markerPosition = 0;
       
       for (var i = 0; i < dataStream.Length-3; i++) {     
           var sequence = Enumerable.Range(0, 4).Select(j => dataStream[i + j]).ToList();
           var characters = new HashSet<char>(sequence);

           if (characters.Count != 4) continue;
           
           markerPosition = i + 4;
           break;
       }
       
       Console.WriteLine(markerPosition);
   }
}
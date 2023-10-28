var counter = 0;
var totals = new List<int>();

try {
    using (StreamReader reader = new StreamReader("input.txt")) {
        string line;
        line = reader.ReadLine();

        while (line != null) {
            if (string.IsNullOrWhiteSpace(line)) {
                totals.Add(counter);
                counter = 0;
            } else {
                counter += Convert.ToInt32(line);
            }
                
            line = reader.ReadLine();
        }
    }

    var topThreeTotal = totals.OrderDescending().Take(3).Sum();
    Console.WriteLine(topThreeTotal);
} catch(Exception e) {
    Console.WriteLine($"Exception: {e.Message}");
}

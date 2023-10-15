using System.Text.RegularExpressions;

string line;

try {
    using (StreamReader reader = new StreamReader("./input.txt")) {
        line = reader.ReadLine();

        var startingStacks = new List<string>();
        while (line != null && !string.IsNullOrWhiteSpace(line)) {
            startingStacks.Add(line);
            line = reader.ReadLine();
        }
        var d = new Dictionary<string, Stack<string>>();

        for (int i = startingStacks.Count - 1; i >= 0; i--) {
            var crates = startingStacks.ElementAt(i).Chunk(4);
            for (int j = 0; j < crates.Count(); j++) {
                var crate = string.Concat(crates.ElementAt(j)).Trim();
                if (!string.IsNullOrWhiteSpace(crate) && !crate.Contains('[')) {
                   d.Add(crate, new Stack<string>()); 
                } else if (crate.Contains('[')) {
                    d[(j + 1).ToString()].Push(crate.Trim());
                }
            }
        }
         
        Regex rx = new Regex(@"^move (?<quantity>\d{1,3}) from (?<start>\d{1,3}) to (?<dest>\d{1,3})$", RegexOptions.ECMAScript);

        while (line != null) {
            var match = rx.Match(line);

            if (match.Success) {
                var quantity = int.Parse(match.Groups["quantity"].ToString());
                var start = match.Groups["start"].ToString();
                var dest = match.Groups["dest"].ToString();

                for (int i = quantity; i > 0; i--) {
                    var crateToMove = d[start].Pop();
                    d[dest].Push(crateToMove);
                }
            }
            line = reader.ReadLine();
        }

        foreach (var x in d) {
            Console.WriteLine(x.Key);
            var v = x.Value.Pop();
            Console.WriteLine(v);
        }
    }
} catch(Exception e) {
    Console.WriteLine($"Exception: {e.Message}");
}

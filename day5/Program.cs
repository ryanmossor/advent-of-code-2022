using System.Text;
using System.Text.RegularExpressions;

string line;
var movesRegex = new Regex(@"^move (?<quantity>\d{1,3}) from (?<start>\d{1,3}) to (?<dest>\d{1,3})$", RegexOptions.ECMAScript);

Dictionary<string, Stack<string>> InitialCrateSetup(List<string> startingBlueprint) {
    var crateStacks = new Dictionary<string, Stack<string>>();

    for (int i = startingBlueprint.Count - 1; i >= 0; i--) {
        var crates = startingBlueprint.ElementAt(i).Chunk(4);
        for (int j = 0; j < crates.Count(); j++) {
            var crate = string.Concat(crates.ElementAt(j)).Trim();

            if (!string.IsNullOrWhiteSpace(crate) && !crate.Contains('['))
                crateStacks.Add(crate, new Stack<string>()); 
            else if (crate.Contains('['))
                crateStacks[(j + 1).ToString()].Push(crate.Trim());
        }
    }
    
    return crateStacks;
}

string PartOne() {
    var crateStacks = new Dictionary<string, Stack<string>>();
    try {
        using (StreamReader reader = new StreamReader("./input.txt")) {
            line = reader.ReadLine();

            var startingStacks = new List<string>();
            while (line != null && !string.IsNullOrWhiteSpace(line)) {
                startingStacks.Add(line);
                line = reader.ReadLine();
            }

            crateStacks = InitialCrateSetup(startingStacks);
            while (line != null) {
                var match = movesRegex.Match(line);

                if (match.Success) {
                    var quantity = int.Parse(match.Groups["quantity"].ToString());
                    var start = match.Groups["start"].ToString();
                    var dest = match.Groups["dest"].ToString();

                    for (int i = quantity; i > 0; i--) {
                        var crateToMove = crateStacks[start].Pop();
                        crateStacks[dest].Push(crateToMove);
                    }
                }
                line = reader.ReadLine();
            }
        }
    } catch(Exception e) {
        Console.WriteLine($"Exception: {e.Message}");
    }

    var sb = new StringBuilder();
    foreach (var entry in crateStacks) {
        entry.Value.TryPop(out var topCrate);
        if (topCrate != null)
            sb.Append(topCrate[1]);
    }

    return sb.ToString();
}
string PartTwo() {
    var crateStacks = new Dictionary<string, Stack<string>>();
    try {
        using (StreamReader reader = new StreamReader("./input.txt")) {
            line = reader.ReadLine();

            var startingStacks = new List<string>();
            while (line != null && !string.IsNullOrWhiteSpace(line)) {
                startingStacks.Add(line);
                line = reader.ReadLine();
            }
            crateStacks = InitialCrateSetup(startingStacks);

            while (line != null) {
                var match = movesRegex.Match(line);
                if (match.Success) {
                    var quantity = int.Parse(match.Groups["quantity"].ToString());
                    var start = match.Groups["start"].ToString();
                    var dest = match.Groups["dest"].ToString();
                    var tempStack = new Stack<string>();

                    for (int i = quantity; i > 0; i--) {
                        var crateToMove = crateStacks[start].Pop();
                        tempStack.Push(crateToMove);
                    }

                    for (int j = 0; j < quantity; j++) {
                        var c = tempStack.Pop();
                        crateStacks[dest].Push(c);
                    }
                }

                line = reader.ReadLine();
            }
        }
    } catch(Exception e) {
        Console.WriteLine($"Exception: {e.Message}");
    }

    var sb = new StringBuilder();
    foreach (var entry in crateStacks) {
        entry.Value.TryPop(out var topCrate);
        if (topCrate != null)
            sb.Append(topCrate[1]);
    }

    return sb.ToString();
}

Console.WriteLine($"Part 1: {PartOne()}");
Console.WriteLine($"Part 2: {PartTwo()}");

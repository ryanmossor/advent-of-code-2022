using System.Text;

var charValues = new Dictionary<char, int>()
{
    { 'a', 1 },
    { 'b', 2 },
    { 'c', 3 },
    { 'd', 4 },
    { 'e', 5 },
    { 'f', 6 },
    { 'g', 7 },
    { 'h', 8 },
    { 'i', 9 },
    { 'j', 10 },
    { 'k', 11 },
    { 'l', 12 },
    { 'm', 13 },
    { 'n', 14 },
    { 'o', 15 },
    { 'p', 16 },
    { 'q', 17 },
    { 'r', 18 },
    { 's', 19 },
    { 't', 20 },
    { 'u', 21 },
    { 'v', 22 },
    { 'w', 23 },
    { 'x', 24 },
    { 'y', 25 },
    { 'z', 26 },
    { 'A', 27 },
    { 'B', 28 },
    { 'C', 29 },
    { 'D', 30 },
    { 'E', 31 },
    { 'F', 32 },
    { 'G', 33 },
    { 'H', 34 },
    { 'I', 35 },
    { 'J', 36 },
    { 'K', 37 },
    { 'L', 38 },
    { 'M', 39 },
    { 'N', 40 },
    { 'O', 41 },
    { 'P', 42 },
    { 'Q', 43 },
    { 'R', 44 },
    { 'S', 45 },
    { 'T', 46 },
    { 'U', 47 },
    { 'V', 48 },
    { 'W', 49 },
    { 'X', 50 },
    { 'Y', 51 },
    { 'Z', 52 },
};

int PartOne() {
    var matches = new List<char>();
    string line = string.Empty;

    try {
        using (StreamReader reader = new StreamReader("./input.txt")) {
            line = reader.ReadLine();

            while (line != null) {
                var halfStrLength = line.Length / 2;
                var firstCompartment = line.Substring(0, halfStrLength);
                var secondCompartment = line.Substring(halfStrLength);

                for (int i = 0; i < halfStrLength; i++) {
                    for (int j = 0; j < halfStrLength; j++) {
                        if (firstCompartment[i] == secondCompartment[j]) {
                            matches.Add(firstCompartment[i]);
                            goto MatchFound;
                        }
                    }
                }

                MatchFound:
                    line = reader.ReadLine();
            }
        }
    } catch(Exception e) {
        Console.WriteLine($"Exception: {e.Message}");
    }

    var total = matches.Sum(match => charValues[match]);
    return total;
}

int PartTwo() {
    var matches = new List<char>();
    string line = string.Empty;

    try {
        using (StreamReader reader = new StreamReader("./input.txt")) {
            line = reader.ReadLine();
            int counter = 0;
            var elfGroup = new StringBuilder();

            while (line != null) {
                counter++;
                elfGroup.AppendLine(line);
                if (counter % 3 == 0) {
                    var rucksacks = elfGroup.ToString().Split("\n");
                    for (int i = 0; i < rucksacks[0].Length; i++) {
                        for (int j = 0; j < rucksacks[1].Length; j++) {
                            for (int k = 0; k < rucksacks[2].Length; k++) {
                                if (rucksacks[0][i] == rucksacks[1][j] && rucksacks[0][i] == rucksacks[2][k]) {
                                    matches.Add(rucksacks[0][i]);
                                    goto MatchFound;
                                }
                            }
                        }
                    }

                    MatchFound:
                        elfGroup.Clear();
                }
                line = reader.ReadLine();
            }
        }
    } catch(Exception e) {
        Console.WriteLine($"Exception: {e.Message}");
    }

    var total = matches.Sum(match => charValues[match]);
    return total;
}

System.Console.WriteLine(PartOne());
System.Console.WriteLine(PartTwo());

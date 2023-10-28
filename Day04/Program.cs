int fullOverlapTotal = 0;
int partialOverlapTotal = 0;

try {
    string line = string.Empty;
    using (StreamReader reader = new StreamReader("./input.txt")) {
        line = reader.ReadLine();

        while (line != null) {
            var pairs = line.Split(',');
            var pair1 = pairs[0].Split('-');
            var pair2 = pairs[1].Split('-');

            var range1 = getRange(pair1);
            var range2 = getRange(pair2);

            IEnumerable<int> largerRange;
            IEnumerable<int> smallerRange;

            if (range1.Count() > range2.Count()) {
                largerRange = range1;
                smallerRange = range2;
            } else {
                largerRange = range2;
                smallerRange = range1;
            }

            var both = smallerRange.Intersect(largerRange);
            if (both.Count() == smallerRange.Count()) // Part 1
                fullOverlapTotal++;
            if (both.Count() > 0) // Part 2
                partialOverlapTotal++;

            line = reader.ReadLine();
        }
    }

    System.Console.WriteLine($"Part 1: {fullOverlapTotal}");
    System.Console.WriteLine($"Part 2: {partialOverlapTotal}");
} catch(Exception e) {
    Console.WriteLine($"Exception: {e.Message}");
}

IEnumerable<int> getRange(string[] rangeStr) {
    var rangeStart = int.Parse(rangeStr[0]);
    var rangeEnd = int.Parse(rangeStr[1]);
    var count = rangeEnd - rangeStart + 1;
    var range = Enumerable.Range(rangeStart, count);
    return range; 
}

var rows = new List<List<double>>();
var cols = new List<List<double>>();

var input = string.Empty;
try {
    using (StreamReader reader = new StreamReader("./input.txt"))
        input = reader.ReadToEnd();
} catch(Exception e) {
    Console.WriteLine(e.Message);
}

var lines = input.Split("\n");
for (int i = 0; i < lines.Length - 1; i++) {
    var row = lines[i].ToCharArray().Select(char.GetNumericValue).ToList();
    rows.Add(row);

    var col = new List<double>();
    for (int j = 0; j < lines.Length - 1; j++)
        col.Add(int.Parse(lines[j][i].ToString()));
    cols.Add(col);
}

Console.WriteLine($"Part 1: {PartOne()}");
Console.WriteLine($"Part 2: {PartTwo()}");

int PartOne() {
    int visibleTrees = 0;

    for (int i = 0; i < rows.Count; i++) {
        var currentRow = rows[i];
        for (int j = 0; j < cols.Count; j++) {
            var isPerimeterTree = i == 0 || i == rows.Count - 1 || j == 0 || j == cols.Count - 1;
            if (isPerimeterTree) {
                visibleTrees++;
                continue;
            }

            var currentCol = cols[j];
            var isVisibleHorizontally = IsVisible(currentRow, j);
            var isVisibleVertically = IsVisible(currentCol, i);
            if (isVisibleHorizontally || isVisibleVertically) 
                visibleTrees++;
        }
    }

    return visibleTrees;
}

int PartTwo() {
    int topScenicScore = 0;

    for (int i = 0; i < rows.Count; i++) {
        var currentRow = rows[i];
        for (int j = 0; j < cols.Count; j++) {
            var currentCol = cols[j];
            var visibleTreesToNorth = CountVisibleTreesBefore(currentCol, i);
            var visibleTreesToWest = CountVisibleTreesBefore(currentRow, j);
            var visibleTreesToEast = CountVisibleTreesAfter(currentRow, j);
            var visibleTreesToSouth = CountVisibleTreesAfter(currentCol, i);

            int scenicScore = visibleTreesToNorth * visibleTreesToWest * visibleTreesToEast * visibleTreesToSouth;
            if (scenicScore > topScenicScore)
                topScenicScore = scenicScore;
        }
    }

    return topScenicScore;
}

bool IsVisible(List<double> rowOrCol, int currentPosition) {
    var currentTree = rowOrCol[currentPosition];
    var isVisibleBefore = rowOrCol.Take(currentPosition).All(x => currentTree > x);
    var isVisibleAfter = rowOrCol.Skip(currentPosition + 1).All(x => currentTree > x);
    return isVisibleBefore || isVisibleAfter;
}

int CountVisibleTreesBefore(List<double> rowOrCol, int currentPosition) {
    var currentTree = rowOrCol[currentPosition];
    int visibleTrees = 0;
    var treesBefore = rowOrCol.Take(currentPosition).ToList();
    for (int i = treesBefore.Count - 1; i >= 0; i--) {
        visibleTrees++;
        if (treesBefore[i] >= currentTree)
            break;     
    }

    return visibleTrees;
}

int CountVisibleTreesAfter(List<double> rowOrCol, int currentPosition) {
    var currentTree = rowOrCol[currentPosition];
    int visibleTrees = 0;
    var treesAfter = rowOrCol.Skip(currentPosition + 1).ToList();
    for (int i = 0; i < treesAfter.Count; i++) {
        visibleTrees++;
        if (treesAfter[i] >= currentTree)
            break;     
    }

    return visibleTrees;
}

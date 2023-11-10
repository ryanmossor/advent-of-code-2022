using Solution;

var input = File.ReadAllLines("./input.txt");

int[,] heights = new int[input.Length, input[0].Length];
var startingPositions = new List<Position>();

for (int r = 0; r < input.Length; r++) {
    for (int c = 0; c < input[0].Length; c++) {
        char ch = input[r][c];
        if (ch == 'S' || ch == 'a') {
            heights[r,c] = 0;
            startingPositions.Add(new Position(r, c));
        }
        else if (ch == 'E')
            heights[r,c] = 'z' - 'a';
        else
            heights[r,c] = ch - 'a';
    }
}

string startingLine = input.First(x => x.Contains('S'));
int startingRow = Array.IndexOf(input, startingLine);
int startingCol = startingLine.IndexOf('S');

string endingLine = input.First(x => x.Contains('E'));
int endingRow = Array.IndexOf(input, endingLine);
int endingCol = endingLine.IndexOf('E');

var distances = new List<int>();

foreach (var startingPos in startingPositions) {
    var endingPos = new Position(endingRow, endingCol);
    var explorer = new Explorer(new Maze(heights), startingPos);

    while (explorer.ExplorationQueue.Count() > 0) {
        Position exploring = explorer.Explore();
        if (exploring == endingPos) {
            distances.Add(explorer.DistanceTo(exploring));
            break;
        }
    }
}

Console.WriteLine(distances.Order().First());

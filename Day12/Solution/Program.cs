using Solution;

var input = File.ReadAllLines("./input.txt");

int[,] heights = new int[input.Length, input[0].Length];
for (int r = 0; r < input.Length; r++) {
    for (int c = 0; c < input[0].Length; c++) {
        char ch = input[r][c];
        if (ch == 'S')
            heights[r,c] = 0;
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

var startingPos = new Position(startingRow, startingCol);
var endingPos = new Position(endingRow, endingCol);

var explorer = new Explorer(new Maze(heights), startingPos);

while (explorer.ExplorationQueue.Count() > 0) {
    Position exploring = explorer.Explore();
    if (exploring == endingPos) {
        Console.WriteLine(explorer.DistanceTo(exploring));
        break;
    }
}

var input = File.ReadAllLines("./input.txt");

System.Console.WriteLine($"Part 1: {CountTailVisitedPositions(2)}");
System.Console.WriteLine($"Part 2: {CountTailVisitedPositions(10)}");

int CountTailVisitedPositions(int ropeLength) {
    var rope = CreateRope(ropeLength);
    var head = rope[0];
    var tail = rope[^1];
    var tailVisitedPositions = new HashSet<string> { tail.ToString() };

    foreach (string line in input) {
        var instructions = line.Split(" ");
        var direction = instructions[0];
        var moves = Convert.ToInt32(instructions[1]);

        for (int i = 0; i < moves; i++) {
            head = MoveHeadPosition(head, direction);
            for (int j = 1; j < rope.Count; j++) {
                var previousKnot = rope[j - 1];
                var currentKnot = rope[j];
                currentKnot = MoveKnotPosition(currentKnot, previousKnot);
            }

            tailVisitedPositions.Add(tail.ToString());
        }
    }

    return tailVisitedPositions.Count;
}

List<Coords> CreateRope(int numberOfKnots) {
    var knots = new List<Coords>();
    for (int i = 0; i < numberOfKnots; i++)
        knots.Add(new Coords(0, 0));

    return knots;
}

Coords MoveKnotPosition(Coords currentKnot, Coords previousKnot) {
    var xDiff = currentKnot.X - previousKnot.X;
    var yDiff = currentKnot.Y - previousKnot.Y;

    if (Math.Abs(xDiff) > 1 || Math.Abs(yDiff) > 1) {
        previousKnot.X += Math.Sign(xDiff);
        previousKnot.Y += Math.Sign(yDiff);
    }

    return previousKnot;
}

Coords MoveHeadPosition(Coords headPosition, string direction) {
    if (direction.Equals("L"))
        headPosition.X -= 1;
    else if (direction.Equals("R"))
        headPosition.X += 1;
    else if (direction.Equals("U"))
        headPosition.Y += 1;
    else if (direction.Equals("D"))
        headPosition.Y -= 1;

    return headPosition;
}

class Coords {
    public int X { get; set; }
    public int Y { get; set; }

    public Coords(int x, int y) {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

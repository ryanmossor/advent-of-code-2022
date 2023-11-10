namespace Solution;

public class Explorer {
    public Maze Map { get; }
    public Position Start { get; }
    public Queue<Position> ExplorationQueue { get; private set; }
    public int[,] Distances;

    public Explorer(Maze map, Position start) {
        Map = map;
        Start = start; 
        Distances = new int[Map.Rows, Map.Cols];

        for (int r = 0; r < Map.Rows; r++) {
            for (int c = 0; c < Map.Cols; c++) {
                Distances[r,c] = -1;
            }
        }

        Distances[Start.Row, Start.Col] = 0;
        ExplorationQueue = new Queue<Position>();
        ExplorationQueue.Enqueue(Start);
    }

    public int DistanceTo(Position pos) => Distances[pos.Row, pos.Col];

    public Position Explore() {
        if (ExplorationQueue.Count == 0)
            throw new Exception("Nothing left to explore");

        Position pos = ExplorationQueue.Dequeue();
        var explorableNeighbors = Map.FindNeighbors(pos);

        foreach (Position neighbor in explorableNeighbors) {
            if (Distances[neighbor.Row, neighbor.Col] == -1) {
                Distances[neighbor.Row, neighbor.Col] = Distances[pos.Row, pos.Col] + 1;
                ExplorationQueue.Enqueue(neighbor);
            }
        }

        return pos;
    }
}

namespace Solution;

public record Maze(int[,] Heights) {
    public int Rows => Heights.GetLength(0);
    public int Cols => Heights.GetLength(1);

    public List<Position> FindNeighbors(Position pos) {
        var neighbors = pos.Neighbors();
        var result = new List<Position>();

        foreach (var neighbor in neighbors) {
            if (CheckValidMove(from: pos, to: neighbor))
                result.Add(neighbor);
        }

        return result;
    }

    public bool Contains(Position pos) {
        return pos.Row >= 0 
            && pos.Col >= 0 
            && pos.Row < Rows 
            && pos.Col < Cols;
    }

    public bool CheckValidMove(Position from, Position to) {
        if (!Contains(from) || !Contains(to))
            return false;

        int toHeight = Heights[to.Row, to.Col];
        int fromHeight = Heights[from.Row, from.Col];

        return toHeight <= fromHeight + 1;
    }
}

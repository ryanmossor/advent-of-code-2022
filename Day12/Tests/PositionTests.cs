using Solution;

namespace Tests;

public class PositionTests {
    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 3)]
    public void NeighborTests(int row, int col) {
        // arrange
        var pos = new Position(row, col);
        var expected = new List<Position>() {
            new Position(row - 1, col),
            new Position(row + 1, col),
            new Position(row, col + 1),
            new Position(row, col - 1),
        };
        
        // act
        var result = pos.Neighbors();

        // assert
        Assert.Equal(expected, result);
    }
}

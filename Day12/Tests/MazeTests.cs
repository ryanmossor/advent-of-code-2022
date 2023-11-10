using Solution;

namespace Tests;

public class MazeTests {
    [Fact]
    public void FindNeighborsAllDirections() {
        // arrange
        int[,] heights = {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 0, 0, 0 },
        };
        var map = new Maze(heights);
        var center = new Position(1, 1);
        var expected = new List<Position>() { center.North, center.South, center.East, center.West };

        // act
        var result = map.FindNeighbors(center);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindNeighborsNoDirections() {
        // arrange
        int[,] heights = {
            { 0, 2, 0 },
            { 2, 0, 2 },
            { 0, 2, 0 },
        };
        var map = new Maze(heights);
        var center = new Position(1, 1);

        // act
        var result = map.FindNeighbors(center);

        // assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindNeighborsDifferentHeights() {
        // arrange
        int[,] heights = {
            { 0, 9, 0 },
            { 3, 1, 2 },
            { 0, 0, 0 },
        };
        var map = new Maze(heights);
        var center = new Position(1, 1);
        var expected = new List<Position>() { center.South, center.East };

        // act
        var result = map.FindNeighbors(center);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindNeighborsCorners() {
        // arrange
        int[,] heights = {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0},
        };
        var map = new Maze(heights);

        var topLeft = new Position(0, 0);
        var topLeftExpected = new List<Position>() { topLeft.South, topLeft.East };

        var topRight = new Position(0, 2);
        var topRightExpected = new List<Position>() { topRight.South, topRight.West };

        var bottomLeft = new Position(2, 0);
        var bottomLeftExpected = new List<Position>() { bottomLeft.North, bottomLeft.East };

        var bottomRight = new Position(2, 2);
        var bottomRightExpected = new List<Position>() { bottomRight.North, bottomRight.West };

        var center = new Position(1, 1);
        var centerExpected = new List<Position>() { center.North, center.South, center.East, center.West };

        // act
        var topLeftNeighbors = map.FindNeighbors(topLeft);
        var topRightNeighbors = map.FindNeighbors(topRight);
        var bottomLeftNeighbors = map.FindNeighbors(bottomLeft);
        var bottomRightNeighbors = map.FindNeighbors(bottomRight);
        var centerNeighbors = map.FindNeighbors(center);

        // assert
        Assert.Equal(topRightExpected, topRightNeighbors);
        Assert.Equal(topLeftExpected, topLeftNeighbors);
        Assert.Equal(bottomLeftExpected, bottomLeftNeighbors);
        Assert.Equal(bottomRightExpected, bottomRightNeighbors);
        Assert.Equal(centerExpected, centerNeighbors);
    }
}

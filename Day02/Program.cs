Console.WriteLine($"First solution: {FirstHalf()}");
Console.WriteLine($"Second solution: {SecondHalf()}");

public static partial class Program
{
    public static Dictionary<string, int> OpponentMoveMap = new Dictionary<string, int>() 
    {
        { "A", 1 },
        { "B", 2 },
        { "C", 3 },
    };

    public static Dictionary<string, int> ScoreMap = new Dictionary<string, int>()
    {
        { "loss", 0 },
        { "draw", 3 },
        { "win", 6 }
    };

    public static int FirstHalf()
    {
        var myMoveMap = new Dictionary<string, int>() 
        {
            { "X", 1 },
            { "Y", 2 },
            { "Z", 3 },
        };

        int totalScore = 0;
        try {
            using (StreamReader reader = new StreamReader("./input.txt")) {
                string line;
                line = reader.ReadLine();

                while (line != null) {
                    var moves = line.Split(' ');
                    var opponentMove = OpponentMoveMap[moves[0]];
                    var myMove = myMoveMap[moves[1]];
                    string outcome = string.Empty;

                    var scissorsVsRock = myMove == 3 && opponentMove == 1;
                    var rockVsScissors = myMove == 1 && opponentMove == 3;

                    if (myMove == opponentMove)
                        outcome = "draw";
                    else if (scissorsVsRock)
                        outcome = "loss";
                    else if (rockVsScissors)
                        outcome = "win";
                    else if (opponentMove > myMove)
                        outcome = "loss";
                    else
                        outcome = "win";

                    totalScore += myMove + ScoreMap[outcome];
                    line = reader.ReadLine();
                }
            }
        } catch(Exception e) {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return totalScore;
    }

    public static int SecondHalf() {
        var myMoveMap = new Dictionary<string, int>() 
        {
            { "A", 1 },
            { "B", 2 },
            { "C", 3 },
        };

        var outcomeMap = new Dictionary<string, string>() 
        {
            { "X", "loss" },
            { "Y", "draw" },
            { "Z", "win" },
        };

        var winMap = new Dictionary<string, string>()
        {
            { "A", "B" },
            { "B", "C" },
            { "C", "A" },
        };

        var lossMap = new Dictionary<string, string>()
        {
            { "A", "C" },
            { "B", "A" },
            { "C", "B" },
        };

        int totalScore = 0;
        string line = string.Empty;
        try {
            using (StreamReader reader = new StreamReader("./input.txt")) {
                line = reader.ReadLine();

                while (line != null) {
                    var moves = line.Split(' ');
                    var opponentMove = OpponentMoveMap[moves[0]];
                    var desiredOutcome = outcomeMap[moves[1]];
                    string myMove = string.Empty;
                    
                    if (desiredOutcome == "draw")
                        myMove = moves[0];
                    else if (desiredOutcome == "win")
                        myMove = winMap[moves[0]];
                    else if (desiredOutcome == "loss")
                        myMove = lossMap[moves[0]];

                    var myMoveScore = myMoveMap[myMove];
                    totalScore += myMoveScore + ScoreMap[desiredOutcome];

                    line = reader.ReadLine();
                }
            }
        } catch(Exception e) {
            Console.WriteLine($"Exception: {e.Message} line: {line}");
        }

        return totalScore;
    }
}

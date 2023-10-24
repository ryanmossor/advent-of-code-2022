using System.Text;

var input = File.ReadAllLines("./input.txt");
int cycleCount = 0;
int register = 1;
int spritePosition = register;
var signalStrengths = new Dictionary<int, int>();
var sb = new StringBuilder();

foreach (var line in input) {
    var instructions = line.Split(" ");
    if (instructions[0] == "noop") {
        UpdateCycle();
    } else {
        int.TryParse(instructions[1], out int value);
        UpdateCycle();
        UpdateCycle(value);
    }
}

Console.WriteLine($"Part 1: {signalStrengths.Sum(x => x.Value)}");
Console.WriteLine($"Part 2: {sb}");

void UpdateCycle(int value = 0) {
    DrawPixel(sb);
    cycleCount += 1;
    if ((cycleCount + 20) % 40 == 0) {
        var signalStrength = register * cycleCount;
        signalStrengths.Add(cycleCount, signalStrength);
    }

    register += value;
    spritePosition = register;
}

void DrawPixel(StringBuilder sb) {
    var currentPosition = cycleCount % 40;
    if (currentPosition == 0)
        sb.AppendLine();

    var startOfSprite = spritePosition - 1;
    var endOfSprite = spritePosition + 1;

    if (currentPosition >= startOfSprite && currentPosition <= endOfSprite)
        sb.Append('#');
    else
        sb.Append('.');
}

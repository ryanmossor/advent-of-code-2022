﻿using System.Text.RegularExpressions;
using Day11;

var input = File.ReadAllText("./input.txt");
var monkeyStats = input.Split("\n\n");

var regex = new Regex(@"^Monkey (?<monkeyId>\d+):\n\s*Starting items: (?<startingItems>\d+(,\s\d+)*)\n\s*Operation: new = old (?<operator>[\+\-\*\/]) (?<operationValue>\d+|old)\n\s*Test: divisible by (?<divisor>\d+)\n\s*If true: throw to monkey (?<trueTarget>\d+)\n\s*If false: throw to monkey (?<falseTarget>\d+)$", RegexOptions.ECMAScript);

System.Console.WriteLine($"Part 1: {PlayKeepAway(20, allowRelief: true)}");
System.Console.WriteLine($"Part 2: {PlayKeepAway(10_000, allowRelief: false)}");

List<Monkey> Init() {
    var monkeys = new List<Monkey>();
    foreach (var stat in monkeyStats) {
        var match = regex.Match(stat);
        var monkeyId = Convert.ToInt32(match.Groups["monkeyId"].Value);

        var items = match.Groups["startingItems"].Value
            .ToString()
            .Split(", ")
            .Select(i => Convert.ToInt32(i))
            .ToList();
        var startingQueue = new Queue<long>();
        foreach (var item in items)
            startingQueue.Enqueue(item);

        var op = match.Groups["operator"].Value == "+"
            ? Operation.Add
            : Operation.Multiply;

        var operationValue = match.Groups["operationValue"].Value == "old"
            ? -1
            : Convert.ToInt32(match.Groups["operationValue"].Value);

        var divisor = Convert.ToInt32(match.Groups["divisor"].Value);
        var trueTarget = Convert.ToInt32(match.Groups["trueTarget"].Value);
        var falseTarget = Convert.ToInt32(match.Groups["falseTarget"].Value);

        var monkey = new Monkey(monkeyId, startingQueue, op, divisor, trueTarget, falseTarget, operationValue);
        monkeys.Add(monkey);
    }

    return monkeys;
}

long PlayKeepAway(int rounds, bool allowRelief) {
    var monkeys = Init();
    int lcm = monkeys.Select(x => x.Divisor).Aggregate((s, a) => s * a);

    for (int i = 0; i < rounds; i++) {
        foreach (var monkey in monkeys) {
            while (monkey.Items.Count() > 0)
                monkey.TransferItem(monkeys, allowRelief, lcm);
        }
    }

    var topTwoProduct = monkeys
        .OrderByDescending(x => x.InspectionCount)
        .Take(2)
        .Select(m => m.InspectionCount)
        .Aggregate((s, a) => s * a);

    return topTwoProduct;
}

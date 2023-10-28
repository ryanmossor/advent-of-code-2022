namespace Day11 {
    public class Monkey {
        public int MonkeyId { get; private set; }
        public Queue<long> Items { get; private set; }
        public long WorryLevel { get; private set; }
        public Operation MathOperation { get; private set; }
        public int OperationValue { get; private set; }
        public int Divisor { get; private set; }
        public int TrueTarget { get; private set; }
        public int FalseTarget { get; private set; }
        public long InspectionCount { get; private set; }

        public Monkey(int monkeyId, Queue<long> startingItems, Operation operation, int divisor, int trueTarget, int falseTarget, int operationValue = -1) {
            MonkeyId = monkeyId;
            Items = startingItems;
            WorryLevel = 0;
            MathOperation = operation;
            OperationValue = operationValue;
            Divisor = divisor;
            TrueTarget = trueTarget;
            FalseTarget = falseTarget;
            InspectionCount = 0;
        }

        public void TransferItem(List<Monkey> monkeys, bool allowRelief, int lcm) {
            var item = Items.Dequeue();
            WorryLevel = item;
            InspectionCount++;
            WorryLevel = DoMath(lcm, allowRelief);

            var targetMonkey = WorryLevel % Divisor == 0
                ? monkeys[TrueTarget]
                : monkeys[FalseTarget];

            targetMonkey.ReceiveItem(WorryLevel);
        }

        public long DoMath(int lcm, bool allowRelief) {
            long val = OperationValue;
            if (OperationValue == -1)
                val = WorryLevel;

            long result;
            if (MathOperation == Operation.Add)
                result = WorryLevel + val;
            else
                result = WorryLevel * val;

            if (allowRelief)
                return (int)Math.Floor((double)(result / 3));
            else
                return result % lcm;
        }

        public void ReceiveItem(long itemWorryLevel) => Items.Enqueue(itemWorryLevel);
    }
}

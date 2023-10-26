namespace Day11 {
    public class Monkey {
        public int MonkeyId { get; private set; }
        public Queue<int> Items { get; private set; }
        public int WorryLevel { get; private set; }
        public Operation MathOperation { get; private set; }
        public int OperationValue { get; private set; }
        public int Divisor { get; private set; }
        public int TrueTarget { get; private set; }
        public int FalseTarget { get; private set; }
        public long InspectionCount { get; private set; }

        public Monkey(int monkeyId, Queue<int> startingItems, Operation operation, int divisor, int trueTarget, int falseTarget, int operationValue = -1) {
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

        public void TransferItem(List<Monkey> monkeys) {
            var item = Items.Dequeue();
            WorryLevel = item;
            InspectionCount++;
            WorryLevel = DoMath();
            WorryLevel = SweetRelief();

            var targetMonkey = WorryLevel % Divisor == 0
                ? monkeys[TrueTarget]
                : monkeys[FalseTarget];

            targetMonkey.ReceiveItem(WorryLevel);
        }

        public int DoMath() {
            var val = OperationValue;
            if (OperationValue == -1)
                val = WorryLevel;

            if (MathOperation == Operation.Add)
                return WorryLevel + val;
            else
                return WorryLevel * val;
        }

        public int SweetRelief() {
            double relievedWorryLevel = WorryLevel / 3;
            return (int)Math.Floor(relievedWorryLevel);
        }

        public void ReceiveItem(int itemWorryLevel) {
            Items.Enqueue(itemWorryLevel);
        }
    }
}

const int TOTAL_DISK_SPACE = 70_000_000;

string line;
try {
    using (StreamReader reader = new StreamReader("./input.txt")) {
        line = reader.ReadLine();
        Node rootDir = new Node(name: "/", parent: null, filesize: 0);
        Node cwd = rootDir;
        while (line != null) {
            var words = line.Split(" ");

            if (line.Equals("$ cd /") || line.StartsWith("$ ls") || line.StartsWith("dir")) {
                line = reader.ReadLine();
                continue;
            } else if (line.Equals("$ cd ..")) {
                cwd = cwd.Parent;
            } else if (line.StartsWith("$ cd")) {
                var newDir = new Node(name: words[2], parent: cwd, filesize: 0);
                cwd.AddChild(newDir);
                cwd = newDir;
            } else {
                var filesize = words[0];
                var filename = words[1];
                int.TryParse(filesize, out var size);
                if (size != null && size != 0) {
                    var file = new Node(name: filename, parent: cwd, filesize: size);
                    cwd.AddChild(file);
                }
            }

            line = reader.ReadLine();
        }

        rootDir.SumChildren();
        Console.WriteLine($"Part 1: {rootDir.GetDirsLessThan(100_000)}");
        var currentSpace = TOTAL_DISK_SPACE - rootDir.Filesize;
        int remainingSpaceRequired = 30_000_000 - currentSpace;
        Console.WriteLine($"Part 2: {rootDir.GetSmallestDirToFreeSpace(remainingSpaceRequired, new List<int>())}");
    }
} catch(Exception e) {
    Console.WriteLine(e.Message);
}

class Node {
    public string Name { get; set; }
    public Node? Parent { get; set; }
    public List<Node> Children { get; set; }
    public int Filesize { get; set; }
    public bool IsDirectory { get; set; }

    public Node(string name, Node? parent, int? filesize)
    {
        Name = name;
        Parent = parent;
        Children = new List<Node>();
        Filesize = filesize ?? 0;
    }

    public void AddChild(Node node) {
        if (!Children.Contains(node)) {
            node.Parent = this;
            Children.Add(node);
        }
    }

    public int SumChildren() {
        if (Children.Count == 0)
            return Filesize;

        int total = 0;
        foreach (var child in Children)
            total += child.SumChildren();

        Filesize += total;
        return total;
    }

    public int GetDirsLessThan(int maxSize) {
        if (Children.Count == 0)
            return 0;

        int total = 0;
        if (Filesize <= maxSize)
            total += Filesize;

        foreach (var child in Children)
            total += child.GetDirsLessThan(maxSize);
        
        return total;
    }

    public int GetSmallestDirToFreeSpace(int remainingSpaceRequired, List<int> eligibleDirs) {
        if (Children.Count == 0)
            return 0;

        if (Filesize >= remainingSpaceRequired)
            eligibleDirs.Add(Filesize);

        foreach (var child in Children)
            child.GetSmallestDirToFreeSpace(remainingSpaceRequired, eligibleDirs);

        return eligibleDirs.Min();
    }
}

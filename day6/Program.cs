string dataBuffer;

try {
    using (StreamReader reader = new StreamReader("./input.txt"))
        dataBuffer = reader.ReadToEnd();
    
    Console.WriteLine($"Packet start: {searchBufferForStart(dataBuffer, BufferMarker.StartOfPacket)}");
    Console.WriteLine($"Message start: {searchBufferForStart(dataBuffer, BufferMarker.StartOfMessage)}");
} catch(Exception e) {
    Console.WriteLine(e.Message);
}

int searchBufferForStart(string buffer, BufferMarker marker) {
    for (int i = 0; i < buffer.Length; i++) {
        var sub = buffer.Substring(i, (int)marker);
        if (sub.Distinct().ToArray().Count() == (int)marker)
            return i + sub.Length;
    }

    return 0;
}

enum BufferMarker {
    StartOfPacket = 4,
    StartOfMessage = 14
}


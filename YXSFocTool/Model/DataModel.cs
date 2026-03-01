namespace YXSFocTool.Model;

public enum DataType
{
    UInt8 = 0,
    Int8,
    UInt16,
    Int16,
    UInt32,
    Int32,
    UInt64,
    Int64,
    Float,
    Double,
}

public readonly record struct DataInfo(DataType Type, string Name, int ID)
{
    public DataType Type { get; } = Type;

    public string Name { get; } = Name;

    public int ID { get; } = ID;

    public readonly int Size => Type switch
    {
        DataType.UInt8 => sizeof(byte),
        DataType.Int8 => sizeof(sbyte),
        DataType.UInt16 => sizeof(ushort),
        DataType.Int16 => sizeof(short),
        DataType.UInt32 => sizeof(uint),
        DataType.Int32 => sizeof(int),
        DataType.UInt64 => sizeof(ulong),
        DataType.Int64 => sizeof(long),
        DataType.Float => sizeof(float),
        DataType.Double => sizeof(double),
        _ => throw new NotSupportedException($"Unsupported data type: {Type}")
    };
}


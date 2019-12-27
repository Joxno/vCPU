namespace Core.DTO
{
    public class OperationDefinition
    {
        public byte OpCode { get; private set; } = 0;
        public int Size { get; private set; } = 0;

        public OperationDefinition(byte Code, int DataSize)
        {
            OpCode = Code;
            Size = DataSize;
        }
    }
}

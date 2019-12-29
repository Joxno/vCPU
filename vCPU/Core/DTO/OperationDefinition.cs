namespace Core.DTO
{
    public class OperationDefinition
    {
        public byte OpCode { get; } = 0;
        public int DataSize { get; } = 0;

        public OperationDefinition(byte Code, int DataSize)
        {
            OpCode = Code;
            this.DataSize = DataSize;
        }
    }
}

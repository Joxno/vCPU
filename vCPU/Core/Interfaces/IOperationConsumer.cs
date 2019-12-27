namespace Core.Interfaces
{
    public interface IOperationConsumer
    {
        void Consume(IOperation Operation);
    }
}

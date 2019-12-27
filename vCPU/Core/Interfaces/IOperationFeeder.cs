namespace Core.Interfaces
{
    public interface IOperationFeeder
    {
        void AddConsumer(IOperationConsumer Consumer);
        void Feed();
    }
}

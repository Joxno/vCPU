namespace Core.Interfaces
{
    public interface ICPU
    { 
        int ExecutedOperations { get; }
        int QueuedOperations { get; }

        void ExecuteOperation(IOperation Operation);
        void QueueOperation(IOperation Operation);
        void Execute();
    }
}

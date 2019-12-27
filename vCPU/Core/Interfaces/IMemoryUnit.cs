namespace Core.Interfaces
{
    public interface IMemoryUnit
    {
        int Size { get; }
        void Store<T>(T Value) where T : struct;
        T Load<T>() where T : struct;
    }
}

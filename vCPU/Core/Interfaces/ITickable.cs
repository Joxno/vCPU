namespace Core.Interfaces
{
    public interface ITickable
    {
        int Ticks { get; }
        bool IsSuspended { get; }
        void Tick();
        void ForceTick();
        void Suspend();
        void Resume();
    }
}

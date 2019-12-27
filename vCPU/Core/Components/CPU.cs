using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Components
{
    public class CPU : ICPU, ITickable
    {
        private readonly Queue<IOperation> m_OperationQueue = new Queue<IOperation>();

        public int Ticks { get; private set; } = 0;
        public bool IsSuspended { get; private set; } = false;
        public int ExecutedOperations { get; private set; } = 0;
        public int QueuedOperations => m_OperationQueue.Count;

        public void ExecuteOperation(IOperation Operation)
        {
            Operation.Execute();
            ExecutedOperations++;
        }

        public void Tick()
        {
            _DequeueAndExecuteOperation();
            Ticks++;
        }

        public void ForceTick()
        {
            Execute();
            Ticks++;
        }

        public void QueueOperation(IOperation Operation)
        {
            m_OperationQueue.Enqueue(Operation);
        }

        public void Execute()
        {
            if (_CanDequeue())
                ExecuteOperation(_RetrieveNextOperation());
        }

        private void _DequeueAndExecuteOperation()
        {
            if (_CanExecute())
                ExecuteOperation(_RetrieveNextOperation());
        }

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Resume()
        {
            IsSuspended = false;
        }

        private bool _CanExecute()
        {
            return 
                m_OperationQueue.Count > 0 &&
                !IsSuspended;
        }

        private bool _CanDequeue()
        {
            return m_OperationQueue.Count > 0;
        }

        private IOperation _RetrieveNextOperation()
        {
            return m_OperationQueue.Dequeue();
        }
    }
}

using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Components
{
    public class CPU : ICPU
    {
        private Queue<IOperation> m_OperationQueue = new Queue<IOperation>();
        private bool m_Suspended = false;

        public int Ticks { get; private set; } = 0;
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
            if(_CanDequeue())
                ExecuteOperation(_RetrieveNextOperation());
            Ticks++;
        }

        public void QueueOperation(IOperation Operation)
        {
            m_OperationQueue.Enqueue(Operation);
        }

        private void _DequeueAndExecuteOperation()
        {
            if (_CanExecute())
                ExecuteOperation(_RetrieveNextOperation());
        }

        public void Suspend()
        {
            m_Suspended = true;
        }

        public void Resume()
        {
            m_Suspended = false;
        }

        private bool _CanExecute()
        {
            return 
                m_OperationQueue.Count > 0 &&
                !_IsSuspended();
        }

        private bool _CanDequeue()
        {
            return m_OperationQueue.Count > 0;
        }

        private bool _IsSuspended()
        {
            return m_Suspended;
        }

        private IOperation _RetrieveNextOperation()
        {
            return m_OperationQueue.Dequeue();
        }
    }
}

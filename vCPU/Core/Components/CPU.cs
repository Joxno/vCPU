﻿using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public class CPU : ICPU
    {
        private bool m_Suspended = false;
        private int m_Ticks = 0;
        public int Ticks => m_Ticks;

        private int m_ExecutedOperations = 0;
        public int ExecutedOperations => m_ExecutedOperations;

        public int QueuedOperations => m_OperationQueue.Count;
        private Queue<IOperation> m_OperationQueue = new Queue<IOperation>();

        public void ExecuteOperation(IOperation Operation)
        {
            m_ExecutedOperations++;
        }

        public void Tick()
        {
            _DequeueAndExecuteOperation();
            m_Ticks++;
        }

        public void QueueOperation(IOperation Operation)
        {
            m_OperationQueue.Enqueue(Operation);
        }

        private void _DequeueAndExecuteOperation()
        {
            if (_CanExecute())
            {
                var t_Operation = m_OperationQueue.Dequeue();
                m_ExecutedOperations++;
            }
        }

        public void Suspend()
        {
            m_Suspended = true;
        }

        private bool _CanExecute()
        {
            return 
                m_OperationQueue.Count > 0 &&
                !_IsSuspended();
        }

        private bool _IsSuspended()
        {
            return m_Suspended;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace CoreTests.Mocks
{
    public class MockOperationConsumer : IOperationConsumer
    {
        public int OperationsCount => Operations.Count;
        public List<IOperation> Operations { get; set; } = new List<IOperation>();

        public void Consume(IOperation Operation)
        {
            Operations.Add(Operation);
        }
    }
}

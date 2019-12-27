using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Components
{
    public class OperationFeeder : IOperationFeeder
    {
        private List<IOperationConsumer> m_Consumers = new List<IOperationConsumer>();
        private IOperationFetcher m_Fetcher = null;

        public OperationFeeder(IOperationFetcher Fetcher)
        {
            m_Fetcher = Fetcher;
        }

        public void AddConsumer(IOperationConsumer Consumer)
        {
            m_Consumers.Add(Consumer);
        }

        public void Feed()
        {
            var t_Operation = _FetchOperation();
            foreach(var t_Consumer in m_Consumers)
                t_Consumer.Consume(t_Operation);
        }

        private IOperation _FetchOperation()
        {
            return m_Fetcher.FetchOperation();
        }
    }
}

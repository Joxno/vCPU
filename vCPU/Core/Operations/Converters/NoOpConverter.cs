using Core.DTO;
using Core.Interfaces;

namespace Core.Operations.Converters
{
    public class NoOpConverter : IOperationConverter
    {
        public IOperation Convert(OperationDTO DTO)
        {
            return new NoOp();
        }
    }
}

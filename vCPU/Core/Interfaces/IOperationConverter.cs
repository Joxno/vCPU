using Core.DTO;

namespace Core.Interfaces
{
    public interface IOperationConverter
    {
        IOperation Convert(OperationDTO DTO);
    }
}

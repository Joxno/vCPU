using Core.DTO;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

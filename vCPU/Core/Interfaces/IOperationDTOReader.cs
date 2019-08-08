﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationDTOReader
    {
        OperationDTO ReadMemory(MemoryAddress Address, IMemoryBank Bank);
        bool CanRead(byte OpCode);
    }
}
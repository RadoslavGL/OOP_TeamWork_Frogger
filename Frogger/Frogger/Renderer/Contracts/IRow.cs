﻿using Frogger.Renderer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger.Renderer.Contracts
{
    public interface IRow : IRowID
    {
        int FrogX { get; }
        bool HasFrog { get; }
    }
}

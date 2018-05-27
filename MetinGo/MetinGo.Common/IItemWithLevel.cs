using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.Common
{
    public interface IItemWithLevel
    {
        IItem Item { get; }
        int Level { get; }
    }
}

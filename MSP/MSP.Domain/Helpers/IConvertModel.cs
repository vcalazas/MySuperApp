using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Helpers
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert { get; }
    }
}

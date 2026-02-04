using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MSP.Domain.Helpers
{
    public interface IConvertModel<TTarget>
    {
        TTarget Convert();
    }
}

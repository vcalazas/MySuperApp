using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MSP.Domain.Helpers
{
    public interface IConvertModel<TSource, TTarget>
    {
        [JsonIgnore]
        TTarget Convert { get; }
    }
}

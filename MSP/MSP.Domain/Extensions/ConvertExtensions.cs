using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSP.Domain.Extensions
{
    public static class ConvertExtensions
    {
        public static IEnumerable<TTarget> ConvertAll<TTarget>(
            this IEnumerable<IConvertModel<TTarget>> values)
        {
            var result = new List<TTarget>();
            if (values == null) return null;
            foreach (var v in values)
            {
                result.Add(v.Convert());
            }
            return result.AsEnumerable();
        }
    }
}

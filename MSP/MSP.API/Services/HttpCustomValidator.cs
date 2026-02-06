using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.Domain.DTOs;
using Newtonsoft.Json.Linq;

namespace MSP.API.Services
{

    public class HttpCustomValidator
    {
        public static async Task<ActionResult> IsValidListAsync<T>(Func<Task<IEnumerable<T>>> predicate) where T : MSPBaseEntityDTO, new()
        {
            try
            {
                IEnumerable<T>? response = await predicate().ConfigureAwait(false);
                return new ObjectResult(response)
                {
                    StatusCode = response != null && response.Count() > 0 ? 200 : 201
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
        public static async Task<ActionResult> IsValidAsync<T>(Func<Task<T>> predicate) where T : MSPBaseEntityDTO, new()
        {
            try
            {
                T response = await predicate().ConfigureAwait(false);
                return new ObjectResult(response)
                {
                    StatusCode = string.IsNullOrEmpty(response.ErrorMessage) ? 200 : 422
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}

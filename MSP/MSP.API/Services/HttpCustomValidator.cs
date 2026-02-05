using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.Domain.DTOs;
using Newtonsoft.Json.Linq;

namespace MSP.API.Services
{
    public class HttpCustomValidator
    {
        public static async Task<ActionResult> IsValidAsync<T>(Func<Task<T>> predicate) where T : MSPBaseEntityDTO, new()
        {
            //if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            try
            {
                T response = await predicate().ConfigureAwait(false);
                return new ObjectResult(response)
                {
                    StatusCode = string.IsNullOrEmpty(response.ErrorMessage) ? 200 : 500
                };
            }
            catch (Exception ex) {
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}

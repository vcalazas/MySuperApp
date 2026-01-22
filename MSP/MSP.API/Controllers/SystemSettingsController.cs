using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MSP.API.Controllers
{
    public class SystemSettingsController : Controller
    {
        //[Authorize]
        [HttpGet]
        //[SwaggerOperation(
        //    Summary = "Creates a new product",
        //    Description = "Requires admin privileges",
        //    OperationId = "CreateProduct",
        //    Tags = ["Purchase", "Products"]
        //)]
        //[Obsolete("This endpoint is deprecated. Use the new endpoint instead.")]
        public IEnumerable<WeatherForecast> Get()
        {
            return ;
        }
    }
}

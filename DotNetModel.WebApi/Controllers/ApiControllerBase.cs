using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNetModel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase: ControllerBase
    {
        private readonly ILogger<ApiControllerBase> _logger;

        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> Return<T>(Func<T> func)
        {
            try
            {
                var res = await Task.Run(() => func());
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

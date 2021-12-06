using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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

        protected IActionResult Return<T>(Func<T> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

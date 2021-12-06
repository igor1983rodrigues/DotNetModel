using DotNetModel.Business;
using DotNetModel.DataEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetModel.WebApi.Controllers
{
    public class ApplicationController : ApiControllerBase
    {
        private readonly IApplicationBusiness applicationBusiness;

        public ApplicationController(ILogger<ApplicationController> logger, IApplicationBusiness applicationBusiness) : base(logger)
        {
            this.applicationBusiness = applicationBusiness;
        }

        [HttpGet]
        public IActionResult Find(string url, string pathLocal, bool? debuggingMode) => Return(() =>
    {
        return applicationBusiness.Find(url, pathLocal, debuggingMode);
    });

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => Return(() =>
        {
            return applicationBusiness.GetById(id);
        });

        [HttpPost]
        public IActionResult Add([FromBody] Application body) => Return(() =>
        {
            return new
            {
                Message = "Applications added!",
                Entity = applicationBusiness.Add(body)
            };
        });

        [HttpPatch("{id:int}")]
        public IActionResult Alter(int id, [FromBody] Application body) => Return(() =>
        {
            return new
            {
                Message = "Application modified!",
                Entity = applicationBusiness.Alter(id, body)
            };
        });

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id) => Return(() =>
        {
            applicationBusiness.Delete(id);
            return new { Message = "Application removed!", };
        });
    }
}

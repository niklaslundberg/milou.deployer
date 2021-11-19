using Arbor.AppModel.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Milou.Deployer.Web.IisHost.Areas.Startup
{
    [AllowAnonymous]
    [Area(StartupConstants.AreaName)]
    public class StartupController : Controller
    {
        [Route("~/startup")]
        [HttpGet]
        public IActionResult Index([FromServices] StartupTaskContext startupTaskContext)
        {
            if (startupTaskContext.IsCompleted)
            {
                return Redirect("/");
            }

            return View();
        }
    }
}
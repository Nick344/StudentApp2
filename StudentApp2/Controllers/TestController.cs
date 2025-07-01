using Microsoft.AspNetCore.Mvc;

namespace StudentApp2.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping() => Ok("pong");
    }

}

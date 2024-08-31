using Microsoft.AspNetCore.Mvc;

namespace StudentApp2.Controllers
{
    public class TestController : ControllerBase
    {

        [HttpGet("throw")]
        public IActionResult ThrowError()
        {
            throw new Exception("Test exception");
        }
    }
}

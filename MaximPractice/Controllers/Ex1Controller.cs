using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MaximPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ex1Controller : ControllerBase
    {
        private ILogger<Ex1Controller> _logger;

        public Ex1Controller(ILogger<Ex1Controller> logger)
        {
            _logger = logger;
        }
    }
}

using Elsa.Activities.Signaling.Services;
using Microsoft.AspNetCore.Mvc;
using Solutions.Now.CMIS2.Elsa.Models;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionTendersController : ControllerBase
    {
        ISignaler _signaler;
        public ExecutionTendersController(ISignaler signaler)
        {
            _signaler = signaler;
        }

        [Route("Request/{id}")]
        [HttpGet]
        public async Task<IActionResult> Requset(int id)
        {
            OutputActivityData data = new OutputActivityData { requestSerial = id };
            await _signaler.TriggerSignalAsync("ExecutionTender", input:data);
            return Ok();
        }
    }
}

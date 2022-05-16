using Elsa.Activities.Signaling.Services;
using Elsa.Attributes;
using Elsa.Models;
using Elsa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutions.Now.CMIS2.Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Now.CMIS2.Elsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowsController : ControllerBase
    {
        ISignaler _signaler;
        public WorkFlowsController(ISignaler signaler)
        {
            _signaler = signaler;
        }

        [Route("Request/{workflowName}/{id}")]
        [HttpGet]
        public async Task<IActionResult> Requset(string workflowName,int id)
        {
            OutputActivityData data = new OutputActivityData { requestSerial = id };
            await _signaler.TriggerSignalAsync(workflowName, input:data);
            return Ok();
        }
    }
}

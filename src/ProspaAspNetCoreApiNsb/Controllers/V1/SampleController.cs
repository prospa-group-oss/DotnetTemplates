using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using ProspaAspNetCoreApiNsb.Routing;
using V1.Commands;

namespace ProspaAspNetCoreApiNsb.Controllers.V1
{
    [ApiController]
    [V1, VersionedRoute("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IMessageSession _messageSession;

        public SampleController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet(Name = "Get")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Get()
        {
            await _messageSession.Send(new SampleCommand { Id = Guid.NewGuid() });

            return Ok("sample");
        }

        // [Authorize(Constants.Auth.Policies.WritePolicy)]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult Put()
        {
            return Ok();
        }
    }
}

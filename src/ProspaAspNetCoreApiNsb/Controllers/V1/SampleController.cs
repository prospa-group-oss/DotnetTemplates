﻿using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProspaAspNetCoreApiNsb.Routing;

namespace ProspaAspNetCoreApiNsb.Controllers.V1
{
    [ApiController]
    [V1, VersionedRoute("[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet(Name = "Get")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            return Ok("sample");
        }

        [Authorize(Constants.Auth.Policies.WritePolicy)]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult Put()
        {
            return Ok();
        }
    }
}

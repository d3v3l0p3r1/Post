using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Post.Server.Web.Controllers
{
    [Route("/")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// Receive message from clients
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMessage(string message)
        {
            return Ok();
        }
    }
}
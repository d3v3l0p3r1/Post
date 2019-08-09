using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Server.Services.Abstract;

namespace Post.Server.Web.Controllers
{
    [Route("/")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Receive message from clients
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMessage(string message)
        {
            try
            {
                await _messageService.CreateMessage(message, "");

                return Ok();
            }
            catch (Exception error)
            {
                return StatusCode(500, error.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Get messages list
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="take">items on page</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/list")]
        public IActionResult Get(int page = 0, int take = 10)
        {
            var list = _messageService.Get(page, take);

            return Ok(list);
        }
    }
}
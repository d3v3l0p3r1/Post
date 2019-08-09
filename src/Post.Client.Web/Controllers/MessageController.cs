using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Client.Services.Abstract;

namespace Post.Client.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Send message to server
        /// </summary>
        /// <param name="message">Message text</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            try
            {
                await _messageService.CreateMessage(message);

                return Ok();
            }
            catch (Exception errror)
            {
                return StatusCode(500, errror.GetBaseException().Message);
            }            
        }
    }
}
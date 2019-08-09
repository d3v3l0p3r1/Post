using Post.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Server.Services.Abstract
{
    public interface IMessageService
    {
        Task CreateMessage(string message, string ip);

        List<ServerMessage> Get(int page = 0, int count = 10);
    }
}

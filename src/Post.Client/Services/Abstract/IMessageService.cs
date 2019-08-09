using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Client.Services.Abstract
{
    public interface IMessageService
    {
        Task CreateMessage(string message);

        Task<int> ProccessInvalidMessages();
    }
}

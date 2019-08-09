using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Base.Repositories.Abstract;
using Post.Base.Services.Concrete;
using Post.Server.Contexts;
using Post.Server.Entities;
using Post.Server.Services.Abstract;

namespace Post.Server.Services.Concrete
{
    public class MessageService : BaseService<ServerMessage, ServerContext>, IMessageService
    {
        public MessageService(IBaseRepository<ServerMessage, ServerContext> repository) : base(repository)
        {

        }

        public async Task CreateMessage(string message, string ip)
        {
            var serverMessage = new ServerMessage()
            {
                Address = ip,
                Date = DateTime.UtcNow,
                Text = message
            };

            await CreateAsync(serverMessage);
        }

        public List<ServerMessage> Get(int page = 0, int count = 10)
        {
            if (page < 0)
            {
                page = 0;
            }

            if (count < 0 || count > 100)
            {
                count = 10;
            }

            var list = Repository.GetAll()
                .OrderByDescending(x => x.ID)
                .Skip(page * count)
                .Take(count).ToList();

            return list;
        }
    }
}

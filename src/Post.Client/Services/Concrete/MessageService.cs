using Microsoft.EntityFrameworkCore;
using Post.Base.Repositories.Abstract;
using Post.Base.Services.Concrete;
using Post.Client.Contexts;
using Post.Client.Entities;
using Post.Client.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Post.Client.Services.Concrete
{
    public class MessageService : BaseService<ClientMessage, ClientContext>, IMessageService
    {
        private readonly IHttpClientWrapper _httpClient;

        public MessageService(IBaseRepository<ClientMessage, ClientContext> repository, IHttpClientWrapper httpClient) : base(repository)
        {
            _httpClient = httpClient;
        }

        public async Task CreateMessage(string message)
        {
            var clientMessage = new ClientMessage()
            {
                Date = DateTime.UtcNow,
                Text = message
            };

            await CreateAsync(clientMessage);

            var isComplete = await _httpClient.SendMessage(message);

            clientMessage.State = isComplete ? MessageState.Complete : MessageState.Error;

            await UpdateAsync(clientMessage);
        }

        public async Task<int> ProccessInvalidMessages()
        {
            int total = 0;
            int page = 0;
            const int itemsOnPage = 10;

            var query = Repository.GetAll()
                .Where(x => x.State == MessageState.Error)
                .OrderBy(x => x.ID);

            List<ClientMessage> list = null;
            do
            {
                list = await query.Skip(page * itemsOnPage).Take(itemsOnPage).ToListAsync();

                foreach (var message in list)
                {
                    try
                    {
                        var result = await _httpClient.SendMessage(message.Text);
                        if (result)
                        {
                            message.State = MessageState.Complete;
                            await UpdateAsync(message);
                            total++;
                        }
                    }
                    catch
                    {
                        //log
                    }
                }
                page++;
            }
            while (list.Any());

            return total;
        }
    }
}

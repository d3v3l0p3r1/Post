using System.Threading.Tasks;

namespace Post.Client.Services.Abstract
{
    public interface IHttpClientWrapper
    {
        Task<bool> SendMessage(string message);
    }
}
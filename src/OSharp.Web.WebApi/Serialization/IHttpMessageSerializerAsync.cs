using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace OSharp.Web.Http.Serialization
{
    public interface IHttpMessageSerializerAsync
    {
        Task SerializeAsync(Task<HttpResponseMessage> response, Stream stream);
        Task SerializeAsync(HttpRequestMessage request, Stream stream);
        Task<HttpResponseMessage> DeserializeToResponseAsync(Stream stream);
        Task<HttpRequestMessage> DeserializeToRequestAsync(Stream stream);
    }
}

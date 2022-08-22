using System.Net;
using System.Text.Json.Serialization;

namespace Optomni.Utilities.Model
{
    public class GeneralResponse<T>
    {
        [JsonPropertyName("status_code")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("message_key")]
        public string MessageKey { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("paging")]
        public PagingResponse Paging { get; set; }
    }
}

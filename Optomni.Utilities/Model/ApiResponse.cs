using System.Text.Json.Serialization;

namespace Optomni.Utilities.Model
{
    public class SuccessResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = false;
    }

    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class ErrorResponseWithCode
    {
        [JsonPropertyName("error")]
        public ErrorWithCode Error { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ErrorWithCode : Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class DataResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }

    public class DataResponseWithPaging<T> : DataResponse<T>
    {
        [JsonPropertyName("paging")]
        public PagingResponse Paging { get; set; }
    }

    public class PagingResponse
    {
        [JsonPropertyName("prev")]
        public bool Previous { get; set; }

        [JsonPropertyName("next")]
        public bool Next { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_records")]
        public int TotalRecords { get; set; }
    }
}

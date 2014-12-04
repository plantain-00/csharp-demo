using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class JsonResultModel
    {
        public JsonResultModel(bool isSuccess, string message = null, string exceptionMessage = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            ExceptionMessage = exceptionMessage;
        }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}
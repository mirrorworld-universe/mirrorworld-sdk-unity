using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CommonResponse<T>
    {
        [JsonProperty("data")] public T Data;

        [JsonProperty("code")] public long Code;

        [JsonProperty("error")] public string Error;

        [JsonProperty("status")] public string Status;

        [JsonProperty("message")] public string Message;

        [JsonProperty("http_status_code")] public long HttpStatusCode;

        // public static CommonResponse<TData> CustomErrorResponse<TData>(long httpStatusCode, string error, string message)
        // {
        //     return new CommonResponse<TData>()
        //     {
        //         Data = default(TData),
        //         Code = 0,
        //         Status = "fail",
        //         Message = message,
        //         Error = error,
        //         HttpStatusCode = httpStatusCode
        //
        //     };
        // }
    }
}
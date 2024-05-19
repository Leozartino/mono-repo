using System.Text.Json.Serialization;

namespace Kuba.Api.Errors
{
    public class ApiResponse<TData> where TData : class
    {
        public ApiResponse(int statusCode, string message = "", TData data = null)
        {
            StatusCode = statusCode;
            Message = string.IsNullOrEmpty(message) ? GetDefaultMessageForStatusCode(statusCode) : message;
            Data = data;

        }

        public ApiResponse(int statusCode, TData data = null)
        {
            StatusCode = statusCode;
            Message = GetDefaultMessageForStatusCode(statusCode);
            Data = data;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData Data { get; set; }

        public bool ShouldSerializeData() => Data != null;


        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Success";
                case 201:
                    return "Created";
                case 400:
                    return "Bad Request, please review the request";
                case 401:
                    return "Not Authorized, please check your credentials";
                case 404:
                    return "Resource Not Found";
                case 403:
                    return "User does not have enough permission to access or peform an action for this resource";
                case 500:
                    return "Internal server error";
                default:
                    return "";
            }
        }
    }
}

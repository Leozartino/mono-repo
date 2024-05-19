namespace Kuba.Api.Errors
{
    public class ApiException : ApiResponse<ApiException>
    {
        public ApiException(int statusCode, string message = "", string details = "") : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}

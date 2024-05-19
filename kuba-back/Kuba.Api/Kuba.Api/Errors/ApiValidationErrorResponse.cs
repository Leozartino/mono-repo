namespace Kuba.Api.Errors
{
    public class ApiValidationErrorResponse : ApiResponse<ApiValidationErrorResponse>
    {
        public ApiValidationErrorResponse() : base(400, "")
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}

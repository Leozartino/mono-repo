
namespace Kuba.Api.Dtos.User
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
 
using Microsoft.AspNetCore.Identity;

namespace Assignment.DTO
{
    public class UserTokenDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserLogInfo Data {  get; set; }
        public string? Token {  get; set; }

    }
    public class UserLogInfo
    {
        public string UserId { get; set; }
        public string UserEmail {  get; set; }
        public string Role { get; set; }
    }

}

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Assignment.DTO
{
    public class UserTokenDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string TokenDate { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
        public string Token {  get; set; }


    }
    

}

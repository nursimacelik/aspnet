using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Core.Auth
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public UserDto NewUser { get; set; }
    }


    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string AccessToken { get; set; }
    }
}
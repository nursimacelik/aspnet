using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
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
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class ChangePasswordRequest : LoginRequest
    {
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }
    }
    

    public class RegisterResponse : ApplicationResult
    {
        public UserDto NewUser { get; set; }
    }

    public class LoginResponse : ApplicationResult
    {
        public string AccessToken { get; set; }
    }
}
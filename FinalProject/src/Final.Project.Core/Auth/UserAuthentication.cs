﻿using AutoMapper;
using Final.Project.Core.EmailServices;
using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Final.Project.Core.Auth
{

    public static class UserAuthentication
    {

        public static User currentUser;


        public static ApplicationResult ChangePassword(IUnitOfWork unitOfWork, ChangePasswordRequest request, IConfiguration configuration)
        {
            // Validate input
            var validator = new ChangePasswordValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ApplicationResult { Success = false, ErrorMessage = validationResult.ToString() };
            }

            // Check if user exists and can authenticate 
            var authResult = Authenticate(unitOfWork, request);
            if (!authResult.Success)
            {
                return authResult;
            }

            // Update password

            var hash = GenerateSaltedHash(request.NewPassword, request.Email);
            currentUser.Password = hash;
            unitOfWork.User.Update(currentUser);
            unitOfWork.Complete();

            return new ApplicationResult { Success = true };
        }



        public static ApplicationResult Register(IUnitOfWork unitOfWork, IMapper mapper, RegisterRequest request)
        {
            // Validate input
            var validator = new RegisterRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new RegisterResponse { Success = false, ErrorMessage = validationResult.ToString() };
            }

            var existingAccount = unitOfWork.User.Where(x => x.Email.Equals(request.Email)).FirstOrDefault();
            if (existingAccount != null)
            {
                return new RegisterResponse { Success = false, ErrorMessage = $"Another account is using {request.Email}." };
            }
            
            var hash = GenerateSaltedHash(request.NewPassword, request.Email);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hash,
            };
            unitOfWork.User.Add(user);
            unitOfWork.Complete();

            // Say welcome to the user
            var email = CreateWelcomeEmail(user);
            EmailService.AddEmailToQueue(email);

            var dto = mapper.Map<UserDto>(user);
            return new RegisterResponse { Success = true, NewUser = dto };
        }

        public static ApplicationResult Authenticate(IUnitOfWork unitOfWork, LoginRequest request)
        {
            // Check if user exists
            var user = unitOfWork.User.Where(x => x.Email.Equals(request.Email)).FirstOrDefault();
            if (user == null)
            {
                return new ApplicationResult { Success = false, ErrorMessage = "Authentication failed. Please check your credentials." };
            }

            // Check if account is blocked
            if (user.FailedLoginCount >= 3)
            {
                return new ApplicationResult { Success = false, ErrorMessage = "You entered invalid credentials too many times." };
            }

            // Check if password is correct
            var hash = GenerateSaltedHash(request.Password, request.Email);
            var correctHash = user.Password;
            bool same = CompareHashes(hash, correctHash);
            if (!same)
            {
                HandleFailedLogin(unitOfWork, user);
                return new ApplicationResult { Success = false, ErrorMessage = "Login failed. Please check your credentials." };
            }
            currentUser = user;
            return new ApplicationResult { Success = true };
        }
        

        public static ApplicationResult Login(IUnitOfWork unitOfWork, LoginRequest request, IConfiguration configuration)
        {
            // Validate input
            var validator = new LoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ApplicationResult { Success = false, ErrorMessage = validationResult.ToString() };
            }


            var result = Authenticate(unitOfWork, request);
            if (!result.Success)
            {
                return result;
            }

            // Create and return the JWT token
            
            var key = configuration["Jwt:Key"];
            var iss = configuration["Jwt:Issuer"];
            string token = TokenHandler.GenerateToken(key, iss, currentUser);

            return new LoginResponse { Success = true, AccessToken = token };
        }

        public static void HandleFailedLogin(IUnitOfWork unitOfWork, User user)
        {
            // Increment count for failed login attemps
            user.FailedLoginCount++;
            unitOfWork.User.Update(user);
            unitOfWork.Complete();

            // If login fails three times, account is blocked 
            if (user.FailedLoginCount >= 3)
            {
                var email = CreateAccountBlockedEmail(user);
                EmailService.AddEmailToQueue(email);
            }
        }

        public static Email CreateWelcomeEmail(User user)
        {
            var email = new Email
            {
                Id = Guid.NewGuid().ToString(),
                ReceiverName = $"{user.FirstName} {user.LastName}",
                ReceiverAddress = user.Email,
                Subject = "Welcome",
                Text = @$"Hello {user.FirstName}, Welcome to the Auction Center. Enjoy!",
            };
            return email;
        }

        public static Email CreateAccountBlockedEmail(User user)
        {
            var email = new Email
            {
                Id = Guid.NewGuid().ToString(),
                ReceiverName = $"{user.FirstName} {user.LastName}",
                ReceiverAddress = user.Email,
                Subject = "About Your Account",
                Text = $"Dear {user.FirstName} " +
                    $"Numerous unsuccessful login attempts has been observed on your account at Auction Center. " +
                    $"Consequently your account has been blocked for your safety. " +
                    $"Please contact to our customer service to gain access to your account. Regards."
            };
            return email;
        }


        public static byte[] GenerateSaltedHash(string password, string salt)
        {
            var salted = Encoding.ASCII.GetBytes(password.Concat(salt).ToArray());
            var saltedHash = SHA1.HashData(salted);
            return saltedHash;

        }

        public static bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for(var i = 0; i < hash1.Length; i++) {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

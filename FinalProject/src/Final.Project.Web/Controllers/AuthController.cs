using AutoMapper;
using Final.Project.Core.Auth;
using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Final.Project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AuthController> logger;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public LoginResponse Login([FromBody] LoginRequest request)
        {
            return UserAuthentication.Authorize(unitOfWork, request, configuration);
        }

        [HttpPost]
        [Route("Register")]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            return UserAuthentication.Register(unitOfWork, mapper, request);
        }
    }
}

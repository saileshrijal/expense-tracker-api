using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Dtos;
using ExpenseTracker.Manager.Interface;
using ExpenseTracker.Services.Interface;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AuthController(IUserService userService, 
                            IAuthManager authManager, 
                            IEmailService emailService)
        {
            _userService = userService;
            _authManager = authManager;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {
            if(!ModelState.IsValid){
                return BadRequest(new{
                    message = "Invalid payload"
                });
            }
            var authResult = await _authManager.Login(vm.UserName!, vm.Password!);
            return Ok(authResult);
            
        }

       
    }
}
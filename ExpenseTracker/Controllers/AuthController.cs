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

        public AuthController(IUserService userService, IAuthManager authManager)
        {
            _userService = userService;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {
            if(!ModelState.IsValid){
                return BadRequest(new{
                    message = "Invalid payload"
                });
            }
            var authResult = await _authManager.Login(vm.UserName, vm.Password);
            return Ok(authResult);
            
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterVM vm)
        {
            try{
                if(!ModelState.IsValid){
                    return BadRequest(new{
                        message = "Invalid payload"
                    });
                }
                var userDto = new UserDto(){
                    UserName = vm.UserName,
                    Email = vm.Email,
                    Password = vm.Password
                };
                await _userService.CreateAsync(userDto);
                return Ok(new{
                    message = "User created successfully"
                });
            }
            catch(Exception ex){
                return BadRequest(new{
                    message = ex.Message
                });
            }
        }
    }
}
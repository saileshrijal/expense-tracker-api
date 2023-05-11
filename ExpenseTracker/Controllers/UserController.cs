using ExpenseTracker.Dtos;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interface;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService,
                            IEmailService emailService,
                            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
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
                await SendEmailConfirmationToken(vm.Email!);
                return Ok();
            }
            catch(Exception ex){
                return BadRequest(new{
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmationToken(string email){
            try{
                var user = await _userManager.FindByEmailAsync(email);
                if(user == null){
                    return BadRequest(new{
                        message = "User not found"
                    });
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
                var confirmationLink = Url.Action("ConfirmEmail", "User", new { token, userId = user!.Id }, Request.Scheme);
                var htmlMessage = $"<a href=\"{confirmationLink}\">Click here to confirm your email</a>";
                await _emailService.SendEmailAsync(user!.Email!, "Expense Tracker Email Confirmation", htmlMessage!);
                return Ok();
            }
            catch(Exception ex){
                return BadRequest(new{
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try{
                if(!ModelState.IsValid){
                    return BadRequest(new{
                        message = "Invalid payload"
                    });
                }
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null){
                    return BadRequest(new{
                        message = "User not found"
                    });
                }
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(!result.Succeeded){
                    return BadRequest(new{
                        message = "Invalid token"
                    });
                }
                return Ok();
            }
            catch(Exception ex){
                return BadRequest(new{
                    message = ex.Message
                });
            }
        }
    }
}
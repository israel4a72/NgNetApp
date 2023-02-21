using System.Security.Cryptography;
using System.Text;
using API.Models.DTOs;
using Data;
using Data.Entities;
using Data.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AccountController(DataContext context, IUserService userService, IAuthService authService)
        {
            _context = context;
            _userService = userService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO userDTO)
        {
            if (await _userService.UserExistsAsync(userDTO.Username)) return BadRequest("Username already has taken");
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = userDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Username = userDTO.Username,
                token = _authService.GenerateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO userDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userDTO.Username.ToLower());
            if (user is null) return Unauthorized("Invalid username");
            if (!_authService.CheckPassword(user, userDTO.Password)) return Unauthorized("Invalid password");
            return new UserDTO
            {
                Username = userDTO.Username,
                token = _authService.GenerateToken(user)
            };
        }
    }
}
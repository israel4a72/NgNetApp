using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }
}
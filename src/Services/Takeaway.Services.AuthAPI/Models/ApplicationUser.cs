﻿using Microsoft.AspNetCore.Identity;

namespace Takeaway.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}

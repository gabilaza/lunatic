﻿using Microsoft.AspNetCore.Identity;

namespace Lunatic.Identity.Models {
    public class ApplicationUser : IdentityUser {
        public string? Name { get; set; }
    }
}

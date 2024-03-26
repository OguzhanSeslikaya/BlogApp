using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public bool isAdmin { get; set; }
        public string? refreshToken { get; set; }
        public DateTime? refreshTokenEndDate { get; set; }

    }
}

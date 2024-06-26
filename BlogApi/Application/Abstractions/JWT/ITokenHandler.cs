﻿using Blog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.JWT
{
    public interface ITokenHandler
    {
        Token createAccessToken(int minute,AppUser user);
        string createRefreshToken();
    }
}

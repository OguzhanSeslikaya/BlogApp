using Blog.Application.Abstractions.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.SignIn
{
    public class SignInUserCommandResponse
    {
        public bool basariDurum { get; set; }
        public string mesaj { get; set; }
        public Token? token { get; set; }
    }
}

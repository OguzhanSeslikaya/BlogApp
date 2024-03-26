using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.SignIn
{
    public class SignInUserCommandRequest : IRequest<SignInUserCommandResponse>
    {
        public string kullaniciAdi { get; set; }
        public string parola { get; set; }
    }
}

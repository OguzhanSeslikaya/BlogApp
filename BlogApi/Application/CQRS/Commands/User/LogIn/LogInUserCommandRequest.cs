using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.LogIn
{
    public class LogInUserCommandRequest : IRequest<LogInUserCommandResponse>
    {
        public string kullaniciAdi { get; set; }
        public string parola { get; set; }
    }
}

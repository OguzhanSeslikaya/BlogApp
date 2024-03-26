using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.Page.AddPage
{
    public class AddPageCommandRequest : IRequest<AddPageCommandResponse>
    {
        public IFormCollection form;
    }
}
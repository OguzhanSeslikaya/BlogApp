using Blog.Application.CQRS.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using Blog.Application.CustomAttributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "user")]
    public class AuthorizeEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizeEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Updating,definition = "AssignEndpointToRole" , menu = "authorizeEndpoint")]
        public async Task<IActionResult> assignRole([FromBody]AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest) {
            assignRoleEndpointCommandRequest.type = typeof(Program);
            var response = await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }
    }
}

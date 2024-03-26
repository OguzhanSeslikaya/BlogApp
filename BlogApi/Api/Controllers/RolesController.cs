using Blog.Application.CQRS.Commands.Role.CreateRole;
using Blog.Application.CQRS.Commands.Role.DeleteRole;
using Blog.Application.CQRS.Commands.Role.UpdateRole;
using Blog.Application.CQRS.Queries.Role.GetRoleById;
using Blog.Application.CQRS.Queries.Role.GetRoles;
using Blog.Application.CustomAttributes;
using Blog.Application.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "user")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(actionType = ActionType.Reading,definition = "GetRoles",menu = "Roles")]
        public async Task<IActionResult> getRoles([FromQuery]GetRolesQueryRequest getRolesQueryRequest)
        {
           GetRolesQueryResponse getRolesQueryResponse = await _mediator.Send(getRolesQueryRequest);
           return Ok(getRolesQueryResponse);
        }
        [HttpGet("{id}")]
        [AuthorizeDefinition(actionType = ActionType.Reading, definition = "GetRoleById", menu = "Roles")]
        public async Task<IActionResult> getRoleById([FromRoute]GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse getRoleByIdQueryResponse = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(getRoleByIdQueryResponse);
        }
        [HttpPost]
        [AuthorizeDefinition(actionType = ActionType.Writing, definition = "CreateRole", menu = "Roles")]
        public async Task<IActionResult> createRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse createRoleCommandResponse = await _mediator.Send(createRoleCommandRequest);
            return Ok(createRoleCommandResponse);
        }
        [HttpPut("{id}")]
        [AuthorizeDefinition(actionType = ActionType.Updating, definition = "UpdateRole", menu = "Roles")]
        public async Task<IActionResult> updateRole([FromRoute,FromBody]UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse updateRoleCommandResponse = await _mediator.Send(updateRoleCommandRequest);
            return Ok(updateRoleCommandResponse);
        } 
        [HttpDelete("[action]")]
        [AuthorizeDefinition(actionType = ActionType.Deleting, definition = "DeleteRole", menu = "Roles")]
        public async Task<IActionResult> deleteRole([FromBody]DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse deleteRoleCommandResponse = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(deleteRoleCommandResponse);
        }
    }
}

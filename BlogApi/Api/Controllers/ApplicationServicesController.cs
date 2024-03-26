using Blog.Application.Abstractions.Configurations;
using Blog.Application.CQRS.Queries.Authorize.GetAuthorizeDefinitionEndPoint;
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
    public class ApplicationServicesController : ControllerBase
    {
        readonly IMediator _mediator;
        public ApplicationServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Reading, definition = "getEndpoints", menu = "applicationService")]
        public async Task<IActionResult> getAuthorizeDefinitionEndPoint([FromRoute] GetAuthorizeDefinitionEndPointQueryRequest getAuthorizeDefinitionEndPointQueryRequest)
        {
            getAuthorizeDefinitionEndPointQueryRequest.type = typeof(Program);
            GetAuthorizeDefinitionEndPointResponse response = await _mediator.Send(getAuthorizeDefinitionEndPointQueryRequest);
            return Ok(response);
        }
    }
}

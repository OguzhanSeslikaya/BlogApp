
using Blog.Application.CQRS.Commands.User.AssignRole;
using Blog.Application.CQRS.Commands.User.Delete;
using Blog.Application.CQRS.Commands.User.LogIn;
using Blog.Application.CQRS.Commands.User.RefreshToken;
using Blog.Application.CQRS.Commands.User.SignIn;
using Blog.Application.CQRS.Queries.User.GetAllUser;
using Blog.Application.CQRS.Queries.User.GetRoleByUsername;
using Blog.Application.CustomAttributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> getRoleByUserName([FromRoute] GetRoleByUsernameQueryRequest getRoleByUsernameQueryRequest)
        {
            GetRoleByUsernameQueryResponse response = await _mediator.Send(getRoleByUsernameQueryRequest);
            return Ok(response);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> signIn()
        {
            SignInUserCommandResponse response = await _mediator.Send(new SignInUserCommandRequest() { kullaniciAdi = Request.Form["kullaniciAdi"],parola = Request.Form["parola"] });
            return Ok(response);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> logIn()
        {
            LogInUserCommandResponse response = await _mediator.Send(new LogInUserCommandRequest() { kullaniciAdi = Request.Form["kullaniciAdi"],parola = Request.Form["parola"] });
            _logger.LogInformation("giriş Yapıldı.");
            return Ok(response);
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> refreshToken([FromBody]RefreshTokenCommandRequest refreshTokenCommandRequest)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenCommandRequest);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "user")]
        [HttpGet]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Reading,definition ="GetAllUser",menu ="User")]
        public async Task<IActionResult> getAllUser()
        {
            GetAllUserQueryResponse response = await _mediator.Send(new GetAllUserQueryRequest());
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "user")]
        [HttpPost("[Action]")]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Updating,definition ="AssignRoleToUser",menu = "User")]
        public async Task<IActionResult> assignRoleToUser([FromBody] AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "user")]
        [HttpDelete]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Deleting,definition ="DeleteUser",menu ="User")]
        public async Task<IActionResult> deleteUser([FromBody] DeleteUserCommandRequest deleteUserCommandRequest)
        {
            DeleteUserCommandResponse response = await _mediator.Send(deleteUserCommandRequest);
            return Ok(response);
        }

        

    }
}

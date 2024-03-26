using Blog.Application.CQRS.Commands.Page.AddPage;
using Blog.Application.CQRS.Commands.Page.DeletePage;
using Blog.Application.CQRS.Commands.Page.GetAllPages;
using Blog.Application.CQRS.Commands.Page.GetPage;
using Blog.Application.CQRS.Commands.Page.GetPageImage;
using Blog.Application.CQRS.Queries.Page.GetSearchedPages;
using Blog.Application.CustomAttributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class PageController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<PageController> _logger;

        public PageController(IMediator mediator, ILogger<PageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> getAllPages([FromQuery]GetAllPagesQueryRequest getAllPagesQueryRequest)
        {
            GetAllPagesQueryResponse response = await _mediator.Send(getAllPagesQueryRequest);
            return Ok(response);
            //return File(response.bytes,"image/jpeg");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> getSearchedPages([FromBody]GetSearchedPagesQueryRequest getSearchedPagesQueryRequest)
        {
            GetSearchedPagesQueryResponse response = await _mediator.Send(getSearchedPagesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{bannerImageName}")]
        public async Task<IActionResult> getBannerImage([FromRoute]GetBannerImageQueryRequest getBannerImageQueryRequest)
        {
            GetBannerImageQueryResponse response = await _mediator.Send(getBannerImageQueryRequest);
            return File(response.bytes,"image/jpeg");
        }
        [HttpGet("[action]/{pageId}")]
        public async Task<IActionResult> getPage([FromRoute] GetPageQueryRequest getPageQueryRequest)
        {
            GetPageQueryResponse response = await _mediator.Send(getPageQueryRequest);
            return Ok(response);
        }



        [HttpDelete("[action]")]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Deleting, definition = "DeletePage", menu = "Page")]
        [Authorize(AuthenticationSchemes = "user")]
        public async Task<IActionResult> delete([FromBody]DeletePageCommandRequest deletePageCommandRequest)
        {
            DeletePageCommandResponse response = await _mediator.Send(deletePageCommandRequest);

            _logger.LogInformation("sayfa silindi.");

            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(actionType = Application.Enums.ActionType.Writing,definition = "CreatePage",menu = "Page")]
        [Authorize(AuthenticationSchemes = "user")]
        public async Task<IActionResult> createPage()
        {
            AddPageCommandResponse response = await _mediator.Send(new AddPageCommandRequest() { form= Request.Form});

            _logger.LogInformation("sayfa eklendi.");
            
            return Ok(response);
        }

        
    }
}

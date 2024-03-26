using Blog.Application.Abstractions.Configurations;

namespace Blog.Application.CQRS.Queries.Authorize.GetAuthorizeDefinitionEndPoint
{
    public class GetAuthorizeDefinitionEndPointResponse
    {
        public List<string>? authorizeEndpoints { get; set; }
        public List<Menu> menus { get; set; }
    }
}
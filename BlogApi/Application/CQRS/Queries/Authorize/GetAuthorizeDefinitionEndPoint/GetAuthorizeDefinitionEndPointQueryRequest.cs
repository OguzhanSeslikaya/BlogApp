using MediatR;

namespace Blog.Application.CQRS.Queries.Authorize.GetAuthorizeDefinitionEndPoint
{
    public class GetAuthorizeDefinitionEndPointQueryRequest : IRequest<GetAuthorizeDefinitionEndPointResponse>
    {
        public string id { get; set; }
        public Type? type { get; set; }
    }
}
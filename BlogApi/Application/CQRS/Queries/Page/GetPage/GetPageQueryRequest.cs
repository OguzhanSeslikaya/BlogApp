using MediatR;

namespace Blog.Application.CQRS.Commands.Page.GetPage
{
    public class GetPageQueryRequest : IRequest<GetPageQueryResponse>
    {
        public string pageId { get; set; }
    }
}
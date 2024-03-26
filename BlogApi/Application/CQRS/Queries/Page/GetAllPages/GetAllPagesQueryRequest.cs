using MediatR;

namespace Blog.Application.CQRS.Commands.Page.GetAllPages
{
    public class GetAllPagesQueryRequest : IRequest<GetAllPagesQueryResponse>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
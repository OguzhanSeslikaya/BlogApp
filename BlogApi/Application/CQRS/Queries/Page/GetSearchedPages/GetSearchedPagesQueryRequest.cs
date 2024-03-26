using MediatR;

namespace Blog.Application.CQRS.Queries.Page.GetSearchedPages
{
    public class GetSearchedPagesQueryRequest : IRequest<GetSearchedPagesQueryResponse>
    {
        public string word { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
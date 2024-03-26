using Blog.Application.CQRS.Commands.Page.GetAllPages;

namespace Blog.Application.CQRS.Queries.Page.GetSearchedPages
{
    public class GetSearchedPagesQueryResponse
    {
        public int count { get; set; }
        public List<PageContract> pages { get; set; }
    }
}
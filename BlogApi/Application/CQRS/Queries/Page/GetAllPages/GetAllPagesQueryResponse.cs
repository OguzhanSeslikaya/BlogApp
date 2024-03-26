

namespace Blog.Application.CQRS.Commands.Page.GetAllPages
{
    public class GetAllPagesQueryResponse
    {
        public int count { get; set; }
        public List<PageContract> pages { get; set; }
    }
}
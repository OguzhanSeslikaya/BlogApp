using Blog.Domain.Entities.Contracts.User;

namespace Blog.Application.CQRS.Queries.User.GetAllUser
{
    public class GetAllUserQueryResponse
    {
        public List<Blog.Domain.Entities.Contracts.User.GetAllUser> users { get; set; }
    }
}
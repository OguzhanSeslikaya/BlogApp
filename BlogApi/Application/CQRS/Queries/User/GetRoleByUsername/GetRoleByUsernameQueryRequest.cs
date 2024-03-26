using MediatR;

namespace Blog.Application.CQRS.Queries.User.GetRoleByUsername
{
    public class GetRoleByUsernameQueryRequest : IRequest<GetRoleByUsernameQueryResponse>
    {
        public string userName { get; set; }
    }
}
using MediatR;

namespace Blog.Application.CQRS.Queries.User.GetAllUser
{
    public class GetAllUserQueryRequest : IRequest<GetAllUserQueryResponse>
    {
    }
}
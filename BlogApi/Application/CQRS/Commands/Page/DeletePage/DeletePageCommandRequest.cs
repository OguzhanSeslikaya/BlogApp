using MediatR;

namespace Blog.Application.CQRS.Commands.Page.DeletePage
{
    public class DeletePageCommandRequest : IRequest<DeletePageCommandResponse>
    {
        public string bannerId { get; set; }
        public string pageId { get; set; }
    }
}
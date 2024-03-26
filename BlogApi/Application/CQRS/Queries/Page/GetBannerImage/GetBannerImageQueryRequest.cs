using MediatR;

namespace Blog.Application.CQRS.Commands.Page.GetPageImage
{
    public class GetBannerImageQueryRequest : IRequest<GetBannerImageQueryResponse>
    {
        public string bannerImageName { get; set; }
    }
}
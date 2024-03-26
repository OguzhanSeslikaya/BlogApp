using Blog.Application.Abstractions.Storage;
using Blog.Application.CQRS.Commands.Page.GetAllPages;
using Blog.Application.Repositories.File.BannerFile;
using Blog.Application.Repositories.File.PageFile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.Page.GetPageImage
{
    public class GetBannerImageQueryHandler : IRequestHandler<GetBannerImageQueryRequest, GetBannerImageQueryResponse>
    {
        readonly IBannerFileReadRepository _bannerFileReadRepository;

        public GetBannerImageQueryHandler(IBannerFileReadRepository bannerFileReadRepository)
        {
            _bannerFileReadRepository = bannerFileReadRepository;
        }

        public async Task<GetBannerImageQueryResponse> Handle(GetBannerImageQueryRequest request, CancellationToken cancellationToken)
        {
            var bannerFile = await _bannerFileReadRepository.getSingleAsync(b => b.fileName.Equals(request.bannerImageName));
            if (bannerFile != null)
            {
                var bytes = File.ReadAllBytes(bannerFile.path);
                return new GetBannerImageQueryResponse() { bytes = bytes };
            }
            return new GetBannerImageQueryResponse() { bytes = new byte[0] };
        }
    }
}

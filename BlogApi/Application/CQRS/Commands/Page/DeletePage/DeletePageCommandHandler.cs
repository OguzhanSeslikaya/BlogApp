using Blog.Application.Abstractions.Storage;
using Blog.Application.Repositories.File.BannerFile;
using Blog.Application.Repositories.File.PageFile;
using Blog.Domain.Entities.File.LocalStorage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.Page.DeletePage
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommandRequest, DeletePageCommandResponse>
    {
        readonly IPageFileReadRepository _pageFileReadRepository;
        readonly IBannerFileReadRepository _bannerFileReadRepository;
        readonly IPageFileWriteRepository _pageFileWriteRepository;
        readonly IBannerFileWriteRepository _bannerFileWriteRepository;
        readonly IStorageService _storage;

        public DeletePageCommandHandler(IPageFileWriteRepository pageFileWriteRepository, IBannerFileWriteRepository bannerFileWriteRepository, IStorageService storage, IBannerFileReadRepository bannerFileReadRepository, IPageFileReadRepository pageFileReadRepository)
        {
            _pageFileWriteRepository = pageFileWriteRepository;
            _bannerFileWriteRepository = bannerFileWriteRepository;
            _storage = storage;
            _bannerFileReadRepository = bannerFileReadRepository;
            _pageFileReadRepository = pageFileReadRepository;
        }

        public async Task<DeletePageCommandResponse> Handle(DeletePageCommandRequest request, CancellationToken cancellationToken)
        {
            PageFile pageFile = await _pageFileReadRepository.getByIdAsync(request.pageId);
            BannerFile bannerFile = await _bannerFileReadRepository.getByIdAsync(request.bannerId);

            if (pageFile == null && bannerFile == null)
            {
                return new DeletePageCommandResponse() { succeeded = false };
            }

            await _storage.deleteAsync(pageFile.path);
            await _storage.deleteAsync(bannerFile.path);


            bool deletePage = await _pageFileWriteRepository.removeAsync(pageFile.id.ToString());
            bool deleteBanner = await _bannerFileWriteRepository.removeAsync(bannerFile.id.ToString());

            await _pageFileWriteRepository.saveAsync();

            return new DeletePageCommandResponse() { succeeded = deletePage && deleteBanner };
        }
    }
}

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

namespace Blog.Application.CQRS.Commands.Page.AddPage
{
    public class AddPageCommandHandler : IRequestHandler<AddPageCommandRequest, AddPageCommandResponse>
    {
        readonly IStorageService _storage;
        readonly IPageFileWriteRepository _pageFileWriteRepository;
        readonly IBannerFileWriteRepository _bannerFileWriteRepository;

        public AddPageCommandHandler(IPageFileWriteRepository pageFileWriteRepository, IStorageService storage, IBannerFileWriteRepository bannerFileWriteRepository)
        {
            _pageFileWriteRepository = pageFileWriteRepository;
            _storage = storage;
            _bannerFileWriteRepository = bannerFileWriteRepository;
        }

        public async Task<AddPageCommandResponse> Handle(AddPageCommandRequest request, CancellationToken cancellationToken)
        {
            var k = request.form["title"];
            var yuklenenBir = await _storage.uploadAsync("files\\Pages", request.form.Files[0]);
            PageFile pageFile = new PageFile() { id = Guid.NewGuid(), fileName = yuklenenBir.fileName, path = yuklenenBir.path, storage = _storage.storageName };
            bool kontrolBir = await _pageFileWriteRepository.addAsync(pageFile);
            await _pageFileWriteRepository.saveAsync();

            var yuklenenIki = await _storage.uploadAsync("files\\Banners", request.form.Files[1]);
            bool kontrolIki = await _bannerFileWriteRepository.addAsync(new() { fileName = yuklenenIki.fileName, path = yuklenenIki.path, storage = _storage.storageName ,pageFile = pageFile,title = request.form["title"] });

            await _bannerFileWriteRepository.saveAsync();

            return new AddPageCommandResponse() { basariDurum=kontrolBir&&kontrolIki};
        }
    }
}

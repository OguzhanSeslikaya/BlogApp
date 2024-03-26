using Blog.Application.Repositories.File.BannerFile;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Blog.Application.Abstractions.Storage.Local;
using Blog.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.CQRS.Commands.Page.GetAllPages
{
    public class GetAllPagesQueryHandler : IRequestHandler<GetAllPagesQueryRequest, GetAllPagesQueryResponse>
    {
        readonly IBannerFileReadRepository _bannerFileReadRepository;
        readonly IStorageService _storage;

        public GetAllPagesQueryHandler(IBannerFileReadRepository bannerFileReadRepository, IStorageService storage)
        {
            _bannerFileReadRepository = bannerFileReadRepository;
            _storage = storage;
        }

        public async Task<GetAllPagesQueryResponse> Handle(GetAllPagesQueryRequest request, CancellationToken cancellationToken)
        {
            var pages = new List<PageContract>();
            var items = _bannerFileReadRepository.getAll().Include(a => a.pageFile).Skip((request.page-1)*request.pageSize).Take(request.pageSize).ToList();
            var count = await _bannerFileReadRepository.table.CountAsync();
            foreach ( var item in items )
            {
                var page = new PageContract(item.fileName,item.title,item.pageFile.id.ToString(),item.id.ToString());
                pages.Add(page);
            }
            
            return new GetAllPagesQueryResponse() { pages = pages,count = count };

        }
    }
}

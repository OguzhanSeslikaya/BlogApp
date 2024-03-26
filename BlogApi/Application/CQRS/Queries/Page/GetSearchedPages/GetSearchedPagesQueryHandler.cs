using Blog.Application.CQRS.Commands.Page.GetAllPages;
using Blog.Application.Repositories.File.BannerFile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Queries.Page.GetSearchedPages
{
    public class GetSearchedPagesQueryHandler : IRequestHandler<GetSearchedPagesQueryRequest, GetSearchedPagesQueryResponse>
    {
        readonly IBannerFileReadRepository _bannerFileReadRepository;

        public GetSearchedPagesQueryHandler(IBannerFileReadRepository bannerFileReadRepository)
        {
            _bannerFileReadRepository = bannerFileReadRepository;
        }

        public async Task<GetSearchedPagesQueryResponse> Handle(GetSearchedPagesQueryRequest request, CancellationToken cancellationToken)
        {
            var pages = new List<PageContract>();
            var items = _bannerFileReadRepository.table.Where(p => p.title.Contains(request.word)).Include(a => a.pageFile).Skip((request.page - 1) * request.pageSize).Take(request.pageSize).ToList();
            var count = await _bannerFileReadRepository.table.Where(p => p.title.Contains(request.word)).CountAsync();
            foreach (var item in items)
            {
                var page = new PageContract(item.fileName, item.title, item.pageFile.id.ToString(), item.id.ToString());
                pages.Add(page);
            }

            return new GetSearchedPagesQueryResponse() { pages = pages, count = count };
        }
    }
}

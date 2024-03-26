using Blog.Application.Repositories.File.PageFile;
using Blog.Domain.Entities.File.LocalStorage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.Page.GetPage
{
    public class GetPageQueryHandler : IRequestHandler<GetPageQueryRequest, GetPageQueryResponse>
    {
        readonly IPageFileReadRepository _pageFileReadRepository;
        public GetPageQueryHandler(IPageFileReadRepository pageFileReadRepository)
        {
            _pageFileReadRepository = pageFileReadRepository;
        }
        public async Task<GetPageQueryResponse> Handle(GetPageQueryRequest request, CancellationToken cancellationToken)
        {
            PageFile pageFile = await _pageFileReadRepository.getByIdAsync(request.pageId);
            if (pageFile != null)
            {
                var innerHtml = File.ReadAllText(pageFile.path);
                return new GetPageQueryResponse() { innerHtml = innerHtml };
            }
            return new GetPageQueryResponse() { innerHtml = "" };
            
        }
    }
}

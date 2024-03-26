using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.Page.GetAllPages
{
    public class PageContract
    {
        public PageContract(string bannerImageName, string bannerTitle, string pageId, string bannerId)
        {
            this.bannerImageName = bannerImageName;
            this.bannerTitle = bannerTitle;
            this.pageId = pageId;
            this.bannerId = bannerId;
        }

        public string bannerId { get; set; }
        public string bannerImageName { get; set; }
        public string bannerTitle { get; set; }
        public string pageId { get; set; }
    }
}

using Blog.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.File.LocalStorage
{
    public class BannerFile : BaseFile
    {
        public string title { get; set; }
        public PageFile pageFile { get; set; }
    }
}

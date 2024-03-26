using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.Contracts.Page
{
    public class Page
    {
        public string title { get; set; }
        public FileShare page { get; set; }
        public FileShare banner { get; set; }

    }
}

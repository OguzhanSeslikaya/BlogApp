using Blog.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.Configurations
{
    public class Action
    {
        public string actionType { get; set; }
        public string httpType { get; set; }
        public string definition { get; set; }
        public string code { get; set; }
    }
}

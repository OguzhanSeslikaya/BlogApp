using Blog.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string menu { get; set; }
        public string definition { get; set; }
        public ActionType actionType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.Configurations
{
    public class Menu
    {
        public string menuName { get; set; }
        public List<Action> actions { get; set; } = new();
    }
}

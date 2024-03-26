using Blog.Application.Abstractions.Configurations;
using Blog.Application.CustomAttributes;
using Blog.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> getAuthorizeDefinitionEndPoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new List<Menu>();
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute),true));
                if (actions != null)
                {
                    foreach (var action in actions)
                    {
                        var attributes = action.GetCustomAttributes(true);
                        if (attributes != null)
                        {
                            Menu menu = null;

                            var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                            if (!menus.Any(m => m.menuName == authorizeDefinitionAttribute.menu))
                            {
                                menu = new Menu() { menuName = authorizeDefinitionAttribute.menu };
                                menus.Add(menu);
                            }
                            else
                                menu = menus.FirstOrDefault(m => m.menuName == authorizeDefinitionAttribute.menu);

                            Blog.Application.Abstractions.Configurations.Action _action = new() { 
                                actionType = Enum.GetName(typeof(ActionType),authorizeDefinitionAttribute.actionType),
                                definition = authorizeDefinitionAttribute.definition
                            };

                            var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                            if (httpAttribute != null)
                            {
                                _action.httpType = httpAttribute.HttpMethods.First();
                            }
                            else
                                _action.httpType = HttpMethods.Get;

                            _action.code = $"{menu.menuName}{_action.httpType}{_action.actionType}{_action.definition}".Replace(" ","");
                            menu.actions.Add(_action);
                            
                        }
                    }
                }
            }
            return menus;
        }
    }
}

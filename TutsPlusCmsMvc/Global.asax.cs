using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TutsPlusCmsMvc.App_Start;
using TutsPlusCmsMvc.Models;
using TutsPlusCmsMvc.Models.ModelBinders;

namespace TutsPlusCmsMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            AuthDbConfig.RegisterAdminAsync();
            AuthDbConfig.RegisterRoles();

            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using QuestionnaireSys.DAL;
using System.Security.Principal;
using System.Web.Security;
using QuestionnaireSys.Web.Models;

namespace QuestionnaireSys.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QuestionnaireSysDbContext,QuestionnaireSys.Web.Migrations.Configuration>());
        }

        //当用户票证不为空时，添加一个角色信息
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        LoginUser user = Tools.Tools.GetLoginUserByTicket(HttpContext.Current.User.Identity);
                        HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, new string[] { user.UserType });
                    }
                }
            }
        }

    }
}

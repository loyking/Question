using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using QuestionnaireSys.Web.Models;
using Newtonsoft.Json;

namespace QuestionnaireSys.Web.Tools
{
    public static class Tools
    {
        //获取用户票证中的信息
        public static LoginUser GetLoginUserByTicket(IIdentity sid)
        {
            if (sid is FormsIdentity)
            {
                FormsIdentity id = sid as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                LoginUser loginUser = JsonConvert.DeserializeObject<LoginUser>(ticket.UserData);
                return loginUser;
            }
            return null;
        }
    }

}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Web.Models;
using Newtonsoft.Json;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string loginName, string loginPwd, string returnUrl = "")
        {
            AdminUser user = work.Repository<AdminUser>().GetList(u => u.LoginName == loginName && u.LoginPwd == loginPwd && u.IsDisabled == false).FirstOrDefault();
            if (user == null)
            {
                return Json(new { success = false, message = "用户名或密码错误！" });
            }
            else
            {
                string UserDate = JsonConvert.SerializeObject(new LoginUser() { UserType = "admin", User = user });
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName,
                    DateTime.Now, DateTime.Now.AddDays(1), false, UserDate,
                    FormsAuthentication.FormsCookiePath);
                HttpCookie cook = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                cook.HttpOnly = true;
                Response.Cookies.Add(cook);
                return Json(new { success = true, Url = "/" });
            }
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            
            HttpCookie co = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (co != null)
            {
                co.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(co);
            }
            return RedirectToAction("Login");
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;
using QuestionnaireSys.Web.Models;
using Newtonsoft.Json;

namespace QuestionnaireSys.Web.Controllers
{
    public class LoginController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: Login
        public ActionResult Login(string ReturnUrl)
        {
                if (ReturnUrl!=null && ReturnUrl.Contains("admin"))
                {
                    Response.Redirect("/admin/Login/Login");
                }
            return View();
        }

        [HttpPost]
        public ActionResult login(string LoginName, string LoginPwd, string returnUrl = "")
        {
            LoginPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginPwd, "md5");
            Student student = work.Repository<Student>().GetList(p => p.Mobile == LoginName && p.LoginPwd == LoginPwd && p.IsDisabled == false).FirstOrDefault();
            if (student != null)
            {
                string studentDate = JsonConvert.SerializeObject(new LoginUser() { UserType = "student", User = student });
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, student.StudentName,
                    DateTime.Now, DateTime.Now.AddDays(1), false, studentDate,
                    FormsAuthentication.FormsCookiePath);
                HttpCookie cook = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                cook.HttpOnly = true;
                Response.Cookies.Add(cook);
                return Json(new { success = true, Url = "/" });
            }
            else
            {
                return Json(new { success = false, message = "用户名或密码错误！" });
            }
        }

        [HttpGet]
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
            return RedirectToAction("Login", "Login");
        }

    }
}

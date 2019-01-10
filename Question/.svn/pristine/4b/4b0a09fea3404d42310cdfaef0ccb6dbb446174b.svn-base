using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;
using QuestionnaireSys.Web.Models;

namespace QuestionnaireSys.Web.Controllers
{
    public class MySuggestController : Controller
    {
        public UnitOfWork work = new UnitOfWork();
        // GET: MySuggest
        public ActionResult Index()
        {
            int stuId = GetStudentId();
            List<StudentAnswer> answer= work.Repository<StudentAnswer>().GetList(p => p.StudentId == stuId && p.Suggest!=null).ToList();
            ViewBag.Answer = answer;
            return View();
        }

        public int GetStudentId()
        {
            LoginUser user = Tools.Tools.GetLoginUserByTicket(User.Identity);
            dynamic dy = user.User as dynamic;
            return dy.Id;
        }

    }
}
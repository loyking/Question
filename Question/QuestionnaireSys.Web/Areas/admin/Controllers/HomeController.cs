using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;


namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: admin/Home
        public ActionResult Index()
        {
            List<Student> student = new List<Student>();
            Student stu = student.Where(p => p.StudentName == "小崔").FirstOrDefault();
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
    }
}
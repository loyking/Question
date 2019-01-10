using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class StudentAnswerController : Controller
    {

        // GET: admin/StudentAnswer
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<StudentAnswer>().GetList().ToList();
            ViewBag.Count = work.Repository<StudentAnswer>().GetCount();
            return View();
        }

        public ActionResult AddStudentAnswer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudentAnswers(string stuName, string moble, string cardId, string pwd, int classId)
        {
            try
            {
                Student stu = new Student();
                stu.StudentName = stuName;
                stu.Mobile = moble;
                stu.CardId = cardId;
                stu.LoginPwd = pwd;
                stu.JxClassId = classId;
                stu.IsDisabled = false;
                work.Repository<Student>().Insert(stu);
                work.Save();
                return Json(new { success = true, message = "添加成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

        [HttpGet]
        public ActionResult DeleteStudentAnswer(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    StudentAnswer stu = work.Repository<StudentAnswer>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<StudentAnswer>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StudentAnswerDetail()
        {
            ViewBag.Data = work.Repository<StudentAnswerDetail>().GetList().ToList();
            ViewBag.Count = work.Repository<StudentAnswerDetail>().GetCount();
            return View();
        }
    }
}
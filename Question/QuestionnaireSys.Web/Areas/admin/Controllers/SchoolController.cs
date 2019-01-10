using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class SchoolController : Controller
    {
        UnitOfWork work = new UnitOfWork();

        // GET: admin/School
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<School>().GetList().ToList();
            ViewBag.Count = work.Repository<School>().GetCount();
            return View();
        }

        public ActionResult AddSchool()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddSchool(string SchoolName)
        {
            try
            {
                School model = new School();
                model = work.Repository<School>().GetList(p => p.SchoolName == SchoolName).FirstOrDefault();
                if (model != null)
                {
                    return Json(new { success = false, message = "添加失败,该学校已存在" });
                }
                else
                {
                    School modelschool = new School();
                    modelschool.SchoolName = SchoolName;
                    work.Repository<School>().Insert(modelschool);
                    work.Save();
                    return Json(new { success = true, message = "添加成功！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }
        [HttpGet]
        public ActionResult DeleteSchoolInfo(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    School stu = work.Repository<School>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<School>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult UpdataSchool(int Id,string SchoolName)
        {
            School sch = new School();
            sch.SchoolName = SchoolName;
            ViewBag.School = sch;

            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Updataschool(int Id, string SchoolName)
        {
            try
            {
                School model = work.Repository<School>().GetList(p => p.Id == Id).FirstOrDefault();
                model.SchoolName = SchoolName;
                work.Repository<School>().Update(model);
                work.Save();
                return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

    }
}
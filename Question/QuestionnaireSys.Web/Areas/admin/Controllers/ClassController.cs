using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class ClassController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: admin/Class
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<Classe>().GetList().ToList();
            ViewBag.Count = work.Repository<Classe>().GetCount();
            return View();
        }
        public ActionResult AddClass()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddClass(int SchoolId, string ClasseName)
        {
            try
            {
                Classe model = new Classe();
                model=work.Repository<Classe>().GetList(p => p.ClasseName == ClasseName).FirstOrDefault();
                if (model!=null)
                {
                    return Json(new { success = false, message = "添加失败,该班级已存在" });
                }
                else
                {
                    Classe modelclass = new Classe();
                    modelclass.SchoolId = SchoolId;
                    modelclass.ClasseName = ClasseName;
                    modelclass.IsGraduate = false;
                    work.Repository<Classe>().Insert(modelclass);
                    work.Save();
                    return Json(new { success = true, message = "添加成功！"});
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }
        [HttpGet]
        public ActionResult DeleteStudentInfo(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    Classe stu = work.Repository<Classe>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<Classe>().Delete(stu);
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
        public ActionResult UpdataClass(int Id,string SchoolName, int SchoolId, string ClasseName)
        {
            School sch = new School();
            sch.Id = SchoolId;
            sch.SchoolName = SchoolName;
            ViewBag.School = sch;
            Classe cl = new Classe();
            cl.ClasseName = ClasseName;
            ViewBag.Classe = cl;

            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public ActionResult Updataclass(int Id, int SchoolId, string ClasseName)
        {
            try
            {
                Classe  model = work.Repository<Classe>().GetList(p => p.Id == Id).FirstOrDefault();
                model.SchoolId = SchoolId;
                model.ClasseName = ClasseName;
                model.IsGraduate = false;
                work.Repository<Classe>().Update(model);
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

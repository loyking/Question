using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class departmentController : Controller
    {
        // GET: admin/User
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<Depart>().GetList().ToList();
            ViewBag.Count = work.Repository<Depart>().GetCount();
            return View();
        }
        public ActionResult Adddepatment()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Adddepartment(string DepartName)
        {
            try
            {
                Depart pa = work.Repository<Depart>().GetList(p => p.DepartName == DepartName).FirstOrDefault();
                if (pa != null)
                {
                    return Json(new { success = false, message = "添加失败，该部门已存在"});
                }
                else {
                    Depart part = new Depart();
                    part.DepartName = DepartName;
                    work.Repository<Depart>().Insert(part);
                    work.Save();
                    return Json(new { success = true, message = "添加成功！",href="/admin/department/Index" });
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
                    Depart stu = work.Repository<Depart>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<Depart>().Delete(stu);
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
        public ActionResult Updatadepatment(int id, string DepartName)
        {
            Depart part = new Depart();
            part.DepartName = DepartName;
            ViewBag.Depart = part;

            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult UpdataDepatment(int id, string DepartName)
        {
            try
            {
                Depart model = work.Repository<Depart>().GetList(p => p.Id == id).FirstOrDefault();
                model.DepartName = DepartName;
                work.Repository<Depart>().Update(model);
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
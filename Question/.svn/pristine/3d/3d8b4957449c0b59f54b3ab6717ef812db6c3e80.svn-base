using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class RoleController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: admin/Role
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<UserRole>().GetList().ToList();
            ViewBag.Count = work.Repository<UserRole>().GetCount();
            return View();
        }
        public ActionResult AddRole()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult AddRole(string RoleName)
        {
            try
            {
                UserRole roles = work.Repository<UserRole>().GetList(p => p.RoleName == RoleName).FirstOrDefault();

                if (roles!=null)
                {
                    return Json(new { success = false, message = "添加失败，该角色已存在" });
                }
                else {
                    UserRole role = new UserRole();
                    role.RoleName = RoleName;
                    work.Repository<UserRole>().Insert(role);
                    work.Save();
                    return Json(new { success = true, message = "添加成功！",href="/admin/Role/Index" });
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
                    UserRole stu = work.Repository<UserRole>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<UserRole>().Delete(stu);
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
        public ActionResult UpdataRole(int id,string RoleName)
        {
            UserRole role = new UserRole();
            role.RoleName = RoleName;
            ViewBag.UserRole = role;

            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public ActionResult updataRole(int id,string RoleName)
        {
            try
            {
                UserRole model = work.Repository<UserRole>().GetList(p => p.Id == id).FirstOrDefault();
                model.RoleName = RoleName;
                work.Repository<UserRole>().Update(model);
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
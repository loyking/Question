using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using System.Web.Security;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    [Authorize(Roles ="Admins")]
    public class AdminUserController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        // GET: admin/AdminUser
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<AdminUser>().GetList().ToList();
            ViewBag.Count = work.Repository<AdminUser>().GetCount();
            return View();
        }
        public ActionResult AddAdminUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdminUser(string LoginName, string LoginPwd,string UserName, int DepartId, int UserRoleId)
        {
            try
            {
                AdminUser model = new AdminUser();
                model = work.Repository<AdminUser>().GetList(p => p.LoginName == LoginName||p.UserName==UserName).FirstOrDefault();
                if (model != null)
                {
                    return Json(new { success = false, message = "添加失败,该管理员已存在" });
                }
                else
                {
                    AdminUser modelclass = new AdminUser();
                    modelclass.LoginName = LoginName;
                    LoginPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginPwd, "md5");
                    modelclass.LoginPwd = LoginPwd;
                    modelclass.UserName = UserName;
                    modelclass.DepartId = DepartId;
                    modelclass.UserRoleId = UserRoleId;
                    modelclass.IsDisabled = false;
                    work.Repository<AdminUser>().Insert(modelclass);
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
        public ActionResult DeleteStudentInfo(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    AdminUser stu = work.Repository<AdminUser>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<AdminUser>().Delete(stu);
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
        public ActionResult UpdataAdminUser(int Id, string LoginName, string LoginPwd, string UserName, int DepartId,string DepartName, int UserRoleId, string RoleName)
        {
            Depart sch = new Depart();
            sch.Id = DepartId;
            sch.DepartName = DepartName;
            ViewBag.Depart = sch;
            UserRole role = new UserRole();
            role.Id = UserRoleId;
            role.RoleName = RoleName;
            ViewBag.UserRole = role;
            AdminUser cl = new AdminUser();
            cl.UserName = UserName;
            cl.LoginName = LoginName;
            cl.LoginPwd = LoginPwd;
            ViewBag.Classe = cl;

            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public ActionResult UpdataAdminuser(int Id, string LoginName, string LoginPwd, string UserName, int DepartId, string DepartName, int UserRoleId, string RoleName)
        {
            try
            {
                AdminUser model = work.Repository<AdminUser>().GetList(p => p.Id == Id).FirstOrDefault();
                model.LoginName = LoginName;
                LoginPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginPwd, "md5");
                model.LoginPwd = LoginPwd;
                model.UserName = UserName;
                model.DepartId = DepartId;
                model.UserRoleId = UserRoleId;
                model.IsDisabled = false;
                work.Repository<AdminUser>().Update(model);
                work.Save();
                return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }
        [HttpPost]
        public ActionResult EditIsDisabled(string id, int isZeroOrOne)
        {
            try
            {
                string Id = id;
                AdminUser stu = work.Repository<AdminUser>().GetEntityById(int.Parse(Id.ToString()));
                if (isZeroOrOne == 0)
                {
                    stu.IsDisabled = true;
                }
                else
                {
                    stu.IsDisabled = false;
                }
                work.Repository<AdminUser>().Update(stu);
                work.Save();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpGet]
        public ActionResult UpdatePwd(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePwd(int id, string pwd)
        {
            try
            {
                AdminUser stu = work.Repository<AdminUser>().GetEntityById(id);
                pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
                stu.LoginPwd = pwd;
                work.Repository<AdminUser>().Update(stu);
                work.Save();
                return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "修改失败：" + ex.Message });
                throw;
            }
        }
    }
}
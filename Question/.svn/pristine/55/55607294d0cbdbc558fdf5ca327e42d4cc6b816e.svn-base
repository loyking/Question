using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class QuestionnaireTypeController : Controller
    {
        // GET: admin/QuestionnaireType
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index()
        {
            
            //ViewBag.Data = work.Repository<QuestionnaireType>().GetList().ToList();
            ViewBag.Count = work.Repository<QuestionnaireType>().GetCount();
            return View();
        }

        public ActionResult AddQuestionnaireType(/*QuestionnaireType model*/)
        {
            //ModelState.AddModelError("QuestionnaireName", "问卷类型已存在!");
            return View(/*model*/);
        }

        [HttpPost]
        public ActionResult AddType(string questionnaireType, int userRoleId)
        {
            try
            {
                QuestionnaireType q = work.Repository<QuestionnaireType>().GetList(p => p.QuestionnaireName == questionnaireType).FirstOrDefault();
                if (q != null)
                {
                    return Json(new { success = false, message = "添加失败，该类型已存在！" });
                }
                else
                {
                    QuestionnaireType qt = new QuestionnaireType();
                    qt.QuestionnaireName = questionnaireType;
                    qt.UserRoleId = userRoleId;
                    work.Repository<QuestionnaireType>().Insert(qt);
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

        //public ActionResult Exists(QuestionnaireType model)
        //{
        //    IQueryable<QuestionnaireType> qt = work.Repository<QuestionnaireType>().GetList().Where(m => m.QuestionnaireName == model.QuestionnaireName).ToList();
        //    if (qt.Count() > 0)
        //    {
        //        ModelState.AddModelError("QuestionnaireName", "问卷类型已存在！");
        //    }
        //    return View(qt);
        //}

        [HttpGet]
        public ActionResult UpdateQuestionType(int id, string typeName,string roleName, int roleId)
        {
            QuestionnaireType gt = new QuestionnaireType();
            gt.QuestionnaireName = typeName;
            ViewBag.QuestionnaireType = gt;
            UserRole ur = new UserRole();
            ur.RoleName = roleName;
            ur.Id = roleId;
            ViewBag.UserRole = ur;

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateQuestionType(int id, string typeName, int roleId)
        {
            try
            {
                QuestionnaireType q = work.Repository<QuestionnaireType>().GetList(p => p.QuestionnaireName == typeName && p.UserRoleId== roleId).FirstOrDefault();
                if (q != null)
                {
                    return Json(new { success = false, message ="修改失败，该问卷类型已存在！" });
                }
                else
                {
                    QuestionnaireType q1 = work.Repository<QuestionnaireType>().GetList(p => p.QuestionnaireName == typeName).FirstOrDefault();
                    if (q1 == null)
                    {
                        QuestionnaireType qw = work.Repository<QuestionnaireType>().GetEntityById(id);
                        qw.QuestionnaireName = typeName;
                        qw.UserRoleId = roleId;
                        work.Repository<QuestionnaireType>().Update(qw);
                        work.Save();
                        return Json(new { success = true, message = "修改成功！" });
                    }
                    else
                    {
                        if (q1.QuestionnaireName == typeName && q1.UserRoleId != roleId)
                        {
                            QuestionnaireType qw = work.Repository<QuestionnaireType>().GetEntityById(id);
                            string QuestionnaireName = qw.QuestionnaireName;
                            int roleIds = qw.UserRoleId;
                            qw.QuestionnaireName = typeName;
                            qw.UserRoleId = roleId;
                            work.Repository<QuestionnaireType>().Update(qw);
                            work.Save();
                            if (work.Repository<QuestionnaireType>().GetList(p => p.QuestionnaireName == typeName).Count() > 1)
                            {
                                QuestionnaireType qw1 = work.Repository<QuestionnaireType>().GetEntityById(id);
                                qw1.QuestionnaireName = QuestionnaireName;
                                qw1.UserRoleId = roleIds;
                                work.Repository<QuestionnaireType>().Update(qw);
                                work.Save();
                                return Json(new { success = false, message = "修改失败，该问卷类型已存在！" });
                            }
                            else
                            {
                                return Json(new { success = true, message = "修改成功！" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "修改失败，该问卷类型已存在！" });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

        [HttpGet]
        public ActionResult DeleteQuestionnaireType(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    QuestionnaireType stu = work.Repository<QuestionnaireType>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<QuestionnaireType>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
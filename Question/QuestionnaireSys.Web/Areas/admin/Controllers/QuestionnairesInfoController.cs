using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;
using QuestionnaireSys.Web.Models;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class QuestionnairesInfoController : Controller
    {
        // GET: admin/QuestionnairesInfo
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<Questionnaire>().GetList().ToList();
            ViewBag.Count = work.Repository<Questionnaire>().GetCount();
            return View();
        }

        public ActionResult AddQuestionnairesInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestionnaire(int classId, string questionnaireName,int questionnareTypeId)
        {
            try
            {
                Questionnaire q = work.Repository<Questionnaire>().GetList(p => p.QuestionnaireName == questionnaireName).FirstOrDefault();
                if (q != null)
                {
                    return Json(new { success = false, message = "添加失败，该问卷名称已存在！" });
                }
                else
                {
                    LoginUser user = Tools.Tools.GetLoginUserByTicket(User.Identity);
                    dynamic users = user.User as dynamic;
                    Questionnaire Questionnaire = new Questionnaire();
                    Questionnaire.ClassesId = classId;
                    int id = users.Id;
                    Questionnaire.AdminUserId = id;
                    Questionnaire.QuestionnaireName = questionnaireName;
                    Questionnaire.CreateDate = DateTime.Now;
                    Questionnaire.IsEnd = false;
                    Questionnaire.QuestionnaireTypeId = questionnareTypeId;
                    Questionnaire.Solution = null;
                    work.Repository<Questionnaire>().Insert(Questionnaire);
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
        public ActionResult UpdataQuestionnairesInfo(int id, string className,int classId, string userName,int userId, string questionName, string questionTypeName,int QuestionnaireTypeId, string solution)
        {
            Classe c = new Classe();
            c.ClasseName = className;
            c.Id = classId;
            ViewBag.Classe = c;
            AdminUser au = new AdminUser();
            au.Id = userId;
            au.UserName = userName;
            ViewBag.AdminUser = au;
            Questionnaire q = new Questionnaire();
            q.QuestionnaireName = questionName;
            q.Solution = solution;
            ViewBag.Questionnaire = q;
            QuestionnaireType qt = new QuestionnaireType();
            qt.QuestionnaireName = questionTypeName;
            qt.Id = QuestionnaireTypeId;
            ViewBag.QuestionnaireType = qt;
            ViewBag.QuestionnaireType = qt;

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdataQuestionnairesInfo(int id, int classId, string questionnaireName, int questionnaireTypeId,string solution)
        {
            try
            {
                Questionnaire qq = work.Repository<Questionnaire>().GetList(p => p.QuestionnaireName == questionnaireName).FirstOrDefault();
                if (qq != null)
                {
                    return Json(new { success = false, message = "修改失败，该问卷名称已存在！" });
                }
                else
                {
                    Questionnaire q = work.Repository<Questionnaire>().GetList(p => p.Id == id).FirstOrDefault();
                    q.ClassesId = classId;
                    //q.AdminUserId = userId;
                    q.QuestionnaireName = questionnaireName;
                    q.CreateDate = DateTime.Now;
                    q.IsEnd = false;
                    q.QuestionnaireTypeId = questionnaireTypeId;
                    q.Solution = solution;
                    work.Repository<Questionnaire>().Update(q);
                    work.Save();
                    return Json(new { success = true, message = "修改成功！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }



        [HttpGet]
        public ActionResult DeleteQuestionnaire(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    Questionnaire stu = work.Repository<Questionnaire>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<Questionnaire>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EditIsEnd(string id, int isZeroOrOne)
        {
            try
            {
                string Id = id;
                Questionnaire q = work.Repository<Questionnaire>().GetEntityById(int.Parse(Id.ToString()));
                if (isZeroOrOne == 0)
                {
                    q.IsEnd = true;
                }
                else
                {
                    q.IsEnd = false;
                }
                work.Repository<Questionnaire>().Update(q);
                work.Save();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}
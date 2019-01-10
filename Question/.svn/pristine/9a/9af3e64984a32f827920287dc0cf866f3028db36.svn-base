using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Web.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace QuestionnaireSys.Web.Controllers
{
    public class MyQuestionnaireController : Controller
    {
        private UnitOfWork work = new UnitOfWork();

        public ActionResult Index()
        {
            LoginUser StuUser = Tools.Tools.GetLoginUserByTicket(User.Identity);
            if (StuUser != null && StuUser.UserType == "student")
            {
                dynamic dy = StuUser.User as dynamic;
                int id = dy.Id;    
                SqlParameter[] para = {
                    new SqlParameter("@StudentId",id)
                };
                List<QuestionInfo> questionInfo = work.QueryByProc<QuestionInfo>("proc_QuestionInfo", para);
                ViewBag.QuestionInfo = questionInfo.Where(p => p.state == "已完成");
            }
            return View();
        }

        public ActionResult AnswerDetail(int QuestionId,string QuestionName)
        {
            LoginUser user = Tools.Tools.GetLoginUserByTicket(User.Identity);
            if (user != null && user.UserType == "student")
            {
                SqlParameter[] para = {
                        new SqlParameter("@StudentId",GetStudentId()),
                        new SqlParameter("@QuestionnaireId",QuestionId),
                        new SqlParameter("@StudentSuggest",System.Data.SqlDbType.VarChar,200),
                        new SqlParameter("@AdminName",System.Data.SqlDbType.VarChar,10),
                        new SqlParameter("@AdminSuggest",System.Data.SqlDbType.VarChar,200)
                    };
                para[para.Length - 1].Direction = System.Data.ParameterDirection.Output;
                para[para.Length - 2].Direction = System.Data.ParameterDirection.Output;
                para[para.Length - 3].Direction = System.Data.ParameterDirection.Output;

                List<AnswerDetail> answer = work.QueryByProc<AnswerDetail>("proc_FinishQuestionnaire", para);
                string AdminSuggest = (string)para[para.Length - 1].Value;
                string AdminName = (string)para[para.Length - 2].Value;
                string StudentSuggest = (string)para[para.Length - 3].Value;

                ViewBag.AnswerDetail = answer;
                ViewBag.AdminName = AdminName;
                ViewBag.AdminSuggest = AdminSuggest;
                ViewBag.StudentSuggest = StudentSuggest;
                ViewBag.MatterItem = work.Repository<MatterItem>().GetList(p => p.QuestionnaireId == QuestionId).FirstOrDefault();
                ViewBag.QuestionName = QuestionName;
                ViewBag.QuestionCount = answer.Count;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public int GetStudentId()
        {
            LoginUser user = Tools.Tools.GetLoginUserByTicket(User.Identity);
            dynamic dy = user.User as dynamic;
            return dy.Id;
        }


    }
}
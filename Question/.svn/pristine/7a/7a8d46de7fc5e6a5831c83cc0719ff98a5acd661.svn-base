using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Web.Tools;
using QuestionnaireSys.Web.Models;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace QuestionnaireSys.Web.Controllers
{
    public class QuestionSurveyController : Controller
    {
        private UnitOfWork work = new UnitOfWork();
        // GET: QuestionSurvey
        public ActionResult Index(int Id = 0)
        {
            LoginUser user = Tools.Tools.GetLoginUserByTicket(User.Identity);
            if (user != null && user.UserType == "student")
            {
                Questionnaire Question = work.Repository<Questionnaire>().GetList(p => p.Id == Id).FirstOrDefault();
                if (Question != null)
                {
                    int stuId = GetLoginUserByTicket();
                    ViewBag.Id = Id;
                    ViewBag.QuestionnaireTypeName = Question.QuestionnaireType.QuestionnaireName;
                    ViewBag.QuestionnaireName = Question.QuestionnaireName;

                    ViewBag.MatterItems = work.Repository<MatterItem>().GetList(p => p.QuestionnaireId == Id).FirstOrDefault();
                    if (work.Repository<StudentAnswer>().GetList(p => p.QuestionnaireId == Id && p.StudentId == stuId).FirstOrDefault() == null)
                    {
                        StudentAnswer anser = new StudentAnswer();
                        anser.StudentId = stuId;
                        anser.QuestionnaireId = Id;
                        anser.CreateDate = DateTime.Now;
                        anser.Suggest = null;
                        anser.AdminUserId = Question.AdminUserId;
                        anser.TeacherSuggest = null;
                        anser.Submit = false;
                        work.Repository<StudentAnswer>().Insert(anser);
                        work.Save();
                    }
                    return View();
                }
                else
                {
                    return PartialView("error");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult GetQuestion(int Id)
        {
            List<AnswerInfo> answerInfo = null;
            int stuId = GetLoginUserByTicket();
            SqlParameter[] para = {
                        new SqlParameter("@StudentId",stuId),
                        new SqlParameter("@QuestionnaireId",Id)
                            };
                answerInfo = work.QueryByProc<AnswerInfo>("proc_AnswerInfo", para);
            
            return Json( new { data = answerInfo,length= answerInfo.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsetAnswerDetail(int QuestionnaireId, int QuestionId,string QuestionContext)
        {
            try
            {
                    int query = 0;
                    int stuId = GetLoginUserByTicket();
                    StudentAnswer answer= work.Repository<StudentAnswer>().GetList(p => p.QuestionnaireId == QuestionnaireId && p.StudentId == stuId).FirstOrDefault();
                    StudentAnswerDetail AnswerDetail= work.Repository<StudentAnswerDetail>().GetList(p => p.StudentAnswerId == answer.Id && p.QuestionId == QuestionId).FirstOrDefault();
                    if (AnswerDetail != null)
                    {
                        AnswerDetail.AnswerOptionContext = QuestionContext;
                        work.Repository<StudentAnswerDetail>().Update(AnswerDetail);
                        work.Save();
                    }
                    else
                    {
                        AnswerDetail = new StudentAnswerDetail();
                        AnswerDetail.StudentAnswerId = answer.Id;
                        AnswerDetail.QuestionId = QuestionId;
                        AnswerDetail.AnswerOptionContext = QuestionContext;
                        work.Repository<StudentAnswerDetail>().Insert(AnswerDetail);
                        work.Save();
                        query = 1;
                    }
                
                return Json(new { success = true,query= query },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false , Message=ex.Message}, JsonRequestBehavior.AllowGet);
                throw;
            }
        }


        public int GetLoginUserByTicket()
        {
            LoginUser user= Tools.Tools.GetLoginUserByTicket(User.Identity);
            dynamic dy = user.User as dynamic;
            return dy.Id;
        }

        public ActionResult InsertSuggest(string suggest,int QuestionnaireId)
        {
            try
            {
                int stuId = GetLoginUserByTicket();
                StudentAnswer answer = work.Repository<StudentAnswer>().GetList(p => p.QuestionnaireId == QuestionnaireId && p.StudentId == stuId).FirstOrDefault();
                answer.Suggest = suggest;
                answer.Submit = true;
                work.Repository<StudentAnswer>().Update(answer);
                work.Save();
                return Json(new { success = true, Message = "提交成功！感谢您的问答" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}
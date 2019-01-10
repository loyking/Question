using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Model;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class QuestionController : Controller
    {
        // GET: admin/Question
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<Question>().GetList().ToList();
            ViewBag.Count = work.Repository<Question>().GetCount();
            return View();
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(int questionnaireId, string context)
        {
            try
            {
                Question question = work.Repository<Question>().GetList(p => p.Context == context).FirstOrDefault();
                if (question != null)
                {
                    return Json(new { success = false, message = "添加失败，该问题内容已存在！" });
                }
                else
                {
                    Question q = new Question();
                    q.QuestionnaireId = questionnaireId;
                    q.Context = context;
                    work.Repository<Question>().Insert(q);
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
        public ActionResult UpdataQuestion(int id, string question, int questionId, string content)
        {
            Questionnaire qs = new Questionnaire();
            qs.QuestionnaireName = question;
            qs.Id = questionId;
            ViewBag.Questionnaire = qs;
            Question q = new Question();
            q.Context = content;
            ViewBag.Question = q;

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdataQuestion(int id, int questionId, string content)
        {
            try
            {
                Question question = work.Repository<Question>().GetList(p => p.Context == content && p.QuestionnaireId==questionId).FirstOrDefault();
                if (question != null)
                {
                    return Json(new { success = false, message = "修改失败，该问题内容已存在！" });
                }
                else
                {
                    Question q1 = work.Repository<Question>().GetList(p => p.Context == content).FirstOrDefault();
                    if (q1 == null)
                    {
                        Question qw = work.Repository<Question>().GetEntityById(id);
                        qw.Context = content;
                        qw.QuestionnaireId = questionId;
                        work.Repository<Question>().Update(qw);
                        work.Save();
                        return Json(new { success = true, message = "修改成功！" });
                    }
                    else
                    {
                        if (q1.Context == content && q1.QuestionnaireId != questionId)
                        {
                            Question qw = work.Repository<Question>().GetEntityById(id);
                            string contents = qw.Context;
                            int questionIds = qw.QuestionnaireId;
                            qw.Context = content;
                            qw.QuestionnaireId = questionId;
                            work.Repository<Question>().Update(qw);
                            work.Save();
                            if (work.Repository<Question>().GetList(p => p.Context == content).Count() > 1)
                            {
                                Question qw1 = work.Repository<Question>().GetEntityById(id);
                                qw1.Context = contents;
                                qw1.QuestionnaireId = questionIds;
                                work.Repository<Question>().Update(qw);
                                work.Save();
                                return Json(new { success = false, message = "修改失败，该问题内容已存在！" });
                            }
                            else
                            {
                                return Json(new { success = true, message = "修改成功！" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "修改失败，该问题内容已存在！" });
                        }
                    }
                }                    
                    //Question stu = work.Repository<Question>().GetList(p => p.Id == id).FirstOrDefault();
                    //stu.QuestionnaireId = questionId;
                    //stu.Context = content;
                    //work.Repository<Question>().Update(stu);
                    //work.Save();
                    //return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }


        [HttpGet]
        public ActionResult DeleteQuestion(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    Question stu = work.Repository<Question>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<Question>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult QuestionOption()
        {
            ViewBag.Data = work.Repository<MatterItem>().GetList().ToList();
            ViewBag.Count = work.Repository<MatterItem>().GetCount();
            return View();
        }
        
        public ActionResult AddQuestionOption()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestionOptions(int questionnaireId, string answerA, string answerB, string answerC, string answerD)
        {
            try
            {
                MatterItem mi = new MatterItem();
                mi.QuestionnaireId = questionnaireId;
                mi.AnswerA = answerA;
                mi.AnswerB = answerB;
                mi.AnswerC = answerC;
                mi.AnswerD = answerD;
                work.Repository<MatterItem>().Insert(mi);
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
        public ActionResult UpdataQuestionOption(int id, string question, int questionId, string answerA, string answerB, string answerC, string answerD)
        {
            Questionnaire qs = new Questionnaire();
            qs.QuestionnaireName = question;
            qs.Id = questionId;
            ViewBag.Questionnaire = qs;
            MatterItem m = new MatterItem();
            m.AnswerA = answerA;
            m.AnswerB = answerB;
            m.AnswerC = answerC;
            m.AnswerD = answerD;
            ViewBag.MatterItem = m;

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdataQuestionOption(int id, int questionId, string answerA, string answerB, string answerC, string answerD)
        {
            try
            {
                MatterItem stu = work.Repository<MatterItem>().GetList(p => p.Id == id).FirstOrDefault();
                stu.QuestionnaireId = questionId;
                stu.AnswerA = answerA;
                stu.AnswerB = answerB;
                stu.AnswerC = answerC;
                stu.AnswerD = answerD;
                work.Repository<MatterItem>().Update(stu);
                work.Save();
                return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

        [HttpGet]
        public ActionResult DeleteMatterItem(string id)
        {
            try
            {
                string[] str = id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    MatterItem stu = work.Repository<MatterItem>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<MatterItem>().Delete(stu);
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
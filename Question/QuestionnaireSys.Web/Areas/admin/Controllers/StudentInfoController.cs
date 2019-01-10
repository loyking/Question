using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using System.Text;
using System.Data.SqlClient;
using QuestionnaireSys.Web.Areas.admin.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Security;

namespace QuestionnaireSys.Web.Areas.admin.Controllers
{
    public class StudentInfoController : Controller
    {
        //QuestionnaireSysDbContext db = new QuestionnaireSysDbContext();
        UnitOfWork work = new UnitOfWork();
        // GET: admin/StudentInfo
        public ActionResult Index()
        {
            ViewBag.Data = work.Repository<Student>().GetList().ToList();
            ViewBag.Count = work.Repository<Student>().GetCount();
            return View();
        }

        public ActionResult AddStudentInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStuInfo(string stuName, string moble, string cardId, int classId)
        {
            try
            {
                Student s= work.Repository<Student>().GetList(p => p.CardId == cardId || p.Mobile == moble).FirstOrDefault();
                if (s != null)
                {
                    return Json(new { success = false, message = "添加失败，该身份证或电话号已存在！" });
                }
                else
                {
                    Student stu = new Student();
                    stu.StudentName = stuName;
                    stu.Mobile = moble;
                    stu.CardId = cardId;
                    stu.JxClassId = classId;
                    string pwd = cardId.Substring(6,8);
                    pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
                    stu.LoginPwd = pwd;
                    stu.IsDisabled = false;
                    work.Repository<Student>().Insert(stu);
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
                Student stu = work.Repository<Student>().GetEntityById(id);
                pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
                stu.LoginPwd = pwd;
                work.Repository<Student>().Update(stu);
                work.Save();
                return Json(new { success = true, message = "修改成功！" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message="修改失败："+ex.Message });
                throw;
            }
        }

        [HttpGet]
        public ActionResult UpdataStudentInfo(int id, string name,string Mobile,string CardId,string ClasseName,int ClassId)
        {
            Student stu = new Student();
            stu.StudentName = name;
            stu.Mobile = Mobile;
            stu.CardId = CardId;
            ViewBag.Student = stu;
            Classe cl = new Classe();
            cl.ClasseName = ClasseName;
            cl.Id = ClassId;
            ViewBag.Classe = cl;

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdataStudentInfo(int id,string stuName, string moble, string cardId, int classId)
        {
            try
            {
                Student stus = work.Repository<Student>().GetList(p => p.Mobile == moble && p.JxClassId==classId).FirstOrDefault();
                if (stus != null)
                {
                    return Json(new { success = false, message = "修改失败，该电话号已存在！" });
                }
                else
                {
                    Student s1 = work.Repository<Student>().GetList(p => p.Mobile == moble).FirstOrDefault();
                    if (s1 == null)
                    {
                        Student s = work.Repository<Student>().GetEntityById(id);
                        s.StudentName = stuName;
                        s.Mobile = moble;
                        s.CardId = cardId;
                        s.JxClassId = classId;
                        work.Repository<Student>().Update(s);
                        work.Save();
                        return Json(new { success = true, message = "修改成功！" });
                    }
                    else
                    {
                        if (s1.Mobile == moble && s1.JxClassId != classId)
                        {
                            Student qw = work.Repository<Student>().GetEntityById(id);
                            string sNames = qw.StudentName;
                            string cardIds = qw.CardId;
                            string mobles = qw.Mobile;
                            int classIds = qw.JxClassId;
                            qw.StudentName = stuName;
                            qw.CardId = cardId;
                            qw.StudentName = stuName;
                            qw.JxClassId = classId;
                            work.Repository<Student>().Update(qw);
                            work.Save();
                            if (work.Repository<Student>().GetList(p => p.Mobile == moble).Count() > 1)
                            {
                                Student qw1 = work.Repository<Student>().GetEntityById(id);
                                qw1.StudentName = sNames;
                                qw1.CardId = cardIds;
                                qw1.Mobile = mobles;
                                qw1.JxClassId = classIds;
                                work.Repository<Student>().Update(qw1);
                                work.Save();
                                return Json(new { success = false, message = "修改失败，该电话号已存在！" });
                            }
                            else
                            {
                                return Json(new { success = true, message = "修改成功！" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "修改失败，该电话号已存在！" });
                        }
                    }
                }
                //Student stu = work.Repository<Student>().GetList(p => p.Id == id).FirstOrDefault();
                //stu.StudentName = stuName;
                //stu.Mobile = moble;
                //stu.CardId = cardId;
                //stu.JxClassId = classId;
                //work.Repository<Student>().Update(stu);
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
        public ActionResult DeleteStudentInfo(string id)
        {
            try
            {
                string[] str=id.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    Student stu = work.Repository<Student>().GetEntityById(int.Parse(str[i].ToString()));
                    work.Repository<Student>().Delete(stu);
                    work.Save();
                }
                return Json(new { success = true },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false ,message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Cartogram()
        {
            List<Classe> model  = work.Repository<Classe>().GetList(p => p.IsGraduate != true).ToList();
            ViewBag.JxClassInfo = model;
            return View();
        }
        [HttpPost]
        public ActionResult EditIsDisabled(string id,int isZeroOrOne)
        {
            try
            {
                string Id = id;
                Student stu = work.Repository<Student>().GetEntityById(int.Parse(Id.ToString()));
                if (isZeroOrOne == 0)
                {
                    stu.IsDisabled = true;
                }
                else
                {
                    stu.IsDisabled = false;
                }
                work.Repository<Student>().Update(stu);
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
        public ActionResult QuestionnaireInfo(int id)
        {
            List<Questionnaire> question = work.Repository<Questionnaire>().GetList(p => p.ClassesId == id).Select(p=>new Questionnaire(){ Id=p.Id, QuestionnaireName = p.QuestionnaireName}).ToList();
            if (question.Count!=0)
            {
                return Json(new { success = true,data = question, length = question.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false,Message="该班级还未有要问答的问卷" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult StatisticsQuestionInfo(int JxClassId, int QuestionId)
        {
            SqlParameter[] para = {
                new SqlParameter("@QuestionnaireId",QuestionId),
                new SqlParameter("@ClassID",JxClassId),
                new SqlParameter("@ClassSumNumber",SqlDbType.Int),
                new SqlParameter("@finishSumNumber",SqlDbType.Int)
            };
            para[para.Length - 2].Direction = ParameterDirection.Output;
            para[para.Length - 1].Direction = ParameterDirection.Output;
            List<StatisticsQuestion> questionInfo = work.QueryByProc<StatisticsQuestion>("proc_StatisticsQuestion", para);
            int ClassSumNumber = (int)para[para.Length - 2].Value;
            int finishSumNumber = (int)para[para.Length - 1].Value;

            if (questionInfo.Count != 0)
            {
                MatterItem Items= work.Repository<MatterItem>().GetList(p => p.QuestionnaireId == QuestionId).Select(p=> new MatterItem(){ AnswerA= p.AnswerA, AnswerB=p.AnswerB, AnswerC=p.AnswerC, AnswerD=p.AnswerD }).FirstOrDefault();
                List<string> Context = new List<string>();
                List<int> AnswerA = new List<int>();
                List<int> AnswerB = new List<int>();
                List<int> AnswerC = new List<int>();
                List<int> AnswerD = new List<int>();
                foreach (StatisticsQuestion item in questionInfo)
                {
                    Context.Add(item.Context);
                    AnswerA.Add(item.AnswerA);
                    AnswerB.Add(item.AnswerB);
                    AnswerC.Add(item.AnswerC);
                    AnswerD.Add(item.AnswerD);
                }
                return Json(new { success = true, MatterItems = Items, ClassSumNumber = ClassSumNumber, finishSumNumber = finishSumNumber, Context= Context, AnswerA= AnswerA, AnswerB= AnswerB, AnswerC= AnswerC, AnswerD= AnswerD }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "该班级该问卷还未有人回答！" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
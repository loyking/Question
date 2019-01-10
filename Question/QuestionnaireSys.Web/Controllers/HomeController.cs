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
using QuestionnaireSys.Web.Tools;
using iShare.ApiData;


namespace QuestionnaireSys.Web.Controllers
{
    public class Test
    {
        public string name { get; set; }

        public string sex { get; set; }
    }


    public class HomeController : Controller
    {
        private UnitOfWork work = new UnitOfWork();

        public ActionResult Index()
        {
            Test test = new Test();
            test.name = "哈哈";
            test.sex = "男";

            AnalysisJson.JsonToModel<Test>(JsonConvert.SerializeObject(test));


            List<List<string>> s = new List<List<string>>();
            LoginUser StuUser= Tools.Tools.GetLoginUserByTicket(User.Identity);
            if (StuUser != null && StuUser.UserType=="student")
            {
                dynamic dy = StuUser.User as dynamic;
                int id = dy.Id;     //用户id
                
                SqlParameter[] para = {
                    new SqlParameter("@StudentId",id)
                };
                List<QuestionInfo> questionInfo = work.QueryByProc<QuestionInfo>("proc_QuestionInfo", para);
                ViewBag.QuestionInfo = questionInfo.Where(p => p.state != "已完成");
            }
            return View();
        }
    }
}
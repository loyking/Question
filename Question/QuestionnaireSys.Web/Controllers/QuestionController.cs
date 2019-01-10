using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using QuestionnaireSys.Web.Models;
using Newtonsoft.Json;

namespace QuestionnaireSys.Web.Controllers
{

    public class QuestionController : Controller
    {
        private UnitOfWork uw = new UnitOfWork();

        public ActionResult Index()
        {
            
            return View();
        }





    }
}
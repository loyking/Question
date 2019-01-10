using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSys.Web.Models
{
    //题目信息与作答详情
    public class QuestionItem
    {
        public int QuestionId { get; set; } //问题id
        public string QuestionContext { get; set; } //问题
    }
}
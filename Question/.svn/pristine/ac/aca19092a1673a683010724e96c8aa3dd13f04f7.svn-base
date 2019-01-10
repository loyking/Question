using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSys.Web.Models
{
    /// <summary>
    /// 存储过程
    /// </summary>
    public class QuestionInfo
    {
        public int Id { get; set; } //问卷id
        public int QuestionnaireTypeId { get; set; }  //板块id
        public string QuestionnaireTypeName { get; set; }   //问卷类型名称
        public string QuestionnaireName { get; set; } //问卷名称
        public string UserName { get; set; } //问卷发表者名称
        public DateTime CreateDate { get; set; }    //创建时间（已完成与未完成时间为作答时间，未开始时间为问卷发表时间）
        public string state { get; set; }   //状态（已完成，未完成，未开始）
    }
}
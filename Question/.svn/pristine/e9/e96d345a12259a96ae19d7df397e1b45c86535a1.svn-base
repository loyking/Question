using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 学生答题表
    /// </summary>
   public class StudentAnswer : EntiyBase
    {
        public int StudentId { get; set; }  //学员id
        public int QuestionnaireId { get; set; }    //问卷表id
        public DateTime CreateDate { get; set; }        //填写时间
        public string Suggest { get; set; }             //学员意见
        public int AdminUserId { get; set; }            //教师ID
        public string TeacherSuggest { get; set; }      //老师建议
        public bool Submit { get; set; }            //是否提交

        public virtual Student Student { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
        public virtual AdminUser AdminUser { get; set; }
        public virtual ICollection<StudentAnswerDetail> StudentAnswerDetail { get; set; }
    }
}

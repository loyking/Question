using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 问题详情
    /// </summary>
    public class StudentAnswerDetail : EntiyBase
    {
        public int StudentAnswerId { get; set; }    //答题表Id
        public int QuestionId { get; set; }     //问题表ID
        public string AnswerOptionContext { get; set; } //学生选的项内容
        public virtual StudentAnswer StudentAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}

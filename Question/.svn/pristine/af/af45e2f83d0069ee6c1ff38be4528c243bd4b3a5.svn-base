using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 问卷表
    /// </summary>
     public class Questionnaire : EntiyBase
    {
        public int ClassesId { get; set; }  //班级id
        public int AdminUserId { get; set; }        //管理员id
        public string QuestionnaireName { get; set; }       //问卷名称
        public DateTime CreateDate { get; set; }        //创建日期
        public bool IsEnd { get; set; }     //是否关闭
        public int QuestionnaireTypeId { get; set; }    //问卷类型Id
        public string Solution { get; set; }            //解决方案
        public virtual QuestionnaireType QuestionnaireType { get; set; }
        public virtual Classe Classes { get; set; }  
        public virtual AdminUser AdminUser { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswer { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 问卷类型
    /// </summary>
        public class QuestionnaireType:EntiyBase
    {
        public string QuestionnaireName { get; set; }   //问卷类型名称
        public int UserRoleId { get; set; }     //角色
        public UserRole UserRole { get; set; }
        public virtual ICollection<Questionnaire> Questionnaire { get; set; }
    }
}

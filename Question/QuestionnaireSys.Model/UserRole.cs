using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 用户角色
    /// </summary>
   public  class UserRole : EntiyBase
    {
        //RoleName 角色名称       nvarchar（15）
        public string RoleName { get; set; }
        public virtual ICollection<QuestionnaireType> QuestionnaireType { get; set; }
    }
}

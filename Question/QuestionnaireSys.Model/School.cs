using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 学校类
    /// </summary>
    public class School : EntiyBase
    {
        public string SchoolName { get; set; }   //学校名称
        public virtual ICollection<Classe> JxClass { get; set; }
    }

}

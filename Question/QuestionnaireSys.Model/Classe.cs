using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 班级
    /// </summary>
        public class Classe : EntiyBase
    {
        public int SchoolId { get; set; }    //学校ID
        public string ClasseName { get; set; }  //班级名称
        public bool IsGraduate { get; set; }    //是否毕业
        public virtual School School { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}

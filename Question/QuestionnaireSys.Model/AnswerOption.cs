using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 学生选的项
    /// </summary>
   public class AnswerOption : EntiyBase
    {
        //Id int类型，主键，自动增长
        //Context 学生选的项 nvarchar（max）
        public string Context { get; set; }
    }
}

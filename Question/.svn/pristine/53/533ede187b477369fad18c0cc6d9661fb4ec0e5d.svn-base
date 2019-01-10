using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 学员信息
    /// </summary>
     public class Student : EntiyBase
    {
        public string StudentName { get; set; } //学员姓名
        public string Mobile { get; set; }      //电话号码
        public string CardId { get; set; }      //身份证号
        public string LoginPwd { get; set; }    //登录密码
        public int JxClassId { get; set; }      //班级Id
        public bool IsDisabled { get; set; }    //是否禁用
        [JsonIgnore]
        public virtual Classe JxClass { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentAnswer> StudentAnswer { get; set; }
    }
}

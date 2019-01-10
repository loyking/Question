using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuestionnaireSys.Model
{
    /// <summary>
    /// 管理员信息
    /// </summary>
   public  class AdminUser : EntiyBase
    {

        public string LoginName { get; set; }       //账号
        public string LoginPwd { get; set; }        //密码
        public string UserName { get; set; }        //用户名称
        public int DepartId { get; set; }           //部门
        public int UserRoleId { get; set; }         //角色
        public bool IsDisabled { get; set; }        //是否禁止登录

        [JsonIgnore]
        public virtual UserRole UserRole { get; set; }
        [JsonIgnore]
        public virtual Depart Depart { get; set; }
        [JsonIgnore]
        public virtual ICollection<Questionnaire> Questionnaire { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;
using QuestionnaireSys.Model;

namespace QuestionnaireSys.DAL
{
    public interface IRespository<TEntity> where TEntity : EntiyBase
    {        
        void Insert(TEntity model);
        void Delete(TEntity model);
        void Delete(object id);
        void Update(TEntity model);
        TEntity GetEntityById(object id);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where = null);
        IEnumerable<TEntity> GetPageList(Expression<Func<TEntity, bool>> where = null, string order = "", int pageIndex = 1, int pageSize = 10);
        int GetCount(Expression<Func<TEntity, bool>> where = null);
    }
}

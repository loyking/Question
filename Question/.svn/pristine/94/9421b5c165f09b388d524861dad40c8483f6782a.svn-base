using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace QuestionnaireSys.DAL
{
    public class GenericRespository<TEntity> : IRespository<TEntity> where TEntity : EntiyBase
    {
        internal QuestionnaireSysDbContext _db;
        internal DbSet<TEntity> _dbset;
        public GenericRespository(QuestionnaireSysDbContext db)
        {
            this._db = db;
            this._dbset = this._db.Set<TEntity>();
        }

        public void Delete(TEntity model)
        {
            this._dbset.Remove(model);
        }
        public void Delete(object id)
        {
            TEntity model = this._dbset.Find(id);
            this._dbset.Remove(model);
        }

        public int GetCount(Expression<Func<TEntity, bool>> where = null)
        {
            if (where == null)
                return this._dbset.Count();
            else
                return this._dbset.Count(where);
        }

        public TEntity GetEntityById(object id)
        {
            return this._dbset.Find(id);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where = null)
        {
            if (where == null)
                return this._dbset;
            else
                return this._dbset.Where(where);
        }

        public IEnumerable<TEntity> GetPageList(Expression<Func<TEntity, bool>> where = null, string order = "", int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<TEntity> query = this._dbset;

            if (where != null)
                query = query.Where(where);
            if (order == "")
                order = "Id asc";
            query = query.OrderBy(order);
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public void Insert(TEntity model)
        {
            this._dbset.Add(model);
        }

        public void Update(TEntity model)
        {
            DbEntityEntry<TEntity> entry = this._db.Entry(model);
            entry.State = EntityState.Modified;
        }
    }
}

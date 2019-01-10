using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSys.Model;
using QuestionnaireSys.DAL;
using System.Data.SqlClient;

namespace QuestionnaireSys.DAL
{
    public class UnitOfWork:IDisposable
    {
        private QuestionnaireSysDbContext _db = null;
        public UnitOfWork()
        {
            this._db = new QuestionnaireSysDbContext();
        }

        //private GenericRespository<User> userRespository;

        //public GenericRespository<User> UserRespository
        //{
        //    get
        //    {
        //        if (this.userRespository == null)
        //        {
        //            this.userRespository = new GenericRespository<User>(this._db);
        //        }
        //        return this.userRespository;
        //    }
        //}


        private Dictionary<Type, object> respositories = new Dictionary<Type, object>();

        public IRespository<TEntity> Repository<TEntity>()where TEntity : EntiyBase
        {
            IRespository<TEntity> res = null;
            if (respositories.ContainsKey(typeof(TEntity)))
            {
                 res = respositories[typeof(TEntity)] as IRespository<TEntity>;
            }
            else
            {
                res = new GenericRespository<TEntity>(this._db);
                respositories.Add(typeof(TEntity), res);
            }
            return res;
        }

        public int Save()
        {
            return this._db.SaveChanges();
        }
        public List<T> Query<T>(string sql,params SqlParameter[] paras) where T : class
        {
            return this._db.Database.SqlQuery<T>(sql, paras).ToList();
        }
        public List<T> QueryByProc<T>(string procName,params SqlParameter[] paras)where T:class
        {
            //"EXEC [dbo].[up_GetPagedPostReviews] @postId,@pageIndex,@pageSize,@total output"
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("exec [dbo].[{0}] ", procName);
            foreach (SqlParameter item in paras)
            {
                sql.Append(item.ParameterName);
                if (item.Direction == System.Data.ParameterDirection.Output)
                    sql.Append(" output");
                sql.Append(",");
            }
            if (paras.Length > 0)
                sql.Remove(sql.Length - 1, 1);
            return this._db.Database.SqlQuery<T>(sql.ToString(), paras).ToList();

        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._db.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UnitOfWork() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}

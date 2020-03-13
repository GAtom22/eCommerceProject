using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using eCommerceProject.DAL;

namespace eCommerceProject.Repository
{
    public class GenericRepository<MyEntity> : IRepository<MyEntity> where MyEntity : class
    {
        DbSet<MyEntity> _dbSet;
        private dbMyOnlineShopEntities _DBEntity;

        public GenericRepository(dbMyOnlineShopEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet= _DBEntity.Set<MyEntity>();
        }

        public void Add(MyEntity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public IEnumerable<MyEntity> GetAllRecords()
        {
            return _dbSet.ToList();
            
        }

        public int GetAllRecordsCount()
        {
            return _dbSet.Count();
        }

        public IQueryable<MyEntity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public MyEntity GetFirstOrDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public MyEntity GetFirstOrDefaultByParameter(Expression<Func<MyEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<MyEntity> GetListParameter(Expression<Func<MyEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<MyEntity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<MyEntity, bool>> wherePredict, Expression<Func<MyEntity, int>> orderByPredict)
        {
            if(wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<MyEntity> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _DBEntity.Database.SqlQuery<MyEntity>(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Database.SqlQuery<MyEntity>(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<MyEntity, bool>> wherePredict, Action<MyEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(MyEntity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            _DBEntity.SaveChanges();
        }

        public void RemoveByWhereClause(Expression<Func<MyEntity, bool>> wherePredict)
        {
            MyEntity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeByWhereClause(Expression<Func<MyEntity, bool>> wherePredict)
        {
            List<MyEntity> entity = _dbSet.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        public void Update(MyEntity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<MyEntity, bool>> wherePredict, Action<MyEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

    }
}
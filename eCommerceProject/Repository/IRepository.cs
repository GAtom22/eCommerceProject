using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace eCommerceProject.Repository
{
    public interface IRepository<MyEntity> where MyEntity : class
    {
        IEnumerable<MyEntity> GetAllRecords();
 
        IQueryable<MyEntity> GetAllRecordsIQueryable();
        int GetAllRecordsCount();

        void Add(MyEntity entity);
        void Update(MyEntity entity);
        void UpdateByWhereClause(Expression<Func<MyEntity, bool>> wherePredict, Action<MyEntity> ForEachPredict);
        MyEntity GetFirstOrDefault(int recordId);
        void Remove(MyEntity entity);
        void RemoveByWhereClause(Expression<Func<MyEntity, bool>> wherPredict);
        void RemoveRangeByWhereClause(Expression<Func<MyEntity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<MyEntity, bool>> wherePredict, Action<MyEntity> ForEachPredict);
        MyEntity GetFirstOrDefaultByParameter(Expression<Func<MyEntity,bool>> wherePredict);
        IEnumerable<MyEntity> GetListParameter(Expression<Func<MyEntity,bool>> wherePredict);
        IEnumerable<MyEntity> GetResultBySqlProcedure(string query, params object[] parameters);
        IEnumerable<MyEntity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<MyEntity, bool>> wherePredict, Expression<Func<MyEntity, int>> orderByPredict);

    }
}
using APIBanco.InfraEstrutura.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APIBanco.InfraEstrutura.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool tracking = false);
        TEntity GetID(int id, bool include = false);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool tracking = false);
        IQueryable<TEntity> GetListByPredicate(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool tracking = false);
        IQueryable<TEntity> GetListByPredicateSkipTake(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, bool tracking = false, int skip = 0, int take = 0);
        bool Add(TEntity entity, bool AutoSave = true);
        bool AddRange(TEntity[] entities, bool AutoSave = true);
        bool Update(TEntity entity);
        bool UpdateRange(TEntity[] entities);
        bool Delete(int id);
        bool DeleteByPredicate(Expression<Func<TEntity, bool>> predicate);
        public bool Delete(TEntity entity);
        void Save();
        public void DetachObject(TEntity obj);
        OABContext GetDBContext();

        public void StartTransacion();
        public void CommitTransaction();
    }
}

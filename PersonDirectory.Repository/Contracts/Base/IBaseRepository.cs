using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersonDirectory.Repository.Contracts.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>

    // შენიშვნა: ზოგიერთი ფუნქციის პარამეტრი წარმოადგენს EF Core - ს სპეციფიკაციას, ვიცი რომ იდეურად არასწორია, თუმცა ასე ვამჯობინე ერთიდაიგივე კოდის მრავალჯერადად გამოყენებისთვის

    public interface IBaseRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        void Add(TDto entityDto, out TEntity dbentity);
        void AddRange(IEnumerable<TDto> entityDtos);
        void Remove(object ID);
        void RemoveRange(IEnumerable<object> IDes);
        TDto Find(Expression<Func<TEntity, bool>> filterPredicate = null, params Expression<Func<TEntity, object>>[] includes);
        TDto Find(object ID);
        TEntity SingleEntity(object ID);
        TEntity SingleEntity(Expression<Func<TEntity, bool>> filterPredicate, params Expression<Func<TEntity, object>>[] includes);
        List<TDto> Get(Expression<Func<TEntity, bool>> filterPredicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByPredicate = null, int? skipAccessorPredicate = null, int? takeAccessorPredicate = null);
        void Update(TEntity entity);
        bool Any(Expression<Func<TEntity, bool>> filterPredicate = null);
        bool All(Expression<Func<TEntity, bool>> filterPredicate = null);

    }
}

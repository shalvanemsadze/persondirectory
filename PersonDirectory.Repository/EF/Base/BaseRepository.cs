using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data.Models;
using PersonDirectory.Repository.Contracts.Base;
using PersonDirectory.Shared.Helper_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersonDirectory.Repository.EF.Base
{
    public class BaseRepository<TEntity, TDto> : IBaseRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        protected DbSet<TEntity> DataBaseSet { get; set; }
        public PersonDirectoryContext Context { get; set; }

        protected Mapper Mapper = AutoMapperConfiguration.Mapper;

        public BaseRepository(PersonDirectoryContext context)
        {
            this.Context = context;
            this.DataBaseSet = context.Set<TEntity>();
        }

        public void Add(TDto entityDto, out TEntity dbentity)
        {
            var entity = Mapper.Map<TDto, TEntity>(entityDto);
            this.DataBaseSet.Add(entity);
            dbentity = entity;
        }

        public void AddRange(IEnumerable<TDto> entityDtos)
        {
            IEnumerable<TEntity> entities = Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(entityDtos);
            this.DataBaseSet.AddRange(entities);
        }

        public void Remove(object ID)
        {
            TEntity entity = this.DataBaseSet.Find(ID);
            this.DataBaseSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<object> IDes)
        {
            IEnumerable<TEntity> entities = (IEnumerable<TEntity>)this.DataBaseSet.Find(IDes);
            this.DataBaseSet.RemoveRange(entities);
        }

        public TDto Find(Expression<Func<TEntity, bool>> filterPredicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DataBaseSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query.Include(include);

            if (filterPredicate != null)
                return Mapper.Map<TEntity, TDto>(query.FirstOrDefault(filterPredicate));

            return Mapper.Map<TEntity, TDto>(query.FirstOrDefault());
        }

        public TDto Find(object ID)
        {
            TEntity entity = this.DataBaseSet.Find(ID);
            return Mapper.Map<TEntity, TDto>(entity);
        }

        public TEntity SingleEntity(object ID)
        {
            return this.DataBaseSet.Find(ID);
        }

        List<TDto> IBaseRepository<TEntity, TDto>.Get(
            Expression<Func<TEntity, bool>> filterPredicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByPredicate,
            int? skipAccessorPredicate,
            int? takeAccessorPredicate)
        {
            IQueryable<TEntity> query = this.DataBaseSet;

            if (filterPredicate != null)
                query = query.Where(filterPredicate);

            if (orderByPredicate != null)
                query = orderByPredicate(query);

            if (skipAccessorPredicate != null)
                query = query.Skip(skipAccessorPredicate.Value);

            if (takeAccessorPredicate != null)
                query = query.Take(takeAccessorPredicate.Value);

            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(query).ToList();
        }

        public void Update(TEntity entity)
        {
            this.DataBaseSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public bool Any(Expression<Func<TEntity, bool>> filterPredicate = null)
        {
            if (filterPredicate == null)
                return this.DataBaseSet.Any();

            return this.DataBaseSet.Any(filterPredicate);
        }

        public bool All(Expression<Func<TEntity, bool>> filterPredicate = null)
        {
            if (filterPredicate == null)
                return default(bool);

            return this.DataBaseSet.All(filterPredicate);
        }


    }

}

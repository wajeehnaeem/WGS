using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using WGS;
using WGS.Models;

namespace Repository
{
    public interface IGenericDataRepository<T> where T : class
    {
        void Add(params T[] items);
        Task AddAsync(params T[] items);
        List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);
        List<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<List<T>> GetListAsync(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingleAsync(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Remove(params T[] items);
        Task RemoveAsync(params T[] items);
        bool ChechIfExists(Expression<Func<T, Boolean>> predicate);
        void Update(params T[] items);
        Task UpdateAsync(params T[] items);
    }

    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        private readonly WgsDbContext _context;

        public bool ChechIfExists(Expression<Func<T, Boolean>> predicate )
        {
            return _context.Set<T>().Any(predicate);
        }

        public GenericDataRepository()
        {
            _context = Helpers.Context;
        }

      
        public virtual async Task AddAsync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
            await _context.SaveChangesAsync();
        }

        //made copy
        public virtual void Add(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Added;
            }
            _context.SaveChanges();
        }

        public virtual Task<List<T>> GetListAsync(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return Task.Run(() =>
            {
                List<T> list;

                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (var navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty);

                list = dbQuery
                    .Where(where).ToList();

                return list;
            });
        }

        //made copy 
        public virtual List<T> GetList(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            list = dbQuery
                .Where(where).ToList();

            return list;
        }

        public virtual Task<T> GetSingleAsync(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return Task.Run(() =>
            {
                T item = null;

                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (var navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty);

                item = dbQuery
                    //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause

                return item;
            });
        }

        //made copy
        public virtual T GetSingle(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            item = dbQuery
                //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }


        public virtual async Task RemoveAsync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }

        //made copy
        public virtual void Remove(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }


        public virtual Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            return Task.Run(() =>
            {
                List<T> list;

                IQueryable<T> dbQuery = _context.Set<T>();

                //Apply eager loading
                foreach (var navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty);

                list = dbQuery
                    .ToList();

                return list;
            });
        }

        //Made copy
        public virtual List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            list = dbQuery
                .ToList();

            return list;
        }

        public virtual async Task UpdateAsync(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        //made copy
        public virtual void Update(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
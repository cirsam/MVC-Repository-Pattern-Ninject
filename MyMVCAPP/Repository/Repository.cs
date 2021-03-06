﻿using MyMVCAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MyMVCAPP.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal MyDbContext context;
        internal DbSet<TEntity> dbSet;


        public Repository(MyDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;

            return query.ToList();
        }

        public virtual TEntity GetByID(int? id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(int? id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
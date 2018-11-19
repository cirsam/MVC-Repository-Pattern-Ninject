using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMVCAPP.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(int? id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(int? id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Dispose();
    }
}

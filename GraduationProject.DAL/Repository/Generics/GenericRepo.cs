using GraduationProject.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository.Generics
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        IEnumerable<TEntity> IGenericRepo<TEntity>.GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        TEntity IGenericRepo<TEntity>.GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        void IGenericRepo<TEntity>.Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        void IGenericRepo<TEntity>.Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        void IGenericRepo<TEntity>.Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

    }
}

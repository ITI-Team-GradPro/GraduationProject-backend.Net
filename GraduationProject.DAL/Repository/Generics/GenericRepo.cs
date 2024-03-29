using GraduationProject.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var query = _context.Set<TEntity>(); // Get DbSet
            return await query.ToListAsync(); // Execute query asynchronously and materialize results
        }


        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);// Get DbSet of data first
            await _context.SaveChangesAsync(); // Save changes asynchronously
        }


        public async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<TEntity>> GetAllById(string Id, Expression<Func<TEntity, bool>> predicate)
        {
            var query = _context.Set<TEntity>(); // Get DbSet

            // Apply filtering with Where (builds IQueryable)
            var filteredQuery = query.Where(predicate);

            // Execute the query asynchronously and materialize results
            return await filteredQuery.ToListAsync();
        }
    }
}

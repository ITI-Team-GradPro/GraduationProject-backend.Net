using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository.Generics
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> GetAll();
        //TEntity? GetById(int id);

        //void Add(TEntity entity);
        //void Update(TEntity entity);
        //void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);

    }
}

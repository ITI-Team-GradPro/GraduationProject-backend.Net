using GraduationProject.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
{
    private readonly GP_Db _context;

    public GenericRepo(GP_Db context)
    {
        _context = context;
    }
    IEnumerable<TEntity> IGenericRepo<TEntity>.GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }
    TEntity? IGenericRepo<TEntity>.GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }
    void IGenericRepo<TEntity>.Add(TEntity TEntity)
    {
        _context.Set<TEntity>().Add(TEntity);
    }
    void IGenericRepo<TEntity>.Update(TEntity TEntity)
    {
        _context.Set<TEntity>().Update(TEntity);
    }

    void IGenericRepo<TEntity>.Delete(TEntity TEntity)
    {
        _context.Set<TEntity>().Remove(TEntity);
    }
}

using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace GraduationProject.Data.Context
{
    public class GP_Db : DbContext
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GP-Evently;Integrated Security=True;Trust Server Certificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public GP_Db(DbContextOptions<GP_Db> options) : base(options)
        {

        }



    }
}

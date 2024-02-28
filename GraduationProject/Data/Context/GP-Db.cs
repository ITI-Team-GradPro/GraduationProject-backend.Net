using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace GraduationProject.Data.Context
{
    internal class GP_Db : DbContext
    {


        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GP-Evently;Integrated Security=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }



    }
}

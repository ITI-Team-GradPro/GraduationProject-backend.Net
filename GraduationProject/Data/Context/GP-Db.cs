using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace GraduationProject.Data.Context
{
    public class GP_Db : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImgsPlace> ImagesPlaces { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GP-Evently;Integrated Security=True;Trust Server Certificate=True");
        //    //base.OnConfiguring(optionsBuilder);
        //}


        public GP_Db(DbContextOptions<GP_Db> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //to make the two columns as promary key 
        //    modelBuilder.Entity<WishList>()
        //    .HasKey(wl => new { wl.UserId, wl.PlaceId });

        //    // to make the email uniqe 
        //    modelBuilder.Entity<User>()
        //    .HasIndex(u => u.Email)
        //    .IsUnique();

        //    modelBuilder.Entity<Booking>()
        //    .HasOne(b => b.Place)
        //    .WithMany(p => p.Bookings)
        //    .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Comment>()
        //    .HasOne(c => c.Place)
        //    .WithMany(p => p.Comments)
        //    .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Review>()
        //   .HasOne(r => r.Place)
        //    .WithMany(p => p.Reviews)
        //    .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<WishList>()
        //    .HasOne(wl => wl.Place)
        //    .WithMany(p => p.WishListPlaceUsers)
        //    .OnDelete(DeleteBehavior.Restrict);


        //    modelBuilder.Entity<WishList>()
        //   .HasOne(wl => wl.User)
        //   .WithMany(u => u.WishListUserPlaces)
        //   .HasForeignKey(wl => wl.UserId)
        //   .OnDelete(DeleteBehavior.Restrict);

        //}

    }
}








using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Place> Places { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImgsPlace> ImagesPlaces { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source= ICARUSLAPTOP\\SQLEXPRESS; Initial Catalog= API_Project; Integrated Security= true; Encrypt= false");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for relationships and keys
            modelBuilder.Entity<WishList>().HasKey(wl => new { wl.UserId, wl.PlaceId });

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Place)
                .WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Place)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Place)
                .WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishList>()
                .HasOne(wl => wl.Place)
                .WithMany(p => p.WishListPlaceUsers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishList>()
                .HasOne(wl => wl.User)
                .WithMany(u => u.WishListUserPlaces)
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Place>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnedPlaces)
                .HasForeignKey(p => p.OwnerId);
        }
    }
}

//public class GP_Db : IdentityDbContext<User>
//{
//    public DbSet<User> Users { get; set; }
//    public DbSet<Place> Places { get; set; }
//    public DbSet<WishList> WishList { get; set; }
//    public DbSet<Comment> Comments { get; set; }
//    public DbSet<Review> Reviews { get; set; }
//    public DbSet<Notification> Notifications { get; set; }
//    public DbSet<Booking> Bookings { get; set; }
//    public DbSet<Category> Categories { get; set; }
//    public DbSet<ImgsPlace> ImagesPlaces { get; set; }


//    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
//    //{
//    //    //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GP-Evently;Integrated Security=True;Trust Server Certificate=True");
//    //    //base.OnConfiguring(optionsBuilder);

//    //}


//    public GP_Db(DbContextOptions<GP_Db> options) : base(options)
//    {
//    }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        //optionsBuilder.UseSqlServer(
//        //   ("Data Source=.;Initial Catalog=GP-Evently;Integrated Security=True;Trust Server Certificate=True"),
//        //    b => b.MigrationsAssembly("GraduationProject.DAL")); // Replace with your migrations assembly name
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        //to make the two columns as promary key 
//        modelBuilder.Entity<WishList>()
//        .HasKey(wl => new { wl.UserId, wl.PlaceId });

//        // to make the email uniqe 
//        modelBuilder.Entity<User>()
//        .HasIndex(u => u.Email)
//        .IsUnique();

//        modelBuilder.Entity<Booking>()
//        .HasOne(b => b.Place)
//        .WithMany(p => p.Bookings)
//        .OnDelete(DeleteBehavior.Restrict);

//        modelBuilder.Entity<Comment>()
//        .HasOne(c => c.Place)
//        .WithMany(p => p.Comments)
//        .OnDelete(DeleteBehavior.Restrict);

//        modelBuilder.Entity<Review>()
//       .HasOne(r => r.Place)
//        .WithMany(p => p.Reviews)
//        .OnDelete(DeleteBehavior.Restrict);

//        modelBuilder.Entity<WishList>()
//        .HasOne(wl => wl.Place)
//        .WithMany(p => p.WishListPlaceUsers)
//        .OnDelete(DeleteBehavior.Restrict);


//        modelBuilder.Entity<WishList>()
//       .HasOne(wl => wl.User)
//       .WithMany(u => u.WishListUserPlaces)
//       .HasForeignKey(wl => wl.UserId)
//       .OnDelete(DeleteBehavior.Restrict);


//        modelBuilder.Entity<Place>()
//            .HasOne(p => p.Owner)
//            .WithMany(u => u.OwnedPlaces)
//            .HasForeignKey(p => p.OwnerId);




//    }

//}





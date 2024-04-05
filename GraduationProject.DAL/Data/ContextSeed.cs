using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace GraduationProject.DAL.Data
{
    public static class ContextSeed
    {
        public async static void SeedAsync(ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Categories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../GraduationProject.DAL/Data/DataSeed/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                foreach (var category in categories)
                {
                    context.Set<Category>().Add(category);
                }
                await context.SaveChangesAsync();
            }
            if (context.Places.Count() == 0)
            {
                var placesData = File.ReadAllText("../GraduationProject.DAL/Data/DataSeed/Places.json");
                var places = JsonSerializer.Deserialize<List<Place>>(placesData);
                foreach (var x in places)
                {
                    context.Set<Place>().Add(x);
                }
                await context.SaveChangesAsync();
            }
            if (context.Bookings.Count() == 0)
            {
                var bookingsData = File.ReadAllText("../GraduationProject.DAL/Data/DataSeed/Bookings.json");
                var bookings = JsonSerializer.Deserialize<List<Booking>>(bookingsData);
                foreach (var x in bookings)
                {
                    context.Set<Booking>().Add(x);
                }
                await context.SaveChangesAsync();
            }
            if (context.Reviews.Count() == 0)
            {
                var reviewsData = File.ReadAllText("../GraduationProject.DAL/Data/DataSeed/Reviews.json");
                var reviews = JsonSerializer.Deserialize<List<Review>>(reviewsData);
                foreach (var x in reviews)
                {
                    context.Set<Review>().Add(x);
                }
                await context.SaveChangesAsync();
            }

        }
    }
}

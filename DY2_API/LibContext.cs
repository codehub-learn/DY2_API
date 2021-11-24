using DY2_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace DY2_API
{
    public class LibContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }

        public LibContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = -1,
                    FirstName = "Umberto",
                    LastName = "Eco"
                },
                new Author()
                {
                    Id = -2,
                    FirstName = "Haruki",
                    LastName = "Murakami"
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = -1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "jsmith@example.com",
                });

            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = -1,
                    Name = "The Name of the Rose",
                    Publisher = "Fixed House",
                    AuthorId = -1,
                    RentedToId = -1
                },
                new Book()
                {
                    Id = -2,
                    Name = "The Limits of Interpretation",
                    Publisher = "Fixed House",
                    AuthorId = -1
                },
                new Book()
                {
                    Id = -3,
                    Name = "Kafka on the Shore",
                    Publisher = "Arctic Editions",
                    AuthorId = -2
                });
        }
    }
}

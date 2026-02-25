using Microsoft.EntityFrameworkCore;
using books.Models;

namespace books.Data;

public class BooksDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    // 🔥 ОБЯЗАТЕЛЬНЫЙ конструктор для миграций
    public BooksDbContext(DbContextOptions<BooksDbContext> options)
        : base(options)
    {
    }

    // 🔥 Конструктор по умолчанию — для приложения
    public BooksDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite("Data Source=books.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Война и Мир", Author = "Толстой", Genre = "Роман", Year = 1867, Count = 5 },
            new Book { Id = 2, Title = "1984", Author = "Оруэлл", Genre = "Антиутопия", Year = 1949, Count = 3 },
            new Book { Id = 3, Title = "Преступление и Наказание", Author = "Достоевский", Genre = "Роман", Year = 1866, Count = 4 }
        );
    }
}
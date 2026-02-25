using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace books.Data;

public class BooksDbContextFactory : IDesignTimeDbContextFactory<BooksDbContext>
{
    public BooksDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
        optionsBuilder.UseSqlite("Data Source=books.db");
        return new BooksDbContext(optionsBuilder.Options);
    }
}
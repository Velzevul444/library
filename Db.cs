using books.Data;

namespace books;

public static class Db
{
    public static BooksDbContext Context { get; } = new BooksDbContext();
}

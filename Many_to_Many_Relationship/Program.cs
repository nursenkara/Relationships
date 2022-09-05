
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");
#region Default Convention
// İki entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız. (ICollection, List)
//public class Book
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Author> Authors { get; set; }
//}
//public class Author
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Book> Books { get; set; } 
//}
#endregion
#region Data Annotations
//cross table manuel olarak oluşturulmak zorundadır.
//public class Book
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<AuthorBook> Authors { get; set; }
//}
//Cross Table
//public class AuthorBook
//{
//    public int BookId { get; set; }
//    public int AuthorId { get; set; }
//    public Book Book { get; set; }
//    public Author Author { get; set; }
//}
//public class Author
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<AuthorBook> Books { get; set; }
//}
#endregion
#region Fluent API
public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AuthorBook> Authors { get; set; }
}
//Cross Table
public class AuthorBook
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AuthorBook> Books { get; set; }
}
#endregion
public class EKitapContext: DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=intern-db.cjq6i1xxy6zz.eu-central-1.rds.amazonaws.com;Database=EKitapDB;Uid=sa;Password=VKynM2xF5P9SLFpdHAJk144X0OyyMTFq7fXu9Vw3tBVXeHYY8S");
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<AuthorBook>()
    //        .HasKey(ab => new
    //        {
    //            ab.AuthorId,
    //            ab.BookId
    //        });
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ab => new
            {
                ab.AuthorId,
                ab.BookId
            });
        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Book)
            .WithMany(ab => ab.Authors)
            .HasForeignKey(ab => ab.BookId);
        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Author)
            .WithMany(ab => ab.Books)
            .HasForeignKey(ab => ab.AuthorId);

    }
}
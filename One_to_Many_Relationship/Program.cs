
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");
#region Default Convention
//Dependent entity
// Default convention yönteminde bire çok ilişkiyi kurarken foreign key kolonuna karşılık gelen bir property tanımlamak mecburiyetinde değiliz.
// Eğer tanımlamazsak EF Core bunu kendisi tanımlayacak yok eğer tanımlamazsak tanımladığımızı baz alacaktır.

//public class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Departmant Departmant { get; set; }
//}
////Principal entity
//public class Departmant
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees  { get; set; }
//}
#endregion

#region Data Annotations

//Default convention yönteminde foreign key kolonuna karşılık gelen property tanımladığımızda bu property ismi temel geleneksel entity tanımlama kurallarına uymuyorsa eğer 
// data annotationslar ile müdahalede bulunabiliriz...
//Dependent entity
//public class Employee
//{
//    public int Id { get; set; }

//    [ForeignKey(nameof(Departmant))]
//    public int DepartmantId { get; set; }
//    /// <summary>
//    /// public int DId { get; set; } farklı bir isim koymak istersek de data annotationsla kullanabiliriz.
//    /// </summary>

//    public string Name { get; set; }
//    public Departmant Departmant { get; set; }
//}
////Principal entity
//public class Departmant
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees { get; set; }
//}

#endregion

#region Fluent API
public class Employee
{
    public int Id { get; set; }
    public int DId { get; set; }
    public string Name { get; set; }
    public Departmant Departmant { get; set; }
}

public class Departmant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
#endregion
public class ESirketDb : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Departmant> Departmants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=intern-db.cjq6i1xxy6zz.eu-central-1.rds.amazonaws.com;Database=ESirketDB;Uid=sa;Password=VKynM2xF5P9SLFpdHAJk144X0OyyMTFq7fXu9Vw3tBVXeHYY8S");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Departmant)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DId);
    }
}



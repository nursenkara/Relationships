
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

#region Default Convention

/// <summary>
/// Her iki entity'de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.
/// One to One ilişki türünde dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir. Bu duruma fiziksel 
/// olarak bir foreign key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.
/// Böylece foreign key' e karşılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.
/// </summary>
//Prinpical entity
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Navigation property
    public EmployeeAddress EmployeeAddress { get; set; }
}

//Dependent entity: EmployeeAddress
public class EmployeeAddress
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Address { get; set; }

    //Navigation property
    public Employee Employee { get; set; }
}
#endregion

#region Data Annotations
// Navigation propertyler tanımlanmalıdır.
// Foreign key kolonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
// Foreign key kolonu oluşturulmak zorunda değildir.
// 1'e 1 ilişkide ekstra foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entitydeki id kolonunun hem foreign key hem de primary key 
//olarak kullanmayı tercih ediyoruz ve bu duruma özen gösteriyoruz.
//public class Employee
//{

//    public int Id { get; set; }
//    public string Name { get; set; }
//    public EmployeeAddress EmployeeAddress { get; set; }
//}


//public class EmployeeAddress
//{
//    [Key,ForeignKey(nameof(Employee))]
//    public int Id { get; set; }
//    //[ForeignKey(nameof(Employee))]
//    //public int EmployeeId { get; set; } 
//    /// <summary>
//    /// daha az maliyetli hali: iki tablonun primary key alanını hem de foreign key yapmak
//    /// </summary>
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion


#region Fluent API
// Navigation propertyler tanımlanmalı!
//public class Employee
//{

//    public int Id { get; set; }
//    public string Name { get; set; }
//    public EmployeeAddress EmployeeAddress { get; set; }
//}


//public class EmployeeAddress
//{
//    public int Id { get; set; }
//    public string Address { get; set; }
//    public Employee Employee { get; set; }
//}
#endregion
public class ESirketDb : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=intern-db.cjq6i1xxy6zz.eu-central-1.rds.amazonaws.com;Database=ESirketDB;Uid=sa;Password=VKynM2xF5P9SLFpdHAJk144X0OyyMTFq7fXu9Vw3tBVXeHYY8S");
    }
    //Fluent API için
    // Modelların (entitylerin) veritabanında generate edilecek yapıları bu fonskiyon içinde konfigüre edilir.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<EmployeeAddress>()
    //        .HasKey(e => e.Id);
    //    modelBuilder.Entity<Employee>()
    //         .HasOne(e => e.EmployeeAddress)
    //         .WithOne(e => e.Employee)
    //         .HasForeignKey<EmployeeAddress>(e => e.Id);
    //}
}

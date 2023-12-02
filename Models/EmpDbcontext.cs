global using Microsoft.EntityFrameworkCore;

namespace DemoApp1.Models;

public class EmpDbcontext : DbContext
{
    public DbSet<Employee> Employees {get; set;}

    public DbSet<Department> Departments {get; set;}

    public DbSet<Admins> Admins {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Empdept.db");
        
    }
}
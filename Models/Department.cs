using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp1.Models;

  [Table("R6364DEPT")]
public class Department
{
    [Column("DEPTNO")]
    public decimal Id {get; set;}

    [Column("DNAME")]
    public string Name {get; set;}

    [Column("LOC")]
    public string Location {get; set;}

    public List<Employee> Employees {get; set;} =new();
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;
namespace DemoApp1.Models;

[Table("R24EMPLOYEE")]
public class Employee
{

    [Column("EMPNO")]
    public decimal Id {get; set;}

    [Required]
    [Column("ENAME")]
    public string Name {get; set;}

    [Column("SAL")]
    public decimal salary {get; set;}

    [Column("DEPTNO")]
    public decimal DepartmentId {get; set;}

    [Column("LOC")]
    public string Location {get; set;}
}
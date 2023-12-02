using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp1.Models;
// using System.Runtime.CompilerServices;

public class Admins{

   [Column("USERNAME")]
    public string Id{get; set; }

    [Column("PASSWORD")]
    public string Password {get; set;}

}
using System.Numerics;
using DemoApp1.Models;
public class Program
{

public static void Main(string[] args){
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();
    builder.Services.AddAuthentication()
     .AddCookie(option => option.LoginPath = "/Index");
    builder.Services.AddDbContext<DemoApp1.Models.EmpDbcontext>();
    var app = builder.Build();
    app.UseFileServer();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapDefaultControllerRoute();
    app.Run();
}
}
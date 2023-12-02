
using System.Security.Claims;
using DemoApp1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DemoApp1.Controllers;


public class HomeController : Controller
{
    [Authorize][ResponseCache(NoStore = true)]
    public IActionResult Employee([FromServices] EmpDbcontext site)
    {
        var selection = from emp in site.Employees
                        select new EmpInfo
                        {
                            EMPNO=emp.Id,
                            ENAME= emp.Name,
                            SALARY= emp.salary,
                            DEPTNO=emp.DepartmentId,
                            LOC=emp.Location
                        };
             if(selection is not null){           
                    return View(selection.ToList()); 
                }
               return NotFound(); 
    }


    [Authorize][ResponseCache(NoStore = true)]
    public IActionResult Register()
    {
        return View(new Employee());
    }
  
  
   [HttpGet]
   [HttpPost][Authorize][ResponseCache(NoStore = true)]
    public IActionResult Register([FromServices] EmpDbcontext site, Employee input)
    {
        if(ModelState.IsValid)
        {   
            var emp = site.Employees.Find(input.Id);
             if(emp is null)
            {
               // emp=input;
               site.Employees.Add(input);
            }
            site.SaveChanges();
            return RedirectToAction("Employee");
        }
        return Register();
    }
//====================================New  Department=========================================

// [HttpPost]
// [Authorize][ResponseCache(NoStore = true)]
// public IActionResult Department([FromServices] EmpDbcontext site)
    // {
        // var selection = from dept in site.Departments
                        // select new DepartmentInfo
                        // {
                            // DEPTNO=dept.Id,
                            // DNAME= dept.Name,
                            // LOC=dept.Location
                        // };
            //  if(selection is not null){           
                    // return View(selection.ToList()); 
                // }
            //    return NotFound(); 
    // }

    // [Authorize][ResponseCache(NoStore = true)]
//    public IActionResult Department()
    // {
    //  return View(new Department());
    // }
//======================================================================================================

[Authorize]
[ResponseCache(NoStore = true)]
public IActionResult Department([FromServices] EmpDbcontext site)
{
    // Assuming this is a POST method receiving data
    // You might want to change the method signature and parameter type accordingly
    // For example, [HttpPost]
    // public IActionResult Department([FromBody] SomeModel model, [FromServices] EmpDbContext site)

    // Your existing LINQ query to convert Departments to DepartmentInfo
    var selection = from dept in site.Departments
                    select new DepartmentInfo
                    {
                        DEPTNO = dept.Id,
                        DNAME = dept.Name,
                        LOC = dept.Location
                    };

    // Check if the selection is not empty before returning the view
    if (selection.Any())
    {
        return View(selection.ToList());
    }

    // If no data is found, return a NotFound result
    return NotFound();
}

// [Authorize]
// [ResponseCache(NoStore = true)]
// public IActionResult Department()
// {
    // Assuming this is a GET method to render the view
    // You can modify this method based on your requirements
    // For example, fetching a single department or initializing an empty model

    // Your existing code to return a view with an empty Department model
    // return View(new Department());
// }



//========================================== Register dept ==================================================
   
    // [Authorize][ResponseCache(NoStore = true)]
    //   public IActionResult DeptRegister()
    // {
        // return View(new DeptRegister());
    // }
    // 
    // [HttpPost][Authorize][ResponseCache(NoStore = true)]
    // public IActionResult DeptRegister([FromServices] EmpDbcontext site, Department input)
    // {   
        // if(ModelState.IsValid)
        // {   
            // var dept = site.Departments.Find(input.Id);
            //  if(dept is null)
            // {
                ////dept=input;
            //    site.Departments.Add(input);
            // }
            // site.SaveChanges();
            // return RedirectToAction("Department");
        // }
        // return Register();
    // }
  //============================================================================================





[Authorize]
[ResponseCache(NoStore = true)]
public IActionResult DeptRegister()
{
    return View(new Department());
}
[HttpPost]
public IActionResult DeptRegister([FromServices] EmpDbcontext site, Department input)
{
    if (ModelState.IsValid)
    {
        var dept = site.Departments.Find(input.Id);
        if (dept is null)
        {
            site.Departments.Add(input);
        }
// 
        site.SaveChanges();
        return RedirectToAction("Department");
    }

    return View("DeptRegister", new DemoApp1.Models.Department());
}
// 
// 


//==================================== Remove Employee =========================================

    [Authorize][ResponseCache(NoStore = true)]
    public IActionResult RemoveEmp()
    {
        return View(new Employee());
    }


    [HttpPost][Authorize][ResponseCache(NoStore = true)]
    public IActionResult RemoveEmp([FromServices] EmpDbcontext site, Employee input)
    {
            var emp = site.Employees.Find(input.Id);
             if(emp is not null)
            {
               site.Employees.Remove(emp);
            //    site.SaveChanges();
            //    return RedirectToAction("Index");
            }
            site.SaveChanges();
           return RedirectToAction("Employee");
    }

//================================ Remove Department===============================================
    
    // [Authorize][ResponseCache(NoStore = true)]
    // public IActionResult RemoveDept()
    // {
        // return View(new Department());
    // }

    // [HttpPost][Authorize][ResponseCache(NoStore = true)]
    // public IActionResult RemoveDept([FromServices] EmpDbcontext site, Department input)
    // {
            // var dept = site.Departments.Find(input.Id);
            //  if(dept is not null)
            // {
            //    site.Departments.Remove(dept);
                //   site.SaveChanges();
            // }
        //  
            //  return RedirectToAction("Department");
    // }

//=======================================================================================

[Authorize]
[ResponseCache(NoStore = true)]
public IActionResult RemoveDept()
{
    return View(new Department());
}

[HttpPost]
[Authorize]
[ResponseCache(NoStore = true)]
public IActionResult RemoveDept([FromServices] EmpDbcontext site, Department input)
{
    var dept = site.Departments.Find(input.Id);
    if (dept != null)
    {
        site.Departments.Remove(dept);
        site.SaveChanges();
    }
    else
    {
        // Handle the case when no department is found with the specified ID
        return NotFound();
    }

    return RedirectToAction("Department");
}


//==============================Admin login==============================================

public IActionResult Index(){
        return View();
    }
  
[HttpPost]
public IActionResult Index([FromServices] EmpDbcontext site, Admins input)
{
    if (ModelState.IsValid)
    {
        var Admins = site.Admins.Find(input.Id);
        if (Admins?.Password == input.Password)
        {
            var identity = new ClaimsIdentity("Admins");
            identity.AddClaim(new Claim(ClaimTypes.Name, input.Id));
            HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToAction("Employee"); // Use RedirectToAction instead of RedirectToPage
        }

        ModelState.AddModelError("Login", "Invalid Username or Password");
    }

    return RedirectToAction("Index");
}

}

internal class DeptRegister
{
    public DeptRegister()
    {
    }
}
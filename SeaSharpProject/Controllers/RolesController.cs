using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

//this sucks
namespace SeaSharpProject.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager; //init this below

        public RolesController(RoleManager<IdentityRole> _roleManager) 
        {
            roleManager = _roleManager; //role manager init
        }

        public IActionResult Index() //listing all roles created by the user
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]

        public IActionResult Create() 
        {
            return View();
        
        }

        [HttpPost]

        public async Task<IActionResult> Create (IdentityRole model)
        {
            if (!roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()) //if not exist
            {
                roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult(); //create
            }
            return RedirectToAction("Index");
        }

    }
}

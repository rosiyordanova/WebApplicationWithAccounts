using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplicationWithAccounts.Models;

namespace WebApplicationWithAccounts.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        { 
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async IActionResult Create(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
                if(roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "The role already exists");
            }
        }



    }
}

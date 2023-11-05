using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectoBaseComIdentity.Models;
using System.ComponentModel.DataAnnotations;
using ProjectoBaseComIdentity.Models.ViewModels;

namespace ProjectoBaseComIdentity.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // Criar nova Role
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new AppRole
                {
                    Name = name,
                    NormalizedName = name.ToUpper()
                });

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        // Eliminar alguma role
        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id) + "> inválido");

            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else Errors(result);
            }
            else
                ModelState.AddModelError("", "Nenhuma role foi encontrada");
            return View("Index", _roleManager.Roles);
        }

        // Update 
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id) + "> inválido");
            AppRole role = await _roleManager.FindByIdAsync(id?.ToString());
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        #region ErrrosRegion
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        #endregion
    }
}
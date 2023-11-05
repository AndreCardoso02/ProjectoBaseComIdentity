using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectoBaseComIdentity.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace ProjectoBaseComIdentity.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
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
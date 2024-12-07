using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BibliotheekApp.Models;
using BibliotheekWebApp.Models;

namespace BibliotheekApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Panel()
        {
            var users = _userManager.Users.ToList();

            var model = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new AdminUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsAdmin = roles.Contains("Admin")
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdminRole(string userId, bool isAdmin)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (isAdmin)
            {
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                }
            }

            return RedirectToAction(nameof(Panel));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Panel));
        }

        [HttpGet]
        public async Task<IActionResult> EditRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var model = new EditRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                AllRoles = _roleManager.Roles.Select(r => r.Name).ToList(),
                SelectedRoles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoles(EditRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.SelectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(model.SelectedRoles).ToList();

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction(nameof(Panel));
        }
    }
}

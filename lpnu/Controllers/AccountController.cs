using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using lpnu.Models;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
				string refererUrl = Request.Headers["Referer"].ToString();

				if (!string.IsNullOrEmpty(refererUrl))
				{
					return Redirect(refererUrl);
				}
				else
				{
					return RedirectToAction("Index", "Home"); // Redirect to home page after successful login
				}
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        return View();
    }

    [HttpGet]
    public IActionResult DeleteUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                ViewBag.Message = "User successfully deleted.";
            }
            else
            {
                ViewBag.Error = "Error deleting user.";
            }
        }
        else
        {
            ViewBag.Error = "User not found.";
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
		// Get the URL of the page from which the request was made
		string refererUrl = Request.Headers["Referer"].ToString();

		// If the URL is available, redirect to it, otherwise to the main page
		if (!string.IsNullOrEmpty(refererUrl))
		{
			return Redirect(refererUrl);
		}
		else
		{
			return RedirectToAction("Index", "Home");
		}
	}
}
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace lpnu.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProfessorService _professorService;
		private readonly IContactService _contactService;

		public HomeController(ILogger<HomeController> logger, IProfessorService professorService, IContactService contactService)
		{
			_logger = logger;
			_professorService = professorService;
			_contactService = contactService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public async Task<IActionResult> Contacts()
		{
			var model = await _contactService.GetAllContactsAsync();
			return View(model);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForContact()
		{
			return View("~/Views/Home/AdminActions/Create/CreateItemForContact.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForContact(CreateContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				var contact = new Contact
				{
					EducationalBuilding = model.EducationalBuilding,
					Room = model.Room,
					PhoneNumber = model.PhoneNumber,
					StuffNameCollectionSerialized = model.StuffNameCollectionSerialized
				};

				await _contactService.AddContactAsync(contact);
				return RedirectToAction(nameof(Contacts));
			}
			return View("~/Views/Home/AdminActions/Create/CreateItemForContact.cshtml", model);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForContact(int id)
		{
			var contact = await _contactService.GetContactByIdAsync(id);
			if (contact == null)
			{
				return NotFound();
			}
			return View("~/Views/Home/AdminActions/Delete/DeleteItemForContact.cshtml", contact);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForContact(int id)
		{
			await _contactService.DeleteContactAsync(id);
			return RedirectToAction(nameof(Contacts));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForContact(int id)
		{
			var contact = await _contactService.GetContactByIdAsync(id);
			if (contact == null)
			{
				return NotFound();
			}

			var model = new CreateContactViewModel
			{
				EducationalBuilding = contact.EducationalBuilding,
				Room = contact.Room,
				PhoneNumber = contact.PhoneNumber,
				StuffNameCollectionSerialized = contact.StuffNameCollectionSerialized
			};

			return View("~/Views/Home/AdminActions/Edit/EditItemForContact.cshtml", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForContact(int id, CreateContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				var contact = await _contactService.GetContactByIdAsync(id);
				if (contact == null)
				{
					return NotFound();
				}

				contact.EducationalBuilding = model.EducationalBuilding;
				contact.Room = model.Room;
				contact.PhoneNumber = model.PhoneNumber;
				contact.StuffNameCollectionSerialized = model.StuffNameCollectionSerialized;

				await _contactService.UpdateContactAsync(contact);
				return RedirectToAction(nameof(Contacts));
			}
			return View("~/Views/Home/AdminActions/Edit/EditItemForContact.cshtml", model);
		}

		[HttpPost]
		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)}
			);

			return LocalRedirect(returnUrl);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

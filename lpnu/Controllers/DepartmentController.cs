using lpnu.Data;
using lpnu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using lpnu.Services.Interfaces;

namespace lpnu.Controllers
{
    public class DepartmentController : Controller
	{
		private readonly IProfessorService _professorService;
		private readonly IStructurePostService _structurePostService;
		private readonly IPhotoService _photoService;
		private readonly IPartnerService _partnerService;

		public DepartmentController(IProfessorService professorService, IStructurePostService structurePostService, IPhotoService photoService, IPartnerService partnerService)
		{
			_professorService = professorService;
			_structurePostService = structurePostService;
			_photoService = photoService;
			_partnerService = partnerService;

		}

		// GET: Teachers
		public async Task<IActionResult> Staff(int pageIndex = 1, int pageSize = 8)
		{
			var model = await _professorService.GetProfessorsPageAsync(pageIndex, pageSize);
			if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_TeachersPartial", model);
			}
			return View(model);
		}

		// GET: Teachers/Create
		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForStaff()
		{
			return View("~/Views/Department/AdminActions/Create/CreateItemForStaff.cshtml");
		}

		// POST: Teachers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForStaff(Professor professor)
		{
			if (ModelState.IsValid)
			{
				await _professorService.AddProfessorAsync(professor);
				return RedirectToAction(nameof(Staff));
			}
			return View("~/Views/Department/AdminActions/Create/CreateItemForStaff.cshtml", professor);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForStaff(int id)
		{
			var professor = await _professorService.GetProfessorByIdAsync(id);
			if (professor == null)
			{
				return NotFound();
			}

			return View("~/Views/Department/AdminActions/Delete/DeleteItemForStaff.cshtml", professor);
		}

		[HttpPost, ActionName("DeleteConfirmed")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForStaff(int id)
		{
			await _professorService.DeleteProfessorAsync(id);
			return RedirectToAction(nameof(Staff));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForStaff(int id)
		{
			var professor = await _professorService.GetProfessorByIdAsync(id);
			if (professor == null)
			{
				return NotFound();
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForStaff.cshtml", professor);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForStaff(int id, Professor professor)
		{
			if (id != professor.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _professorService.UpdateProfessorAsync(professor);
				return RedirectToAction(nameof(Staff));
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForStaff.cshtml", professor);
		}

		public async Task<IActionResult> Structure()
		{
			var posts = await _structurePostService.GetAllStructurePostsAsync();
			var viewModel = new CreateItemForStructureViewModel
			{
				Posts = posts,
			};
			return View(viewModel);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForStructure()
		{
			return View("~/Views/Department/AdminActions/Create/CreateItemForStructure.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForStructure(StructureViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("CreateItemForStructure", model);
			}

			var professor = await _professorService.GetProfessorByFullNameAsync(model.FirstName, model.LastName, model.Patronymic);
			if (professor == null)
			{
				ViewBag.ErrorMessage = "Викладач не знайдений.";
				return View("~/Views/Department/AdminActions/Create/CreateItemForStructure.cshtml", model);
			}

			var post = new StructurePost
			{
				ProfessorName = $"{professor.LastName} {professor.FirstName} {professor.Patronymic}",
				PhotoPath = professor.PhotoPath,
				Position = model.Position,
				Description = model.Description,
				CreatedAt = DateTime.UtcNow
			};

			await _structurePostService.AddStructurePostAsync(post);
			return RedirectToAction("Structure");
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForStructure(int id)
		{
			var post = await _structurePostService.GetStructureByIdAsync(id);
			if (post == null)
			{
				return NotFound();
			}
			return View("~/Views/Department/AdminActions/Delete/DeleteItemForStructure.cshtml", post);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForStructure(int id)
		{
			await _structurePostService.DeleteStructurePostAsync(id);
			return RedirectToAction(nameof(Structure));
		}

		// GET: StructurePost/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForStructurePost(int id)
		{
			var post = await _structurePostService.GetStructureByIdAsync(id);
			if (post == null)
			{
				return NotFound();
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForStructurePost.cshtml", post);
		}

		// POST: StructurePost/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForStructurePost(int id, StructurePost post)
		{
			if (id != post.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _structurePostService.UpdateStructurePostAsync(post);
				return RedirectToAction(nameof(Structure));
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForStructurePost.cshtml", post);
		}

		public async Task<IActionResult> Gallery(int pageIndex = 1, int pageSize = 12)
		{
			var model = await _photoService.GetGalleryPageAsync(pageIndex, pageSize);
			if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_GalleryPartial", model);
			}
			return View(model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForGallery()
		{
			return View("~/Views/Department/AdminActions/Create/CreateItemForGallery.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForGallery(Photo photo)
		{
			if (ModelState.IsValid)
			{
				await _photoService.AddPhotoAsync(photo);
				return RedirectToAction(nameof(Gallery));
			}
			return View("~/Views/Department/AdminActions/Create/CreateItemForGallery.cshtml", photo);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForGallery(int id)
		{
			var photo = await _photoService.GetPhotoByIdAsync(id);
			if (photo == null)
			{
				return NotFound();
			}

			return View("~/Views/Department/AdminActions/Delete/DeleteItemForGallery.cshtml", photo);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForGallery(int id)
		{
			await _photoService.DeletePhotoAsync(id);
			return RedirectToAction(nameof(Gallery));
		}

		public async Task<IActionResult> Partners()
		{
			var model = await _partnerService.GetAllPartnersAsync();
			return View(model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForPartner()
		{
			return View("~/Views/Department/AdminActions/Create/CreateItemForPartner.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForPartner(Partner partner)
		{
			if (ModelState.IsValid)
			{
				await _partnerService.AddPartnerAsync(partner);
				return RedirectToAction(nameof(Partners));
			}
			return View("~/Views/Department/AdminActions/Create/CreateItemForPartner.cshtml", partner);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForPartner(int id)
		{
			var partner = await _partnerService.GetPartnerByIdAsync(id);
			if (partner == null)
			{
				return NotFound();
			}

			return View("~/Views/Department/AdminActions/Delete/DeleteItemForPartner.cshtml", partner);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForPartner(int id)
		{
			await _partnerService.DeletePatrnerAsync(id);
			return RedirectToAction(nameof(Partners));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForPartner(int id)
		{
			var partner = await _partnerService.GetPartnerByIdAsync(id);
			if (partner == null)
			{
				return NotFound();
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForPartner.cshtml", partner);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForPartner(int id, Partner partner)
		{
			if (id != partner.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _partnerService.UpdatePartnerAsync(partner);
				return RedirectToAction(nameof(Partners));
			}
			return View("~/Views/Department/AdminActions/Edit/EditItemForPartner.cshtml", partner);
		}
	}
}

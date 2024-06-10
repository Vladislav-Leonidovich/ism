using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace lpnu.Controllers
{
    public class AccreditationController : Controller
	{
		private readonly IAccreditationProgramService _accreditationProgramService;
		private readonly IDocumentService _documentService;

		public AccreditationController(IAccreditationProgramService accreditationProgramService, IDocumentService documentService)
		{
			_accreditationProgramService = accreditationProgramService;
			_documentService = documentService;
		}

		public async Task<IActionResult> AccreditationPrograms()
		{
			var programs = await _accreditationProgramService.GetAllProgramsAsync();
			return View(programs);
		}

		public async Task<IActionResult> AccreditationDocuments(int? id)
		{
			var programs = await _accreditationProgramService.GetAllProgramsAsync();
			var viewModel = new AllAccreditationProgramsViewModel();

			foreach (var program in programs)
			{
				var documents = await _documentService.GetAllDocumentsByProgramIdAsync(program.Id);
				var accreditationDocuments = documents.OfType<AccreditationDocument>().ToList();

				var programWithDocuments = new AccreditationProgramViewModel()
				{
					Program = program,
					AccreditationDocuments = accreditationDocuments
				};

				viewModel.ProgramsWithDocuments.Add(programWithDocuments);
			}

			ViewBag.ProgramId = id;
			return View(viewModel);
		}


		public async Task<IActionResult> OtherDocuments(int? id)
		{
			var programs = await _accreditationProgramService.GetAllProgramsAsync();
			var viewModel = new AllAccreditationProgramsViewModel();

			foreach (var program in programs)
			{
				var documents = await _documentService.GetAllDocumentsByProgramIdAsync(program.Id);
				var otherDocuments = documents.OfType<OtherDocument>().ToList();

				var programWithDocuments = new AccreditationProgramViewModel()
				{
					Program = program,
					OtherDocuments = otherDocuments
				};

				viewModel.ProgramsWithDocuments.Add(programWithDocuments);
			}

			ViewBag.ProgramId = id;
			return View(viewModel);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForAccreditationProgram()
		{
			return View("~/Views/Accreditation/AdminActions/Create/CreateItemForAccreditationProgram.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForAccreditationProgram(CreateAccreditationProgramViewModel model)
		{
			if (ModelState.IsValid)
			{
				var program = new AccreditationProgram { Title = model.Title };
				await _accreditationProgramService.AddProgramAsync(program);

				if (model.AccreditationDocuments.Count + model.OtherDocuments.Count > 20)
				{
					ModelState.AddModelError("", "You can upload a maximum of 20 files.");
					return View(model);
				}

				var sanitizedTitle = SanitizeTitle(program.Title);
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", sanitizedTitle);
				await SaveDocuments(model.AccreditationDocuments, uploadsFolder, program, typeof(AccreditationDocument), sanitizedTitle);
				await SaveDocuments(model.OtherDocuments, uploadsFolder, program, typeof(OtherDocument), sanitizedTitle);

				return RedirectToAction(nameof(AccreditationPrograms));
			}
			return View("~/Views/Accreditation/AdminActions/Create/CreateItemForAccreditationProgram.cshtml", model);
		}

		private string SanitizeTitle(string title)
		{
			var sanitizedTitle = Regex.Replace(title, @"[^\p{L}\p{N}]", ""); // Удаляет недопустимые символы
			return sanitizedTitle;
		}

		private async Task SaveDocuments(List<IFormFile> files, string uploadsFolder, AccreditationProgram program, Type documentType, string sanitizedTitle)
		{
			if (!Directory.Exists(uploadsFolder))
			{
				Directory.CreateDirectory(uploadsFolder); // Создает папку, если она не существует
			}

			foreach (var file in files)
			{
				if (file.Length > 0 && file.Length < 10485760) // less than 10 MB
				{
					var _sanitizedTitle = sanitizedTitle;
					var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
					var filePath = Path.Combine(uploadsFolder, uniqueFileName);
					var relativePath = Path.Combine("uploads", _sanitizedTitle, uniqueFileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}

					Document document = null;
					if (documentType == typeof(AccreditationDocument))
					{
						document = new AccreditationDocument
						{
							FilePath = relativePath,
							Description = file.FileName,
							UploadDate = DateTime.Now,
							AccreditationProgramId = program.Id
						};
					}
					else
					{
						document = new OtherDocument
						{
							FilePath = relativePath,
							Description = file.FileName,
							UploadDate = DateTime.Now,
							AccreditationProgramId = program.Id
						};
					}

					await _documentService.AddDocumentAsync(document);
				}
				else
				{
					ModelState.AddModelError("", "File size must be less than 10 MB.");
				}
			}
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForAccreditationProgram(int id)
		{
			var program = await _accreditationProgramService.GetProgramByIdAsync(id);
			if (program == null)
			{
				return NotFound();
			}

			return View("~/Views/Accreditation/AdminActions/Delete/DeleteItemForAccreditationProgram.cshtml", program);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmedItemForAccreditationProgram(int id)
		{
			await _accreditationProgramService.DeleteProgramAsync(id);
			return RedirectToAction(nameof(AccreditationPrograms));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForAccreditationProgram(int id)
		{
			var program = await _accreditationProgramService.GetProgramByIdAsync(id);
			if (program == null)
			{
				return NotFound();
			}

			var documents = await _documentService.GetDocumentsByProgramIdAsync(id);
			var accreditationDocuments = documents.Where(doc => doc is AccreditationDocument).ToList();
			var otherDocuments = documents.Where(doc => doc is OtherDocument).ToList();

			var viewModel = new EditAccreditationProgramViewModel
			{
				Id = program.Id,
				Title = program.Title,
				ExistingAccreditationDocuments = accreditationDocuments.Select(doc => new DocumentViewModel
				{
					Id = doc.Id,
					Description = doc.Description,
					FilePath = doc.FilePath
				}).ToList(),
				ExistingOtherDocuments = otherDocuments.Select(doc => new DocumentViewModel
				{
					Id = doc.Id,
					Description = doc.Description,
					FilePath = doc.FilePath
				}).ToList()
			};

			return View("~/Views/Accreditation/AdminActions/Edit/EditItemForAccreditationProgram.cshtml", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForAccreditationProgram(EditAccreditationProgramViewModel model)
		{
			if (ModelState.IsValid)
			{
				var program = await _accreditationProgramService.GetProgramByIdAsync(model.Id);
				if (program == null)
				{
					return NotFound();
				}

				program.Title = model.Title;
				await _accreditationProgramService.UpdateProgramAsync(program);

				var sanitizedTitle = SanitizeTitle(program.Title);
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", sanitizedTitle);

				await SaveDocuments(model.AccreditationDocuments, uploadsFolder, program, typeof(AccreditationDocument), sanitizedTitle);
				await SaveDocuments(model.OtherDocuments, uploadsFolder, program, typeof(OtherDocument), sanitizedTitle);

				return RedirectToAction(nameof(AccreditationPrograms));
			}
			return View("~/Views/Accreditation/AdminActions/Edit/EditItemForAccreditationProgram.cshtml", model);
		}
	}
}

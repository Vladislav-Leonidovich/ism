using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace lpnu.Controllers
{
    public class EducationController : Controller
	{
		private readonly IEducationProgramService _educationProgramService;
		private readonly ICompanyService _companyService;
		private readonly IUniversityService _universityService;
		private readonly IConferenceService _conferenceService;
		public EducationController(IEducationProgramService educationProgramService, ICompanyService companyService, IUniversityService universityService, IConferenceService conferenceService)
		{
			_educationProgramService = educationProgramService;
			_companyService = companyService;
			_conferenceService = conferenceService;
			_universityService = universityService;
		}
		public IActionResult Programs()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForEducationProgram()
		{
			return View("~/Views/Education/AdminActions/Create/CreateItemForEducationProgram.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForEducationProgram(EducationProgram programTitle)
		{
			if (ModelState.IsValid)
			{
				await _educationProgramService.AddEducationProgramAsync(programTitle);
				return RedirectToAction(nameof(Programs));
			}
			return View("~/Views/Education/AdminActions/Create/CreateItemForEducationProgram.cshtml", programTitle);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForEducationProgram(int id)
		{
			var educationProgram = await _educationProgramService.GetEducationProgramByIdAsync(id);
			if (educationProgram == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Delete/DeleteItemForEducationProgram.cshtml", educationProgram);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForEducationProgram(int id)
		{
			await _educationProgramService.DeleteEducationProgramAsync(id);
			return RedirectToAction(nameof(Programs));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForEducationProgram(int id)
		{
			var program = await _educationProgramService.GetEducationProgramByIdAsync(id);
			if (program == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForEducationProgram.cshtml", program);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForEducationProgram(int id, EducationProgram program)
		{
			if (id != program.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _educationProgramService.UpdateEducationProgramAsync(program);
				return RedirectToAction(nameof(Programs));
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForEducationProgram.cshtml", program);
		}

		public async Task<IActionResult> Bachelor()
		{
			var bachelorsPrograms = await _educationProgramService.GetAllBachelorsProgramsAsync();
			var viewModel = new EducationProgramViewModel
			{
				BachelorsPrograms = bachelorsPrograms

			};
			return View(viewModel);
		}

		public async Task<IActionResult> Master()
		{
			var mastersPrograms = await _educationProgramService.GetAllMastersProgramsAsync();
			var viewModel = new EducationProgramViewModel
			{
				MastersPrograms = mastersPrograms

			};
			return View(viewModel);
		}

		public async Task<IActionResult> Phd()
		{
			var phdPrograms = await _educationProgramService.GetAllPhdProgramsAsync();
			var viewModel = new EducationProgramViewModel
			{
				PhdPrograms = phdPrograms

			};
			return View(viewModel);
		}

		public async Task<IActionResult> InternationalRelations()
		{
			var companies = await _companyService.GetAllCompaniesAsync();
			var conferences = await _conferenceService.GetAllConferencesAsync();
			var universities = await _universityService.GetAllUniversitiesAsync();
			var viewModel = new InternationalRelationsViewModel
			{
				Companies = companies,
				Conferences = conferences,
				Universities = universities

			};
			return View(viewModel);
		}


		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForCompany()
		{
			return View("~/Views/Education/AdminActions/Create/CreateItemForCompany.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForCompany(Company company)
		{
			if (ModelState.IsValid)
			{
				await _companyService.AddCompanyAsync(company);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Create/CreateItemForCompany.cshtml", company);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForUniversity()
		{
			return View("~/Views/Education/AdminActions/Create/CreateItemForUniversity.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForUniversity(University university)
		{
			if (ModelState.IsValid)
			{
				await _universityService.AddUniversityAsync(university);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Create/CreateItemForUniversity.cshtml", university);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult CreateItemForConference()
		{
			return View("~/Views/Education/AdminActions/Create/CreateItemForConference.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateItemForConference(Conference conference)
		{
			if (ModelState.IsValid)
			{
				await _conferenceService.AddConferenceAsync(conference);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Create/CreateItemForConference.cshtml", conference);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForCompany(int id)
		{
			var company = await _companyService.GetCompanyByIdAsync(id);
			if (company == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Delete/DeleteItemForCompany.cshtml", company);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForCompany(int id)
		{
			await _companyService.DeleteCompanyAsync(id);
			return RedirectToAction(nameof(InternationalRelations));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForUniversity(int id)
		{
			var university = await _universityService.GetUniversitiesByIdAsync(id);
			if (university == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Delete/DeleteItemForUniversity.cshtml", university);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForUniversity(int id)
		{
			await _universityService.DeleteUniversityAsync(id);
			return RedirectToAction(nameof(InternationalRelations));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteItemForConference(int id)
		{
			var conference = await _conferenceService.GetConferencesByIdAsync(id);
			if (conference == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Delete/DeleteItemForConference.cshtml", conference);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmedItemForConference(int id)
		{
			await _conferenceService.DeleteConferenceAsync(id);
			return RedirectToAction(nameof(InternationalRelations));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForCompany(int id)
		{
			var company = await _companyService.GetCompanyByIdAsync(id);
			if (company == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForCompany.cshtml", company);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForCompany(int id, Company company)
		{
			if (id != company.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _companyService.UpdateCompanyAsync(company);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForCompany.cshtml", company);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForUniversity(int id)
		{
			var university = await _universityService.GetUniversitiesByIdAsync(id);
			if (university == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForUniversity.cshtml", university);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForUniversity(int id, University university)
		{
			if (id != university.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _universityService.UpdateUniversityAsync(university);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForUniversity.cshtml", university);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForConference(int id)
		{
			var conference = await _conferenceService.GetConferencesByIdAsync(id);
			if (conference == null)
			{
				return NotFound();
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForConference.cshtml", conference);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditItemForConference(int id, Conference conference)
		{
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await _conferenceService.UpdateConferenceAsync(conference);
				return RedirectToAction(nameof(InternationalRelations));
			}
			return View("~/Views/Education/AdminActions/Edit/EditItemForConference.cshtml", conference);
		}
	}
}

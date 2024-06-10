using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class CreateAccreditationProgramViewModel
	{
		[Required]
		[StringLength(250)]
		public string Title { get; set; }

		public List<IFormFile> AccreditationDocuments { get; set; } = new List<IFormFile>();
		public List<IFormFile> OtherDocuments { get; set; } = new List<IFormFile>();
	}
}

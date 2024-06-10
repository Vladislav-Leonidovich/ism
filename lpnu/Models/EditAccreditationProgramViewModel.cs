namespace lpnu.Models
{
	public class EditAccreditationProgramViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<IFormFile> AccreditationDocuments { get; set; } = new List<IFormFile>();
		public List<IFormFile> OtherDocuments { get; set; } = new List<IFormFile>();
		public List<DocumentViewModel> ExistingAccreditationDocuments { get; set; } = new List<DocumentViewModel>();
		public List<DocumentViewModel> ExistingOtherDocuments { get; set; } = new List<DocumentViewModel>();
	}

	public class DocumentViewModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string FilePath { get; set; }
	}
}

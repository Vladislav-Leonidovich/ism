using System.ComponentModel.DataAnnotations;
namespace lpnu.Models
{
	public abstract class Document
	{
		public int Id { get; set; }
		[Required]
		public string FilePath { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime UploadDate { get; set; }
		public int AccreditationProgramId { get; set; }
		public AccreditationProgram AccreditationProgram { get; set; }
	}

	public class AccreditationDocument : Document
	{
		
	}

	public class OtherDocument : Document
	{
		
	}
}
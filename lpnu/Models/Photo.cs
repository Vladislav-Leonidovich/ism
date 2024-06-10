using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class Photo
	{
		public int Id { get; set; }

		[Required]
		[Url]
		public string Path { get; set; }

		public string Description { get; set; }
	}
}

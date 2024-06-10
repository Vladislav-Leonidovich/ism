using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class Conference
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Abbreviation { get; set; }
	}
}

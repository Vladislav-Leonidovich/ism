using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class Partner
	{
		public int Id { get; set; }

		[Required]
		[Url]
		public string LogoPath { get; set; }
		[StringLength(50)]
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string Description { get; set; }

		[Required]
		public string WebsiteURL { get; set; }
	}
}

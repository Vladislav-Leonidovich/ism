using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class Company
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Url]
		public string LogoPath { get; set; }

		[Required]
		[Url]
		public string FlagPath { get; set; }
	}
}

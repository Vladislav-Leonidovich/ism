using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class University
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		[Url]
		public string LogoPath { get; set; }

		public string City { get; set;}

		public string Country { get; set;}
	}
}

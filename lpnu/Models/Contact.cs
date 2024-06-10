using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace lpnu.Models
{
	public class Contact
	{
		public int Id { get; set; }

		[Required]
		public string EducationalBuilding { get; set; }
		public string Room { get; set; }

		[Required]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		public string StuffNameCollectionSerialized { get; set; }

		public List<string> StuffNames => JsonSerializer.Deserialize<List<string>>(StuffNameCollectionSerialized);
	}
}

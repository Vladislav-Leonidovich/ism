using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
    public class CreateContactViewModel
    {
        public string EducationalBuilding { get; set; }
        public string Room { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
		public string StuffNameCollectionSerialized { get; set; }
	}
}

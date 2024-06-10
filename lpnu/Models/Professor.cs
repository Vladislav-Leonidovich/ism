using System.ComponentModel.DataAnnotations;
namespace lpnu.Models
{
    public class Professor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Patronymic { get; set; }

		public string PhotoPath { get; set; }
        public string ProfilePath { get; set; }
    }
}

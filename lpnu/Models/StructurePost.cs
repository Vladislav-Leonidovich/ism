using System.ComponentModel.DataAnnotations;
namespace lpnu.Models
{
    public class StructurePost
    {
        public int Id { get; set; }

        [Required]
        public string ProfessorName { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        [Required]
        public string Position { get; set; }


        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

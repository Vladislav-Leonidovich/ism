using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace lpnu.Models
{
    public class AccreditationProgram
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public ICollection<AccreditationDocument> AccreditationDocuments { get; set; } = new List<AccreditationDocument>();

        public ICollection<OtherDocument> OtherDocuments { get; set; } = new List<OtherDocument>();
    }
}

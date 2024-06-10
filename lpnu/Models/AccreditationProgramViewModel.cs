namespace lpnu.Models
{
    public class AccreditationProgramViewModel
    {
        public AccreditationProgram Program { get; set; }
        public List<AccreditationDocument> AccreditationDocuments { get; set; }
        public List<OtherDocument> OtherDocuments { get; set; }
    }
}

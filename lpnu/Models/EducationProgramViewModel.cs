namespace lpnu.Models
{
	public class EducationProgramViewModel
	{
		public IEnumerable<EducationProgram> BachelorsPrograms { get; set; }
		public IEnumerable<EducationProgram> MastersPrograms { get; set; }
		public IEnumerable<EducationProgram> PhdPrograms { get; set; }
	}
}

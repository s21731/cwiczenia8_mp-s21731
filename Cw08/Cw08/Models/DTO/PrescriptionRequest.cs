namespace Cw08.Models.DTO
{
    public class PrescriptionRequest
    {
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public string MedicamentName { get; set; }
    }
}

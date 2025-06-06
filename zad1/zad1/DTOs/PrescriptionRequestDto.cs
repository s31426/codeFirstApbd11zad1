using zad1.Models;

namespace zad1.DTOs;

public class PrescriptionRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public int IdDoctor { get; set; }
    
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
}

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}
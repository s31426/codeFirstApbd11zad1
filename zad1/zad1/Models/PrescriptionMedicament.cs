using System.ComponentModel.DataAnnotations;

namespace zad1.Models;

public class PrescriptionMedicament
{
    [Key]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }
    
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }
    
    public int Dose { get; set; }
    public string Details {get; set;}

}
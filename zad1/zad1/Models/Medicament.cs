using System.ComponentModel.DataAnnotations;

namespace zad1.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
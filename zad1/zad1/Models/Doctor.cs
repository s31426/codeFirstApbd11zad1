using System.ComponentModel.DataAnnotations;

namespace zad1.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    
    public ICollection<Prescription> prescriptions { get; set; }
}
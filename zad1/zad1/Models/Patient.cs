namespace zad1.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateTime Birthdate { get; set; }
    
    public ICollection<Prescription> prescriptions { get; set; }
}
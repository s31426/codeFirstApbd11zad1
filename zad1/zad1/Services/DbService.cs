using zad1.Data;
using zad1.DTOs;
using zad1.Models;
using Microsoft.EntityFrameworkCore;


namespace zad1.Services;

public class DbService : IDbService
{
    private readonly ClinicDbContext _context;

    public DbService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(PrescriptionRequestDto dto)
    {
        if (dto.Medicaments.Count > 10)
        {
            throw new Exception();
        }

        if (dto.DueDate < dto.Date)
        {
            throw new Exception();
        }
        
        var existingMedicaments = await _context.Medicaments
            .Where(m => dto.Medicaments.Select(dm => dm.IdMedicament).Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        
        if (existingMedicaments.Count != dto.Medicaments.Count)
            throw new Exception("Niektóre leki nie istnieją");

        // Znajdź lub dodaj pacjenta
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.FirstName == dto.FirstName && p.LastName == dto.LastName && p.Birthdate == dto.Birthdate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birthdate = dto.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Dodaj receptę
        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = dto.IdDoctor,
            PrescriptionMedicaments = dto.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
    
    
    
    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

        if (patient == null)
            return null;

        return new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDetailsDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Type = pm.Medicament.Type,
                        Dose = pm.Dose
                    }).ToList()
                }).ToList()
        };
    }
}
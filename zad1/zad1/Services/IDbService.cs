using zad1.DTOs;

namespace zad1.Services;

public interface IDbService
{
    Task AddPrescriptionAsync(PrescriptionRequestDto dto);
    Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient);
}
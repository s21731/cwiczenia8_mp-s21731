using Cw08.Models;
using Cw08.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Cw08.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _mainDbContext;
        public DbService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }
        public async Task<IActionResult> GetDoctors()
        {
            return new OkObjectResult(await _mainDbContext.Doctors.ToListAsync());
        }

        public async Task<IActionResult> AddDoctors(DoctorToAdd doctor)
        {

            var d = new Doctor()
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };

            await _mainDbContext.AddAsync(d);
            await _mainDbContext.SaveChangesAsync();


            return new OkObjectResult($"Doktor {doctor.FirstName} {doctor.LastName} został dodany do bazy danych.");
        }

        public async Task<IActionResult> ModifyDoctor(ModifyDoctor doctor)
        {
            bool IsExist = await _mainDbContext.Doctors.AnyAsync(d => d.IdDoctor == doctor.IdModifyDoctor);
          
            if (!IsExist)
            {
                throw new Exception("Doktor o podanym id nie istnieje");
            }

            var doc = await _mainDbContext.Doctors.SingleAsync(d => d.IdDoctor == doctor.IdModifyDoctor);

            doc.FirstName = doctor.FirstName;
            doc.LastName = doctor.LastName;
            doc.Email = doctor.Email;

            await _mainDbContext.SaveChangesAsync();

            return new OkObjectResult($"Dane doktora o id = {doc.IdDoctor} zostały zmienione" );
            
        }

        public async Task<IActionResult> DeleteDoctor(int IdDeleteDoctor)
        {
            bool IsExist = await _mainDbContext.Doctors.AnyAsync(d => d.IdDoctor == IdDeleteDoctor);

            if (!IsExist)
            {
                throw new Exception("Doktor o podanym id nie istnieje");
            }

            _mainDbContext.Doctors.Remove(await _mainDbContext.Doctors.SingleAsync(d => d.IdDoctor == IdDeleteDoctor));
               
            await _mainDbContext.SaveChangesAsync();

            return new OkObjectResult($"Doktor o id {IdDeleteDoctor} został usunięty");
        }

        public async Task<IActionResult> GetPrescription(int idPatient, int idDoctor, string MedicamentName)
        {
            bool IsDoctorExist = await _mainDbContext.Doctors.AnyAsync(d => d.IdDoctor == idDoctor);
            bool IsPatientExist = await _mainDbContext.Patients.AnyAsync(d => d.IdPatient == idPatient);
            bool IsMedicineExist = await _mainDbContext.Medicaments.AnyAsync(d => d.Name == MedicamentName);

            if (!IsDoctorExist)
                throw new Exception("Doktor o podanym ID nie istnieje");
            if (!IsPatientExist)
                throw new Exception("Pacjent o podanym ID nie istnieje");
            if (!IsMedicineExist)
                throw new Exception("Lek o podanej nazwie nie istnieje");

            var prescription = await _mainDbContext.Prescriptions
                                    .Include(p => p.PrescriptionMedicaments)
                                    .Where(p => p.IdPatient == idPatient && p.IdDoctor == idDoctor)
                                    .AnyAsync();

            var medicament = await _mainDbContext.Medicaments
                                    .Include(m => m.PrescriptionMedicaments)
                                    .Where(m => m.Name == MedicamentName)
                                    .AnyAsync();

            var response = await _mainDbContext.Prescriptions
                                    .Where(p => p.IdPatient == idPatient && p.IdDoctor == idDoctor)
                                    .Select(p => new PrescriptionResponse
                                    {
                                        IdPrescription = p.IdPrescription,
                                        Date = p.Date,
                                        DueDate = p.DueDate
                                    })
                                    .ToListAsync();


            return new ObjectResult(response);
        }








    }
    }

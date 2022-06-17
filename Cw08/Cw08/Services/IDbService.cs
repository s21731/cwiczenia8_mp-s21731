using Cw08.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace Cw08.Services
{
    public interface IDbService
    {

        public Task<IActionResult> GetDoctors();
        public Task<IActionResult> AddDoctors(DoctorToAdd doctor);
        public Task<IActionResult> ModifyDoctor(ModifyDoctor modifyDoctor);
        public Task<IActionResult> DeleteDoctor(int IdDeleteDoctor);
        public Task<IActionResult> GetPrescription(int idPatient, int idDoctor, string MedicamentName);



    }
}

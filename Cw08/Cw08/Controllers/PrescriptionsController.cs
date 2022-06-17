using Cw08.Models.DTO;
using Cw08.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cw08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _iDbService;
        public PrescriptionsController(IDbService iDbService)
        {
            _iDbService = iDbService;
        }


        //[HttpGet]
       // public async Task<IActionResult> GetPrescription([FromBody] PrescriptionRequest prescriptionRequest)
       // {
       //     return await _iDbService.GetPrescription(prescriptionRequest);
       // }

        [HttpGet]
        public async Task<IActionResult> GetPrescription([FromQuery]int idPatient, [FromQuery]int idDoctor, [FromQuery]string MedicamentName)
        {
            return await _iDbService.GetPrescription(idPatient, idDoctor, MedicamentName);
        }


    }
}

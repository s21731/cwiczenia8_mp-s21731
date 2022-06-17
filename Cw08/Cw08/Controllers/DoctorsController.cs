using Cw08.Models.DTO;
using Cw08.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cw08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IDbService _iDbService;
        public DoctorsController(IDbService iDbService)
        {
            _iDbService = iDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            return await _iDbService.GetDoctors();
        }


        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorToAdd doctorToAdd)
        {
            return await _iDbService.AddDoctors(doctorToAdd);
        }



        [HttpPut]
        public async Task<IActionResult> ModifyDoctor(ModifyDoctor modifyDoctor)
        {
            return await _iDbService.ModifyDoctor(modifyDoctor);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            return await _iDbService.DeleteDoctor(id);
        }







    }
}

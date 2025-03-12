using Data;
using Dto;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DES.Api.Controllers
{
    [Route("api/[controller]")]    //api/PatientUnitOfWork/GetAll
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AreaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetGovs")]
        public IActionResult GetGovs()
        {
            var res = _unitOfWork.Areas.FindAll(m=> m.Ltype.ToLower()=="g");
            return Ok(res);
        }

        [HttpGet("GetDists")]
        public IActionResult GetDists()
        {
            var res = _unitOfWork.Areas.FindAll(m => m.Ltype.ToLower() == "d");
            return Ok(res);
        }

        [HttpGet("GetDistsByGovID")]
        public IActionResult GetDistsByGovID(string id)
        {
            var res = _unitOfWork.Areas.FindAll(m => m.Level.StartsWith(id) && m.Ltype.ToLower() == "d");
            return Ok(res);
        }



        [HttpGet("GetSourcesByDistID")]
        public IActionResult GetSourcesByDistID(string id)
        {
            var res = _unitOfWork.Areas.FindAll(m => m.Level.StartsWith(id) && m.Ltype.ToLower() == "s");
            return Ok(res);
        }

        [HttpPost("Add")]
        public IActionResult Add(PatientDto dto)
        {
            //get curren user authotnicated
            //  var _userId = int.Parse(User.Identity.Name);
            Patient patient = new Patient()
            {
                PatientName = dto.PatientName,
                BirtDate = dto.BirtDate,
                AreaId = (int)dto.AreaId,
                NID = dto.NID,
                CreatedOn = DateTime.Now
            };
            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Save();
            return Ok(patient.Id);
        }

    }
}

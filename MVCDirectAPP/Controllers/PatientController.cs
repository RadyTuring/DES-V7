using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCDirectAPP.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public IActionResult Index()
        {
            var res = _unitOfWork.Patients.GetAll();
            return View(res);
        }
        

    }
}

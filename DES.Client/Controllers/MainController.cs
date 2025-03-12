using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Diagnostics.Metrics;

namespace DES.Client.Controllers
{
    public class MainController : Controller
    {
        private ICallApi _api;
        public MainController(ICallApi api)
        {
            _api = api;
        }
        public IActionResult Index()
        {
             
                      
            return View();
        }
    }
}

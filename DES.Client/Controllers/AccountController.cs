using Dto;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;

namespace DES.Client.Controllers
{
    public class AccountController : Controller
    {
        private ICallApi _api;
        public AccountController(ICallApi api)
        {
            _api = api;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( UserLogin model)
        {
            var res = await _api.GetAsync<LoginSuccessReturnDto>("Account/Login", model);

            //this logic to merge pages if dupicate form differnt roles and Give True if one true and one false 
            //if you want to take false instead of true just repalce Any with All in the nexr lines 
            var uniqueRolePages = res.RolePages
           .GroupBy(rp => rp.DesPageId)
           .Select(g => new RolePage
           {
               DesPageId = g.Key,
               IsAdd = g.Any(rp => rp.IsAdd), 
               IsUpdate = g.Any(rp => rp.IsUpdate),
               IsDelete = g.Any(rp => rp.IsDelete),
               IsView = g.Any(rp => rp.IsView),
               RoleId = g.First().RoleId, // You can decide how to handle this, if needed
               Id = g.First().Id // Similarly, decide how to handle Id
           })
           .ToList();
            string userPages = JsonConvert.SerializeObject(res.DesPages);
            string userData = JsonConvert.SerializeObject(res.DesAccount);
            string rolepages = JsonConvert.SerializeObject(uniqueRolePages);
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
                // IsEssential = true,
                // Secure = true
            };

            Response.Cookies.Append("userdata", userData, options);
            Response.Cookies.Append("userpages", userPages, options);
            Response.Cookies.Append("rolepages", rolepages, options);
            return RedirectToAction(actionName:"index",controllerName:"Main");
        }
    }
}

using Dto;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin model)
        {
            var _currentAccount=_unitOfWork.DesAccounts.Find(m=>m.UserName == model.UserName && m.Password==model.Password);
            if (_currentAccount == null)
            {
                return NotFound();
            }
            LoginSuccessReturnDto dto= new LoginSuccessReturnDto();
            string[] _userRoles = _currentAccount.Roles.Split(',');
            var _rolePages = _unitOfWork.RolePages.FindAll(m => _userRoles.Contains(m.RoleId.ToString()));
            List<string> _pages = _rolePages.Select(m => m.DesPageId.ToString()).ToList();

            var _desRoles = _unitOfWork.DesRoles.FindAll(m => _userRoles.Contains(m.Id.ToString()));
            var _desPages = _unitOfWork.DesPages.FindAll(m => _pages.Contains(m.Id.ToString()));
            dto.DesAccount = _currentAccount;
            dto.DesRoles = _desRoles;
            dto.RolePages = _rolePages;
            dto.DesPages = _desPages;
            return Ok(dto);
        }
    }
}

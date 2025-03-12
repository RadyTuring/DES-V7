using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dto;
public class UserLogin
{
    [Required(ErrorMessage ="Please enter your user name"), DisplayName("User Name")]
    public string  UserName { get; set; }
    [Required(ErrorMessage = "Please enter your password name"), DataType(DataType.Password)]
    public string Password { get; set; }
}

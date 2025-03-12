
using Entities;

namespace Dto;

public class LoginSuccessReturnDto
{
    public DesAccount DesAccount { get; set; }
    public IEnumerable<RolePage> RolePages { get; set; }
    public IEnumerable<DesRole> DesRoles { get; set; }
    public IEnumerable<DesPage> DesPages { get; set; }
}

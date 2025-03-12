
namespace Entities;
public class RolePage
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int DesPageId { get; set; }
    public bool IsAdd{ get; set; }
    public bool IsUpdate { get; set; }
    public bool IsDelete { get; set; }
    public bool IsView { get; set; }
}


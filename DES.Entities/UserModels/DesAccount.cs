
using System.Runtime.CompilerServices;

namespace Entities;
public class DesAccount
{
    public int Id { get; set; }
    public string  UserName { get; set; }
    public string Password { get; set; }
    public string?  ImageProfile { get; set; }
    public string  UserLevel { get; set; }
    public string Roles { get; set; }
}

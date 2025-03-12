using System;
namespace MesModels;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ExcludeAction : Attribute
{
}

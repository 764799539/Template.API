using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    [AttributeUsage((AttributeTargets.Field | AttributeTargets.Property), Inherited = true, AllowMultiple = true)]
    public class DisposableAttribute : Attribute
    {
    }
}

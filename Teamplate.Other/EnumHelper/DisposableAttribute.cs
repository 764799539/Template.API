using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    [AttributeUsage((AttributeTargets)(AttributeTargets.Field | AttributeTargets.Property), Inherited = true, AllowMultiple = true)]
    public class DisposableAttribute : Attribute
    {
    }

}

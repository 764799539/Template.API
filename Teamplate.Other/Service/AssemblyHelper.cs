using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Teamplate.NuGet
{
    public static class AssemblyHelper
    {
        // Methods
        public static List<Assembly> LoadCompileAssemblies()
        {
            var compileLibraries = from lib in DependencyContext.Default.CompileLibraries select lib;
            List<Assembly> list = new List<Assembly>();
            foreach(CompilationLibrary library in compileLibraries)
            {
                Assembly item = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(library.Name));
                list.Add(item);
            }
            return list;
        } 

    //    // Nested Types
    //    [Serializable, CompilerGenerated]
    //    private sealed class <>c
    //{
    //    // Fields
    //    public static readonly AssemblyHelper.<>c<>9 = new AssemblyHelper.<>c();
    //    public static Func<CompilationLibrary, bool> <>9__0_0;

    //    // Methods
    //    internal bool <LoadCompileAssemblies>b__0_0(CompilationLibrary lib) =>
    //        (!lib.Serviceable && (lib.Type == "project"));
    //}
}

}

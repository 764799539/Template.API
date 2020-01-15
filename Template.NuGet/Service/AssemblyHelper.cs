using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Template.NuGet
{
    public static class AssemblyHelper
    {
        public static List<Assembly> LoadCompileAssemblies()
        {
            var compileLibraries = from lib in DependencyContext.Default.CompileLibraries select lib;
            List<Assembly> list = new List<Assembly>();
            foreach (CompilationLibrary library in compileLibraries)
            {
                Assembly item = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(library.Name));
                list.Add(item);
            }
            return list;
        }
    }
}

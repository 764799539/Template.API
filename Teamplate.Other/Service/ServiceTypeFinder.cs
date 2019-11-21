using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Teamplate.NuGet
{
    public static class ServiceTypeFinder
    {
        // Methods
        public static List<Type> Find(List<Assembly> assemblies)
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                list.AddRange((IEnumerable<Type>)Find(assembly));
            }
            return list;
        }

        public static List<Type> Find(Assembly assembly) =>
            Enumerable.ToList<Type>((IEnumerable<Type>)(from a in assembly.GetTypes() select a));

        public static List<Type> FindFromCompileLibraries()
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in AssemblyHelper.LoadCompileAssemblies())
            {
                list.AddRange((IEnumerable<Type>)Find(assembly));
            }
            return list;
        }

        public static List<Type> FindFromDirectory(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return new List<Type>();
            }
            List<Type> list = new List<Type>();
            foreach (FileSystemInfo info in new DirectoryInfo(path).GetFileSystemInfos("*.dll", SearchOption.TopDirectoryOnly))
            {
                string str = info.Name.ToLower();
                if (!str.StartsWith("system", (StringComparison)StringComparison.Ordinal) && !str.StartsWith("microsoft", (StringComparison)StringComparison.Ordinal))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(info.FullName);
                    }
                    catch (FileLoadException)
                    {
                        assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(info.Name)));
                        if (assembly == null)
                        {
                            throw;
                        }
                    }
                    list.AddRange((IEnumerable<Type>)Find(assembly));
                }
            }
            return list;
        }

    //    // Nested Types
    //    [Serializable, CompilerGenerated]
    //    private sealed class <>c
    //{
    //    // Fields
    //    public static readonly ServiceTypeFinder.<>c<>9 = new ServiceTypeFinder.<>c();
    //    public static Func<Type, bool> <>9__2_0;

    //    // Methods
    //    internal bool <Find>b__2_0(Type a) =>
    //        (((!a.IsAbstract && a.IsClass) && typeof(IBaseService).IsAssignableFrom(a)) && (a.GetConstructor(Type.EmptyTypes) != null));
    //}
}

}

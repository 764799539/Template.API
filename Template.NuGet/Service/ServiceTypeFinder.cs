using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Template.NuGet
{
    public static class ServiceTypeFinder
    {
        // Methods
        public static List<Type> Find(List<Assembly> assemblies)
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                list.AddRange(Find(assembly));
            }
            return list;
        }

        public static List<Type> Find(Assembly assembly) =>
            Enumerable.ToList(from a in assembly.GetTypes() select a);

        public static List<Type> FindFromCompileLibraries()
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in AssemblyHelper.LoadCompileAssemblies())
            {
                list.AddRange(Find(assembly));
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
                if (!str.StartsWith("system", StringComparison.Ordinal) && !str.StartsWith("microsoft", StringComparison.Ordinal))
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
                    list.AddRange(Find(assembly));
                }
            }
            return list;
        }
    }
}

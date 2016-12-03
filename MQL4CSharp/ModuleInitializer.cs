using System;
using System.IO;
using System.Linq;
using System.Reflection;
/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs args)
        {
            string assemblyFile = (args.Name.Contains(','))
                ? args.Name.Substring(0, args.Name.IndexOf(','))
                : args.Name;

            assemblyFile += ".dll";

            string absoluteFolder = new FileInfo((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath).Directory.FullName;
            string targetPath = Path.Combine(absoluteFolder, assemblyFile);

            try
            {
                return Assembly.LoadFile(targetPath);
            }
            catch (Exception)
            {
                return null;
            }
        };
    }
}
using Microsoft.Extensions.DependencyInjection;
using SolutionCleaner.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolutionCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string csprojExtension = ".csproj";

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<IProjectCleaner, ProjectCleaner>()
                .BuildServiceProvider();

            var projectCleaner = serviceProvider
                .GetService<IProjectCleaner>();

            Console.WriteLine($"[{appName} by Marcin Grzegorczyk]");

            IEnumerable<string> projectDirectories = projectCleaner
                .GetProjectDirectories(currentPath, csprojExtension);

            Console.WriteLine($"\r\nCurrent path:\r\n{currentPath}");

            if (!projectDirectories.Any())
            {
                Console.WriteLine("\r\nNo projects folders!");
                return;
            }

            Console.WriteLine("\r\nProjects list:");

            foreach (var projDir in projectDirectories)
            {
                var fileName = projectCleaner.GetFileNameByExtension(projDir, csprojExtension);
                Console.WriteLine(fileName);
            }

            Console.WriteLine("\r\n--- .vs  remover ---");
            try
            {
                projectCleaner.RemoveVsDirectory(currentPath);
                Console.WriteLine($@".vs removed from {currentPath}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"\r\n.vs does not exist!");
            }

            Console.WriteLine("\r\n--- bin  remover ---");
            foreach (var projDir in projectDirectories)
            {
                try
                {
                    projectCleaner.RemoveBinFromProject(projDir);
                    Console.WriteLine($@"\bin removed from {projDir}");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine($@"\bin does not exist at {projDir}!");
                }

            }

            Console.WriteLine("\r\n--- obj  remover ---");
            foreach (var projDir in projectDirectories)
            {
                try
                {
                projectCleaner.RemoveObjFromProject(projDir);
                Console.WriteLine($@"\obj removed from {projDir}");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine($@"\obj does not exist at {projDir}!");
                }
            }


            Console.WriteLine("\r\nSolution cleaned! Press any key...");

            Console.ReadKey();
        }


    }



}

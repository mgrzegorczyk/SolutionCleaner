using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolutionCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"[{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} by Marcin Grzegorczyk]");
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            
            string vsFolderName = ".vs";
            string csprojExtension = ".csproj";
            string vsFolderPath = Path.Combine(currentPath + $"{vsFolderName}");

            var remover = new DirRemover(currentPath);

            var projectsDirectories = Directory.EnumerateDirectories(currentPath)
                .Where(dir => Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Any(dirFiles => Path.GetExtension(dirFiles) == csprojExtension)).ToList();

            Console.WriteLine($"\r\nCurrent path:\r\n{currentPath}");
            Console.WriteLine("\r\nProjects list:");

            foreach (var projDir in projectsDirectories)
            {
                var fileName = GetFileNameByExtension(projDir, csprojExtension);
                Console.WriteLine(fileName);
            }

            Console.WriteLine("\r\n--- .vs  remover ---");
            remover.RemoveVsDirectory(vsFolderPath);

            Console.WriteLine("\r\n--- bin  remover ---");
            remover.RemoveBinFromProjects(projectsDirectories);

            Console.WriteLine("\r\n--- obj  remover ---");
            remover.RemoveObjFromProjects(projectsDirectories);

            Console.WriteLine("\r\nSolution cleaned! Press any key...");

            Console.ReadKey();
        }

        static string GetFileNameByExtension(string dirPath, string fileExtension)
        {
            try
            {
                var filePath = Directory.EnumerateFiles(dirPath, "*.*", SearchOption.TopDirectoryOnly)
                    .Single(s => Path.GetExtension(s) == fileExtension);

                var fileName = Path.GetFileName(filePath);

                return fileName;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"The collection does not contain only one {fileExtension} file!");
                //TODO 
            }
        }
    }



}

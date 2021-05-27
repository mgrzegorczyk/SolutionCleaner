using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCleaner
{
    public class DirRemover
    {
        private string currentPath { get; set; }
        public DirRemover(string currentPath)
        {
            this.currentPath = currentPath;
        }

        public void RemoveVsDirectory(string vsFolderPath)
        {
            RemoveDirectory(vsFolderPath);
        }

        public void RemoveBinFromProjects(IEnumerable<string> projectDirsPaths)
        {
            foreach(var projDir in projectDirsPaths)
            {
                var objPath = Path.Combine(projDir + @"\bin");
                RemoveDirectory(objPath);
            }
        }

        public void RemoveObjFromProjects(IEnumerable<string> projectDirsPaths)
        {
            foreach (var projDir in projectDirsPaths)
            {
                var objPath = Path.Combine(projDir + @"\obj");
                RemoveDirectory(objPath);
            }
        }

        private void RemoveDirectory(string directoryPath, bool recursive = true)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive);
                Console.WriteLine($"{directoryPath} [deleted]");
            }
            else
            {
                Console.WriteLine($"{directoryPath} [not exist]");
            }
        }
    }
}

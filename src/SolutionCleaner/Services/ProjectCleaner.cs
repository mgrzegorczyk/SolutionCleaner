using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace SolutionCleaner.Services
{
    public class ProjectCleaner : IProjectCleaner
    {
        private readonly IFileSystem fileSystem;

        public ProjectCleaner(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void RemoveVsDirectory(string projectDirPath)
        {
            var vsFolderPath = fileSystem.Path.Combine(projectDirPath, ".vs");
            fileSystem.Directory.Delete(vsFolderPath, true);
        }

        public void RemoveBinFromProject(string projectDirPath)
        {
            var objPath = fileSystem.Path.Combine(projectDirPath, @"\bin");
            fileSystem.Directory.Delete(objPath, true);
        }

        public void RemoveObjFromProject(string projectDirPath)
        {
            var objPath = fileSystem.Path.Combine(projectDirPath, @"\obj");
            fileSystem.Directory.Delete(objPath, true);
        }

        public IEnumerable<string> GetProjectDirectories(string currentPath, string csprojExtension)
        {
            return fileSystem.Directory.EnumerateDirectories(currentPath)
                .Where(dir => fileSystem.Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Any(dirFiles => fileSystem.Path.GetExtension(dirFiles) == csprojExtension));
        }

        public string GetFileNameByExtension(string dirPath, string fileExtension)
        {
            try
            {
                var filePath = fileSystem.Directory.EnumerateFiles(dirPath, "*.*", SearchOption.TopDirectoryOnly)
                    .Single(s => fileSystem.Path.GetExtension(s) == fileExtension);

                var fileName = fileSystem.Path.GetFileName(filePath);

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


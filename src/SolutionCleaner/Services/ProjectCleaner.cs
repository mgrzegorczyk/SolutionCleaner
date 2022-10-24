using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using SolutionCleaner.Enums;

namespace SolutionCleaner.Services
{
    public class ProjectCleaner : IProjectCleaner
    {
        private readonly IFileSystem fileSystem;

        public ProjectCleaner(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }
        public void RemoveDirectoryFromProject(string projectDirPath, EProjectDirectory projectDir)
        {
            string projectDirName = projectDir switch
            {
                EProjectDirectory.Bin => @"\bin",
                EProjectDirectory.Obj => @"\obj",
                EProjectDirectory.VS => ".vs",
                _ => throw new ArgumentOutOfRangeException(nameof(projectDir), projectDir, "Please choose type of project directory!")
            };

            var objPath = fileSystem.Path.Combine(projectDirPath, projectDirName);
            
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


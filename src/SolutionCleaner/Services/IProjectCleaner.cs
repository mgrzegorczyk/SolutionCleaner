using System.Collections.Generic;
using SolutionCleaner.Enums;

namespace SolutionCleaner.Services
{
    public interface IProjectCleaner
    {
        void RemoveDirectoryFromProject(string projectDirPath, EProjectDirectory projectDir);
        IEnumerable<string> GetProjectDirectories(string currentPath, string csprojExtension);
        string GetFileNameByExtension(string dirPath, string fileExtension);
    }
}


using System.Collections.Generic;

namespace SolutionCleaner.Services
{
    public interface IProjectCleaner
    {
        void RemoveVsDirectory(string vsFolderPath);
        void RemoveBinFromProject(string projectDirPath);
        void RemoveObjFromProject(string projectDirPath);
        IEnumerable<string> GetProjectDirectories(string currentPath, string csprojExtension);
        string GetFileNameByExtension(string dirPath, string fileExtension);
    }
}


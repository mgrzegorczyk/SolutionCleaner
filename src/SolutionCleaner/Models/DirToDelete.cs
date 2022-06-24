using System.IO;

namespace SolutionCleaner.Models
{
    public class ProjectDirToDelete
    {
        public string DirToDeletePath { get; set; }
        public string CsprojFilePath { get; set; }
        public string CsprojFileName { get { return Path.GetFileName(CsprojFilePath); } }
    }
}

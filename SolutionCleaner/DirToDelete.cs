using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionCleaner
{
    public class ProjectDirToDelete
    {
        public string DirToDeletePath { get; set; }
        public string CsprojFilePath { get; set; }
        public string CsprojFileName { get { return Path.GetFileName(CsprojFilePath); } }
    }
}

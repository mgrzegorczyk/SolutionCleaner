using SolutionCleaner.Services;
using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace SolutionCleaner.Tests
{
    public class ProjectCleanerTests
    {
        private readonly ProjectCleaner projectCleaner;
        private readonly MockFileSystem fileSystem;
        private readonly string projectDirPath = Path.Combine(@"c:\");

        public ProjectCleanerTests()
        {
            this.fileSystem = new MockFileSystem();
            this.projectCleaner = new ProjectCleaner(fileSystem);
        }

        [Fact]
        public void RemoveVsDirectory_ShouldRemoveVsDirectory_WhenPathIsCorrect()
        {
            //Arrange
            var vsFolderFullPath = Path.Combine(projectDirPath, ".vs");

            fileSystem.AddDirectory(".vs");

            //Act
            projectCleaner.RemoveVsDirectory(projectDirPath);

            //Assert
            Assert.False(fileSystem.Directory.Exists(vsFolderFullPath));
        }

        [Fact]
        public void RemoveVsDirectory_ShouldThrowArgumentNullException_WhenProjectDirPathIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => projectCleaner.RemoveVsDirectory(null));
        }
    }
}

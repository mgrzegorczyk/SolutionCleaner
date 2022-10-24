using SolutionCleaner.Services;
using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using SolutionCleaner.Enums;
using SolutionCleaner.Tests.Extensions;
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

        [Theory]
        [InlineData(EProjectDirectory.Bin)]
        [InlineData(EProjectDirectory.Obj)]
        [InlineData(EProjectDirectory.VS)]
        public void RemoveDirectoryFromProject_ShouldRemoveProjectDirectory_WhenPathIsCorrect(EProjectDirectory projectDir)
        {
            //Arrange
            string projectDirToRemove = projectDir switch
            {
                EProjectDirectory.Bin => @"\bin",
                EProjectDirectory.Obj => @"\obj",
                EProjectDirectory.VS => ".vs",
            };
            
            var vsFolderFullPath = Path.Combine(projectDirPath, projectDirToRemove);

            fileSystem.AddDirectory(vsFolderFullPath);

            //Act
            projectCleaner.RemoveDirectoryFromProject(projectDirPath, projectDir);

            //Assert
            Assert.False(fileSystem.Directory.Exists(vsFolderFullPath));
        }
        
        [Fact]
        public void RemoveProjectDirectory_ShouldThrowArgumentNullException_WhenProjectDirPathIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => projectCleaner.RemoveDirectoryFromProject(null, EnumExtensions.RandomEnumValue<EProjectDirectory>()));
        }
    }
}

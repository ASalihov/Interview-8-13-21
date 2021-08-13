using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Interview.Api.Domain;
using Interview.Api.Controllers;

namespace Interview.UnitTests
{
    [TestFixture]
    public class FolderAnalyzerTests
    {
        [Test]
        public void GetHangingFolders_ShouldReturnEmptyList_WhenEmptyListPassed()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());
            List<Folder> emptyList = new();

            // Act
            var result = controller.DetermineHangingFolders(emptyList);

            // Assert
            CollectionAssert.AreEqual(emptyList, result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldReturnEmptyList_WhenAllFoldersRelatedProperly()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());
            List<Folder> noHangingList = new()
            {
                new Folder("1", "a"),
                new Folder("1.1", "b"),
                new Folder("1.1.1", "c")
            };

            // Act
            var result = controller.DetermineHangingFolders(noHangingList);

            // Assert
            CollectionAssert.IsEmpty(result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldReturnEmptyList_WhenAllFoldersRelatedProperlyWithBigDimensions()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());
            List<Folder> noHangingList = new()
            {
                new Folder("0", "a"),
                new Folder("0.111", "b"),
                new Folder("0.111.656565", "c")
            };

            // Act
            var result = controller.DetermineHangingFolders(noHangingList);

            // Assert
            CollectionAssert.IsEmpty(result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldReturnEmptyList_WhenAllFoldersAreRootParents()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());
            List<Folder> noHangingList = new()
            {
                new Folder("1", "a"),
                new Folder("2", "b"),
                new Folder("3", "c")
            };

            // Act
            var result = controller.DetermineHangingFolders(noHangingList);

            // Assert
            CollectionAssert.IsEmpty(result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldReturnPassedList_WhenAllFoldersAreHanging()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());
            List<Folder> allHangingList = new()
            {
                new Folder("1.1", "a"),
                new Folder("2.1.2", "b"),
                new Folder("3.1.1.3", "c"),
                new Folder("333.4444", "d")
            };

            // Act
            var result = controller.DetermineHangingFolders(allHangingList);

            // Assert
            CollectionAssert.AreEqual(allHangingList, result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldReturnHangingFolders_WhenTheyPassed()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());

            List<Folder> mixedList = new()
            {
                new Folder("1", "a"),
                new Folder("2.1", "hanging"),
                new Folder("0", "b"),
                new Folder("0.3", "c"),
                new Folder("0.1.186", "hanging"),
                new Folder("3", "d"),
                new Folder("3000", "e")
            };
            var hanging = mixedList.Where(x => x.Name == "hanging").ToList();

            // Act
            var result = controller.DetermineHangingFolders(mixedList);

            // Assert
            CollectionAssert.AreEqual(hanging, result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldTreatIncompleteIndexAsHanging()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());

            List<Folder> incorrectList = new()
            {
                new Folder("1.", "hanging"),
            };

            // Act
            var result = controller.DetermineHangingFolders(incorrectList);

            // Assert
            CollectionAssert.AreEqual(incorrectList, result);
        }
        
        [Test]
        public void GetHangingFolders_ShouldTreatLetterIndexLikeNumerous()
        {
            // Arrange
            var controller = new FoldersController(new FolderAnalyzer());

            List<Folder> mixedList = new()
            {
                new Folder("a.v", "hanging"),
                new Folder("c", "a"),
                new Folder("c.d", "b"),
                new Folder("c.d.eeee", "b"),
                new Folder("abcdef", "c"),
            };
            var hanging = mixedList.Where(x => x.Name == "hanging").ToList();

            // Act
            var result = controller.DetermineHangingFolders(mixedList);

            // Assert
            CollectionAssert.AreEqual(hanging, result);
        }
    }
}
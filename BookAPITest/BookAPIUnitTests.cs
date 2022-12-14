using BookAPI.Controllers;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Repository;
using Moq;
using BookAPI.Models;
using System.Threading.Tasks;
using FluentAssertions;

namespace BookAPITest
{
    public class BookAPIUnitTests
    {

        private BooksController booksController;
        private int Id = 1;
        private readonly Mock<IBookRepo> bookStub = new Mock<IBookRepo>();
        Book sampleBook = new Book
        {
            Id = 1,
            Name = "State Patsy",
            Genere = "Action/Adventure",
            PublisherName = "Queens",
        };
        Book toBePostedBook = new Book
        {
            Name = "Federal Matters",
            Genere = "Suspense",
            PublisherName = "Harpers",
        };
        [Fact]
        public async Task GetBook_BasedOnId_WithExistingBook_ReturnBook()
        {
            //Arrange
            //use the mock to set up the test. we are basically telling here that whatever int id we pass to this method
            //it will always return a new Book object
            bookStub.Setup(service => service.GetBook(It.IsAny<int>())).ReturnsAsync(sampleBook);
            booksController = new BooksController(bookStub.Object);
            //Act
            var actionResult = await booksController.GetBook(1);
            //Assert
            Assert.IsType<Book>(actionResult.Value);
            var result = actionResult.Value;
            //Compare the result member by member
            sampleBook.Should().BeEquivalentTo(result,
                options => options.ComparingByMembers<Book>());
        }

        [Fact]
        public async Task GetBook_BasedOnId_WithNotExistingBook_ReturnNotFound()
        {
            //Arrange
            //use the mock to set up the test. we are basically telling here that whatever int id we pass to this method
            //it will always return null
            booksController = new BooksController(bookStub.Object);
            bookStub.Setup(repo => repo.GetBook(It.IsAny<int>())).ReturnsAsync(new NotFoundResult());
            //Act
            var actionResult = await booksController.GetBook(1);
            //Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}

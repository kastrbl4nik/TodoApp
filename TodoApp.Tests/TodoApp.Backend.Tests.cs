using NUnit.Framework;
using Moq;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Services;
using NUnit.Framework.Internal;
using TodoApp.Backend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using TodoApp.Domain.Models;
using NUnit.Framework.Interfaces;

namespace TodoApp.Domain.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController controller;
        private Mock<IUserService> mockUserService;
        private Mock<ITodoListService> mockTodoListService;
        private Mock<ITodoService> mockTodoService;

        [SetUp]
        public void SetUp()
        {
            mockUserService = new Mock<IUserService>();
            mockTodoListService = new Mock<ITodoListService>();
            mockTodoService = new Mock<ITodoService>();

            controller = new UserController(
                mockUserService.Object,
                mockTodoListService.Object,
                mockTodoService.Object
            );
        }

        [Test]
        public void GetUser_ValidUsername_ReturnsOkObjectResultWithUserModel()
        {
            // Arrange
            var username = "testuser";
            var user = new User { Username = username };
            mockUserService.Setup(s => s.Users).Returns(new[] { user }.AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetUser(username);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            OkObjectResult? okResult = (OkObjectResult)result.Result;
            Assert.That(okResult?.Value, Is.InstanceOf<UserModel>());
            var userModel = (UserModel)okResult.Value;
            Assert.That(userModel?.Username, Is.EqualTo(username));
        }

        [Test]
        public void GetUser_UsernameDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var username = "testuser";
            mockUserService.Setup(s => s.Users).Returns(Enumerable.Empty<User>().AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetUser(username);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetUser_UserCannotRetrieveOtherUserData_ReturnsForbidResult()
        {
            // Arrange
            var username = "testuser";
            var otherUsername = "otheruser";
            var user = new User { Username = otherUsername };
            mockUserService.Setup(s => s.Users).Returns(new[] { user }.AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetUser(otherUsername);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<ForbidResult>());
        }

        [Test]
        public void GetTodoLists_ValidUsername_ReturnsOkObjectResultWithTodoListModel()
        {
            // Arrange
            var username = "testuser";
            var todoList1 = new TodoList { Title = "List 1", User = new User { Username = username } };
            var todoList2 = new TodoList { Title = "List 2", User = new User { Username = username } };
            mockUserService.Setup(s => s.Users).Returns(new[] { new User { Username = username } }.AsQueryable());
            mockTodoListService.Setup(s => s.TodoLists).Returns(new[] { todoList1, todoList2 }.AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetTodoLists(username, null, null);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

            OkObjectResult? okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TodoListModel>>());

            IEnumerable<TodoListModel>? todoLists = okResult?.Value as IEnumerable<TodoListModel>;
            Assert.Multiple(() =>
            {
                Assert.That(todoLists?.Count(), Is.EqualTo(2));

                Assert.That(todoLists?.Any(tl => tl.Title == todoList1.Title), Is.True);
                Assert.That(todoLists?.Any(tl => tl.Title == todoList2.Title), Is.True);
            });
        }

        [Test]
        public void GetTodoLists_UserCannotRetrieveOtherUserData_ReturnsForbidResult()
        {
            // Arrange
            var username = "testuser";
            var otherUsername = "otheruser";
            var user = new User { Username = otherUsername };
            mockUserService.Setup(s => s.Users).Returns(new[] { user }.AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetTodoLists(otherUsername, null, null);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<ForbidResult>());
        }

        [Test]
        public void GetTodoLists_HiddenFlagIsTrue_ReturnsOnlyHiddenLists()
        {
            // Arrange
            var username = "testuser";
            var todoList1 = new TodoList { Title = "List 1", User = new User { Username = username }, Hidden = true };
            var todoList2 = new TodoList { Title = "List 2", User = new User { Username = username }, Hidden = false };
            mockUserService.Setup(s => s.Users).Returns(new[] { new User { Username = username } }.AsQueryable());
            mockTodoListService.Setup(s => s.TodoLists).Returns(new[] { todoList1, todoList2 }.AsQueryable());

            Authorize(username);

            // Act
            var result = controller.GetTodoLists(username, true, null);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

            OkObjectResult? okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TodoListModel>>());

            var todoLists = (IEnumerable<TodoListModel>)okResult.Value;
            Assert.Multiple(() =>
            {
                Assert.That(todoLists?.Count(), Is.EqualTo(1));
                Assert.That(todoLists?.Any(tl => tl.Title == todoList1.Title), Is.True);
                Assert.That(todoLists?.Any(tl => tl.Title == todoList2.Title), Is.False);
            });
        }

        private void Authorize(string username)
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(i => i.Name).Returns(username);
            var mockUser = new ClaimsPrincipal(mockIdentity.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockUser }
            };
        }
    }
}
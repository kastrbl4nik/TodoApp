using NUnit.Framework;
using Moq;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Services;
using NUnit.Framework.Internal;

namespace TodoApp.Domain.Tests
{
    [TestFixture]
    public class TodoListServiceTests
    {
        private Mock<ITodoListRepository> repositoryMock;
        private ITodoListService service;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<ITodoListRepository>();
            service = new TodoListService(repositoryMock.Object);
        }

        #region Getting entities from a repository
        [Test]
        public void TodoLists_Should_Return_All_TodoLists()
        {
            // Arrange
            var todoLists = new[] { new TodoList { Id = 1 }, new TodoList { Id = 2 } };
            repositoryMock.Setup(r => r.TodoLists).Returns(todoLists.AsQueryable());

            // Act
            var result = service.TodoLists.ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(todoLists));
        }
        #endregion

        #region Adding entities to a repository
        [Test]
        public void Add_Should_Add_TodoList_To_Repository()
        {
            // Arrange
            var todoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new TodoList[0].AsQueryable());
            repositoryMock.Setup(r => r.Add(todoList)).Returns(todoList);

            // Act
            var result = service.Add(todoList);

            // Assert
            Assert.That(result, Is.EqualTo(todoList));
            repositoryMock.Verify(r => r.Add(todoList), Times.Once);
        }

        [Test]
        public void Add_Should_Throw_If_TodoList_With_Same_Id_Already_Exists()
        {
            // Arrange
            var existingTodoList = new TodoList { Id = 1 };
            var todoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new[] { existingTodoList }.AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Add(todoList));
        }
        #endregion

        #region Updating entities within a repository
        [Test]
        public void Update_Should_Update_TodoList_In_Repository()
        {
            // Arrange
            var existingTodoList = new TodoList { Id = 1 };
            var updatedTodoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new[] { existingTodoList }.AsQueryable());
            repositoryMock.Setup(r => r.Update(updatedTodoList)).Returns(updatedTodoList);

            // Act
            var result = service.Update(updatedTodoList);

            // Assert
            Assert.That(result, Is.EqualTo(updatedTodoList));
            repositoryMock.Verify(r => r.Update(updatedTodoList), Times.Once);
        }

        [Test]
        public void Update_Should_Throw_If_TodoList_With_Specified_Id_Does_Not_Exist()
        {
            // Arrange
            var todoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new TodoList[0].AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Update(todoList));
        }
        #endregion

        #region Deleting entities from a repository
        [Test]
        public void Delete_Should_Delete_TodoList_From_Repository()
        {
            // Arrange
            var existingTodoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new[] { existingTodoList }.AsQueryable());
            repositoryMock.Setup(r => r.Delete(existingTodoList)).Returns(existingTodoList);

            // Act
            var result = service.Delete(existingTodoList);

            // Assert
            Assert.That(result, Is.EqualTo(existingTodoList));
            repositoryMock.Verify(r => r.Delete(existingTodoList), Times.Once);
        }

        [Test]
        public void Delete_Should_Throw_If_TodoList_With_Specified_Id_Does_Not_Exist()
        {
            // Arrange
            var todoList = new TodoList { Id = 1 };
            repositoryMock.Setup(r => r.TodoLists).Returns(new TodoList[0].AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Delete(todoList));
        }
        #endregion
    }

    [TestFixture]
    public class TodoServiceTests
    {
        private Mock<ITodoRepository> repositoryMock;
        private TodoService service;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<ITodoRepository>();
            service = new TodoService(repositoryMock.Object);
        }

        #region Getting entities from a repository
        [Test]
        public void Todos_Should_Return_All_Todos()
        {
            // Arrange
            var todo1 = new Todo { Id = 1, Title = "Buy groceries", Completed = false };
            var todo2 = new Todo { Id = 2, Title = "Walk the dog", Completed = true };
            repositoryMock.Setup(r => r.Todos).Returns(new[] { todo1, todo2 }.AsQueryable());

            // Act
            var todos = service.Todos.ToList();

            // Assert
            Assert.That(todos, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(todos[0], Is.EqualTo(todo1));
                Assert.That(todos[1], Is.EqualTo(todo2));
            });
        }
        #endregion

        #region Adding entities to a repository
        [Test]
        public void Add_Should_Add_Todo_To_Repository()
        {
            // Arrange
            var newTodo = new Todo { Id = 1 };
            repositoryMock.Setup(r => r.Todos).Returns(new Todo[0].AsQueryable());
            repositoryMock.Setup(r => r.Add(newTodo)).Returns(newTodo);

            // Act
            var result = service.Add(newTodo);

            // Assert
            Assert.That(result, Is.EqualTo(newTodo));
            repositoryMock.Verify(r => r.Add(newTodo), Times.Once);
        }

        [Test]
        public void Add_Should_Throw_If_Todo_With_Specified_Id_Already_Exists()
        {
            // Arrange
            var existingTodo = new Todo { Id = 1 };
            repositoryMock.Setup(r => r.Todos).Returns(new[] { existingTodo }.AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Add(existingTodo));
        }
        #endregion

        #region Updating entities within a repository
        [Test]
        public void Update_Should_Update_Existing_Todo_In_Repository()
        {
            // Arrange
            var existingTodo = new Todo { Id = 1, Completed = false };
            var updatedTodo = new Todo { Id = 1, Completed = true };
            repositoryMock.Setup(r => r.Todos).Returns(new[] { existingTodo }.AsQueryable());
            repositoryMock.Setup(r => r.Update(updatedTodo)).Returns(updatedTodo);

            // Act
            var result = service.Update(updatedTodo);

            // Assert
            Assert.That(result, Is.EqualTo(updatedTodo));
            repositoryMock.Verify(r => r.Update(updatedTodo), Times.Once);
        }

        [Test]
        public void Update_Should_Throw_If_Todo_With_Specified_Id_Does_Not_Exist()
        {
            // Arrange
            var todo = new Todo { Id = 1 };
            repositoryMock.Setup(r => r.Todos).Returns(new Todo[0].AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Update(todo));
        }
        #endregion

        #region Deleting of entities from a repository
        [Test]
        public void Delete_Should_Delete_Todo_From_Repository()
        {
            // Arrange
            var existingTodo = new Todo { Id = 1 };
            repositoryMock.Setup(r => r.Todos).Returns(new[] { existingTodo }.AsQueryable());
            repositoryMock.Setup(r => r.Delete(existingTodo)).Returns(existingTodo);

            // Act
            var result = service.Delete(existingTodo);

            // Assert
            Assert.That(result, Is.EqualTo(existingTodo));
            repositoryMock.Verify(r => r.Delete(existingTodo), Times.Once);
        }

        [Test]
        public void Delete_Should_Throw_If_Todo_With_Specified_Id_Does_Not_Exist()
        {
            // Arrange
            var todo = new Todo { Id = 1 };
            repositoryMock.Setup(r => r.Todos).Returns(new Todo[0].AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.Delete(todo));
        }
        #endregion
    }

    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> repositoryMock;
        private UserService service;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            service = new UserService(repositoryMock.Object);
        }

        [Test]
        public void Users_ReturnsEntitiesFromRepository()
        {
            // Arrange
            var user1 = new User { Id = 1, Username = "alice" };
            var user2 = new User { Id = 2, Username = "bob" };
            repositoryMock.Setup(r => r.Users).Returns(new[] { user1, user2 }.AsQueryable());

            // Act
            var users = service.Users.ToList();

            // Assert
            Assert.That(users, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(users[0], Is.EqualTo(user1));
                Assert.That(users[1], Is.EqualTo(user2));
            });
        }

        [Test]
        public void Add_ThrowsExceptionIfUserWithUsernameAlreadyExists()
        {
            // Arrange
            var existingUser = new User { Id = 1, Username = "alice" };
            repositoryMock.Setup(r => r.Users).Returns(new[] { existingUser }.AsQueryable());
            var newUser = new User { Id = 2, Username = "alice" };

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => service.Add(newUser));
            Assert.That(ex.Message, Is.EqualTo("User with this username already exists"));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application_Test.Authentication
{
    public class SignUpUserServiceTests
    {
        [Fact]
        public async Task ExecuteAsync_WithValidInputs_ReturnsSuccess()
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();

            var users = new List<User>();

            mockContext.Setup(c => c.Users).ReturnsDbSet(users);
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var service = new SignUpUserService(mockContext.Object);

            var request = new SignUpUserRequestInfo
            {
                Email = "test@example.com",
                Password = "StrongPass123",
                UserName = "TestUser",
                Phone = "09123456789"
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("کاربر با موفقیت ایجاد شد.", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal("TestUser", result.Data.UserName);
            Assert.Equal("09123456789", result.Data.Phone);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithInvalidEmail_ThrowsFormatException()
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var service = new SignUpUserService(mockContext.Object);

            var request = new SignUpUserRequestInfo
            {
                Email = "invalid-email",
                Password = "pass",
                UserName = "user",
                Phone = "0912"
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ایمیل معتبر نیست.", result.Message);
            Assert.Null(result.Data);
        }

        [Theory]
        [InlineData(null, "pass", "user", "0912", "Email cant be null.")]
        [InlineData("test@example.com", null, "user", "0912", "Password cant be null.")]
        [InlineData("test@example.com", "pass", null, "0912", "UserName cant be null.")]
        [InlineData("test@example.com", "pass", "user", null, "Phone cant be null.")]
        public async Task ExecuteAsync_WithNullInputs_ThrowsValidationError(string email, string password, string userName, string phone, string expectedMessage)
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var service = new SignUpUserService(mockContext.Object);

            var request = new SignUpUserRequestInfo
            {
                Email = email,
                Password = password,
                UserName = userName,
                Phone = phone
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
            Assert.Null(result.Data);
        }
    }
}

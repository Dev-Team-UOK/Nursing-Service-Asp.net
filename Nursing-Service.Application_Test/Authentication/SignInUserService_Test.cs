using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Users.Queries.SignIn;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.User;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Nursing_Service.Application_Test.Authentication
{
    public class SignInUserService_Test
    {
        [Theory]
        [InlineData(null, "password")]
        [InlineData("", "password")]
        [InlineData("email@example.com", null)]
        [InlineData("email@example.com", "")]
        public async Task ExecuteAsync_WithInvalidInputs_ReturnsValidationError(string email, string password)
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();

            // تنظیم DbSet خالی برای کاربران
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var service = new SignInUserService(mockContext.Object);

            var request = new SignInUserRequestInfo { Email = email, Password = password };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("نام کاربری یا رمز عبور وارد نگردیده است.", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ExecuteAsync_WithNonExistentUser_ReturnsUserNotFound()
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();

            // تنظیم DbSet خالی برای کاربران
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var service = new SignInUserService(mockContext.Object);

            var request = new SignInUserRequestInfo
            {
                Email = "nonexistent@example.com",
                Password = "aA123456"
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ایمیل به درستی وارد نشده است.", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ExecuteAsync_WithWrongPassword_ReturnsPasswordError()
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();

            var passwordHasher = new PasswordHasher();
            var correctPassword = "CorrectPassword123";
            var hashedPassword = passwordHasher.HashPassword(correctPassword);

            var existingUser = new User
            {
                Id = 1,
                Email = "user@example.com",
                Password = hashedPassword,
                UserName = "TestUser",
                PhoneNumber = "09123456789",
                Role = RoleEnum.Operator
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });

            var service = new SignInUserService(mockContext.Object);

            var request = new SignInUserRequestInfo
            {
                Email = "user@example.com",
                Password = "WrongPassword456"
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("رمز عبور اشتباه است.", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var mockContext = new Mock<IDataBaseContext>();

            var passwordHasher = new PasswordHasher();
            var rawPassword = "ValidPassword123";
            var hashedPassword = passwordHasher.HashPassword(rawPassword);

            var user = new User
            {
                Id = 42,
                Email = "validuser@example.com",
                Password = hashedPassword,
                UserName = "ValidUser",
                PhoneNumber = "09121234567",
                Role = RoleEnum.Operator
            };

            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { user });

            var service = new SignInUserService(mockContext.Object);

            var request = new SignInUserRequestInfo
            {
                Email = "validuser@example.com",
                Password = rawPassword
            };

            // Act
            var result = await service.ExcuteAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("ورود به سایت با موفقیت انجام شد", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(user.Id, result.Data.Id);
            Assert.Equal(user.UserName, result.Data.UserName);
            Assert.Equal(user.PhoneNumber, result.Data.Phone);
            mockContext.Verify(x => x.Users, Times.Once());
        }
    }
}

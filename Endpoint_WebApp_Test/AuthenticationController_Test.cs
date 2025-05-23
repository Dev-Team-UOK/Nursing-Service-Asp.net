using Endpoint_WebApp.Controllers;
using Endpoint_WebApp.Models.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Application.Services.Users.Queries.SignIn;
using Nursing_Service.Common.Dto.Base;

namespace Endpoint_WebApp_Test
{
    public class AuthenticationController_Test
    {
        //private readonly AuthenticationController? _authcontroller;
        //private readonly new Mock<ISignInUserService>? _singInServiceMock;
        //private readonly new Mock<ISignUpUserService>? _singUpServiceMock;
        //private AuthenticationController_Test()
        //{
        //    _authcontroller = new AuthenticationController(_singUpServiceMock.Object, _singInServiceMock.Object);
        //}

        [Fact]
        public void AuthenticationMethod_ReturnsAuthenticationViewResult()
        {
            //Arrange
            var signInMock = new Mock<ISignInUserService>();
            var signUpMock = new Mock<ISignUpUserService>();
            var authController = new AuthenticationController(signUpMock.Object, signInMock.Object);

            //Act
            var result = authController.Authentication();

            //Assert
            var trueResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Authentication", trueResult.ViewName);
        }

        [Theory]
        [InlineData("TestEmail@gmail.com", "aA123456")]
        public async Task SignInMethod_WithValidRequestInfo_ReturnsSuccessModelInJSON(string email, string password)
        {
            //Arrange
            var signInMock = new Mock<ISignInUserService>();
            var signUpMock = new Mock<ISignUpUserService>();

            signInMock.Setup(x => x.ExcuteAsync(It.IsAny<SignInUserRequestInfo>()))
                .ReturnsAsync(
                new BaseResultDTO<SignInUserResultDto>
                {
                    IsSuccess = true,
                });

            var authController = new AuthenticationController(signUpMock.Object, signInMock.Object);

            // Act
            var result = await authController.SignIn(email, password);

            // Assert
            // بررسی اینکه دیتای بازگشتی تایپ جی سان دارد
            var jsonResult = Assert.IsType<JsonResult>(result);
            // بررسی اینکه مدل داخل جیسان بازگشتی معتبر و صحیح است
            var resultModel = Assert.IsType<BaseResultDTO<SignInUserResultDto>>(jsonResult.Value);
            // بررسی اینکه دیتای بازگشتی نال نباشد
            Assert.NotNull(resultModel);
            // بررسی اینکه دیتای بازگشتی فیلد موفقیت آن درست باشد
            Assert.True(resultModel.IsSuccess);
            // بررسی اینکه دیتای ارسالی به سرویس همان دیتای وارد شده به اکشن کنترلر است
            // بررسی اینکه سرویس یک بار فراخوانی میشود
            signInMock.Verify(x => x.ExcuteAsync(
                It.Is<SignInUserRequestInfo>(r =>
                    r.Email == email &&
                    r.Password == password)
                ),
                Times.Once
            );
        }

        [Theory]
        [InlineDataAttribute("email@gmail.com", "aA123456", "userName", "09199111991")]
        public async Task SignUpMethod_WithValidRequestInfo_ReturnsSuccessModelInJSON(
            string email, 
            string password, 
            string userName, 
            string phone)
        {
            //Arrange
            var signInMock = new Mock<ISignInUserService>();
            var signUpMock = new Mock<ISignUpUserService>();

            signUpMock.Setup(x => x.ExcuteAsync(It.IsAny<SignUpUserRequestInfo>()))
                .ReturnsAsync(
                new BaseResultDTO<SignUpUserResultDto>
                {
                    IsSuccess = true,
                });

            var authController = new AuthenticationController(signUpMock.Object, signInMock.Object);

            // Act
            var result = await authController.SignUp(new SignUpViewModel
            {
                Email = email,
                Password = password,
                UserName = userName,
                Phone = phone
            });

            // Assert
            // بررسی اینکه دیتای بازگشتی تایپ جی سان دارد
            var jsonResult = Assert.IsType<JsonResult>(result);
            // بررسی اینکه مدل داخل جیسان بازگشتی معتبر و صحیح است
            var resultModel = Assert.IsType<BaseResultDTO<SignUpUserResultDto>>(jsonResult.Value);
            // بررسی اینکه دیتای بازگشتی نال نباشد
            Assert.NotNull(resultModel);
            // بررسی اینکه دیتای بازگشتی فیلد موفقیت آن درست باشد
            Assert.True(resultModel.IsSuccess);
            // بررسی اینکه دیتای ارسالی به سرویس همان دیتای وارد شده به اکشن کنترلر است
            // بررسی اینکه سرویس یک بار فراخوانی میشود
            signUpMock.Verify(x => x.ExcuteAsync(
                It.Is<SignUpUserRequestInfo>(r =>
                    r.Email == email &&
                    r.UserName == userName &&
                    r.Phone == phone &&
                    r.Password == password)
                ),
                Times.Once
            );
        }
    }
}

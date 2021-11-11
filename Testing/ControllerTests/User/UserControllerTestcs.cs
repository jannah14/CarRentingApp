using CarRentingApp.Controllers;
using CarRentingApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testing.ControllerTests.User
{
    public class UserControllerTestcs : IClassFixture<UserControllerFixture>
    {
        private readonly UserController _sut;
        private readonly UserControllerFixture _userF;

        public UserControllerTestcs( UserControllerFixture userF)
        {
            _userF = userF;

            _sut = new UserController(_userF._userRepoMock.Object, _userF._roleRepoMock.Object, _userF._loggerMock.Object);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Delete_ShouldReturn_BadRequest_For_InvalidInputParameter(string input)
        {
            //Arrenge
            var parameter = new Identifier { UserId = input };

            //Act
            var result = await _sut.Delete(parameter);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("abhdconxo")]
        [InlineData("mclenerfiorof")]
        public async void Delete_ShouldReturn_NotFount_For_WrongUserId(string input)
        {
            //Arrenge
            var parameter = new Identifier { UserId = input };

            _userF._userRepoMock.Setup(u => u.DeleteUser(input)).ReturnsAsync(false);

            //Act
            var result = await _sut.Delete(parameter);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Delete_ShouldReturn_Ok_For_CorrectUserId()
        {
            //Arrenge
            var input = "23f47ee9-46f4-4acc-b882-8b201bdec0dc"; //an existing user in database
            var parameter = new Identifier { UserId = input };

            _userF._userRepoMock.Setup(u => u.DeleteUser(input)).ReturnsAsync(true);

            //Act
            var result = await _sut.Delete(parameter);

            //Assert
            Assert.IsType<OkResult>(result);
        }




        public class UpdateUser : UserControllerTestcs
        {
            public UpdateUser(UserControllerFixture fixture) : base(fixture) { }

            public static IEnumerable<object[]> BadRequestData
            {
                get
                {
                    return new[]
                    {
                        new object[]{new UpdateUserDTO { Id = "", Role="Admin"} },
                        new object[]{new UpdateUserDTO { Id = null, Role="Admin"} },
                        new object[]{new UpdateUserDTO { Id = "23f47ee9-46f4-4acc-b882-8b201bdec0dc", Role=""} },
                        new object[]{new UpdateUserDTO { Id = "23f47ee9-46f4-4acc-b882-8b201bdec0dc", Role=null} }
                    };
                }
            }

            [Theory]
            [MemberData(nameof(BadRequestData))]
            public async void Should_return_BadRequest(UpdateUserDTO users)
            {
                //Arrange

                //Act
                var result = await _sut.UpdateUser(users);

                //Assert
                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void Should_return_NotModified()
            {
                //Arrange
                var parameter = new UpdateUserDTO
                {
                    Id = "a",
                    Role = "Admin",
                    Firstname = "Anna"
                };

                _userF._userRepoMock.Setup(u => u.UpdateUser(parameter)).ReturnsAsync(false);

                //Act
                var result = await _sut.UpdateUser(parameter);

                //Assert
                Assert.IsType<StatusCodeResult>(result);

                var statusCodeResult = result as StatusCodeResult;
                Assert.NotNull(statusCodeResult);

                Assert.Equal(StatusCodes.Status304NotModified, statusCodeResult.StatusCode);


            }


            [Fact]
            public async void Should_return_RedirecToAction()
            {
                //Arrange
                var parameter = new UpdateUserDTO
                {
                    Id = "23f47ee9-46f4-4acc-b882-8b201bdec0dc",
                    Role = "Admin",
                    Firstname = "Anna"
                };

                _userF._userRepoMock.Setup(u => u.UpdateUser(parameter)).ReturnsAsync(true);

                //Act
                var result = await _sut.UpdateUser(parameter);

                //Assert
                Assert.IsType<RedirectToActionResult>(result);
            }

        }

    }
}

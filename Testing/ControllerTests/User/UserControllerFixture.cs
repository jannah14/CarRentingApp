using CarRentingApp.Controllers;
using CarRentingApp.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Testing.ControllerTests.User
{
    public class UserControllerFixture
    {
        public Mock<IUserRepository> _userRepoMock { get; set; }
        public Mock<IRoleRepository> _roleRepoMock { get; set; }
        public Mock<ILogger<UserController>> _loggerMock { get; set; }

        public UserControllerFixture()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _roleRepoMock = new Mock<IRoleRepository>();
            _loggerMock = new Mock<ILogger<UserController>>();
        }
    }
}

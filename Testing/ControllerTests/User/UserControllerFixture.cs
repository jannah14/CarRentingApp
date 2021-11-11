using CarRentingApp.Repositories;
using Moq;

namespace Testing.ControllerTests.User
{
    public class UserControllerFixture
    {
        public Mock<IUserRepository> _userRepoMock { get; set; }
        public Mock<IRoleRepository> _roleRepoMock { get; set; }

        public UserControllerFixture()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _roleRepoMock = new Mock<IRoleRepository>();
        }
    }
}

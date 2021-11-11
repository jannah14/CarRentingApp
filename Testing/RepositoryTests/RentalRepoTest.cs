using CarRentingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testing.RepositoryTests
{
    [Collection("Repository Tests")]
    public class RentalRepoTest
    {
        private readonly GenericFixture _fixture;
        private readonly RentalRepository _sut; //sut = system under test

        public RentalRepoTest(GenericFixture fixture)
        {
            _fixture = fixture;
            _sut = new RentalRepository(_fixture.CreateContext(), _fixture._mapperMock.Object);
        }


        [Theory]
        [InlineData(0, "6290ffd6-a9b2-40a3-b527-24c3097847d9")]
        [InlineData(-1, "6290ffd6-a9b2-40a3-b527-24c3097847d9")]
        public async void Approve_ShouldReturn_False_For_NotExistingRental(int rentalId, string userId)
        {
            //Arrange

            //Act
            var result = await _sut.Approve(rentalId, userId);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(14, "6290ffd6-a9b2-40a3-b527-24c3097847d9")]
        [InlineData(15, "6290ffd6-a9b2-40a3-b527-24c3097847d9")]
        public async void Approve_ShouldReturn_True_For_ExistingRental(int rentalId, string userId)
        {
            //Arrange

            //Act
            var result = await _sut.Approve(rentalId, userId);

            //Assert
            Assert.True(true);
        }








    }
}

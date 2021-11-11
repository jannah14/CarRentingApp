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


        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public async void GetRentalById_ShouldReturn_Null_For_NonExistingRentalId(int rentalId)
        {
            //Arrange

            //Act
            var result = await _sut.GetRentalById(rentalId);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        public async void GetRentalById_ShouldReturn_Rental_For_ExistingRentalId(int rentalId)
        {
            //Arrange

            //Act
            var result = await _sut.GetRentalById(rentalId);

            //Assert
            Assert.NotNull(result); //it returns an object

            Assert.Equal(rentalId, result.Id); //it returns the expected rental from the database
        }


        [Fact]
        public async void GetUserRentals_ShouldReturn_RentalsOfThisUser()
        {
            //Arrange
            byte? filterByStatus = null;
            var userId = "6290ffd6-a9b2-40a3-b527-24c3097847d9";
            
            //Act
            var result = await _sut.GetUserRentals(userId, filterByStatus);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetUserRentals_ShouldReturn_Null_ForNonExistingUser()
        {
            //Arrange
            byte? filterByStatus = null;
            var userId = "test";

            //Act
            var result = await _sut.GetUserRentals(userId, filterByStatus);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("6290ffd6-a9b2-40a3-b527-24c3097847d9", 1)]
        [InlineData("6290ffd6-a9b2-40a3-b527-24c3097847d9", 0)]
        public async void GetUserRentals_ShouldReturn_Rentals_WithSpecificStatus(string userId, byte? status)
        {
            //Arrange

            //Act
            var result = await _sut.GetUserRentals(userId, status);

            //Assert
            Assert.NotNull(result);

            Assert.Collection(result, item => Assert.Equal(status, item.Status));
        }









    }
}

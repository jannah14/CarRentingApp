using AutoMapper;
using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Xunit;

namespace Testing.RepositoryTests
{
    public class GenericFixture
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        //public Mock<UserManager<AppUser>> _userManagerMock { get; set; }
        //public Mock<RoleManager<IdentityRole>> _roleManagerMock { get; set; }

        public Mock<IMapper> _mapperMock { get; set; }

        public GenericFixture()
        {
            Connection = new SqlConnection(@"Server=LAPTOP-0MC2BL5U;Database=CarRentingApp;Trusted_Connection=True;MultipleActiveResultSets=true");

            Connection.Open();

            //_userManagerMock = new Mock<UserManager<AppUser>>();
            //_roleManagerMock = new Mock<RoleManager<IdentityRole>>();
            _mapperMock = new Mock<IMapper>();
        }

        public DbConnection Connection { get; }

        public CarRentingAppContext CreateContext(DbTransaction transaction = null)
        {
            var context = new CarRentingAppContext(new DbContextOptionsBuilder<CarRentingAppContext>().UseSqlServer(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }
        public void Dispose() => Connection.Dispose();
    }

    [CollectionDefinition("Repository Tests")]
    public class RepoCollectionDefinition : ICollectionFixture<GenericFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces
    }


}

    


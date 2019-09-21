using System;
using System.Collections.Generic;
using AutoMapper;
using HealthCatalystUserSearchAPI.AutoMapper;
using HealthCatalystUserSearchAPI.Context;
using HealthCatalystUserSearchAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfEmployees()
        {
        //    //auto mapper configuration
        //    var mockMapper = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new MappingProfile());
        //    });
        //    var mapper = mockMapper.CreateMapper();

        //    var options = new DbContextOptionsBuilder<UserDbContext>()
        //        .UseInMemoryDatabase(databaseName: "WebApiTestingDb")
        //        .Options;
        //    using (var context = new UserDbContext(options))
        //    {
        //        var controller = new UsersController(mockRepo.Object);
        //    }

           

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<List<Employee>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        //private IEnumerable<Employee> GetTestEmployees()
        //{
        //    return new List<Employee>()
        //    {
        //        new Employee()
        //        {
        //            Id = 1,
        //            Name = "John"
        //        },
        //        new Employee()
        //        {
        //            Id = 2,
        //            Name = "Doe"
        //        }
        //    };
        }
    }
}

using System;
using Xunit;
using DotNetModel.Business;
using Moq;
using DotNetModel.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DotNetModel.DataEntity;
using DotNetModel.DataRepository;
using DotNetModel.Business.Impl;

namespace DotNetModel.WebApi.UnitTest
{
    public class ApplicationControllerTest
    {
        private readonly Mock<IApplicationDao> repositoryMoq;
        private readonly ApplicationBusiness business;

        public ApplicationControllerTest()
        {
            this.repositoryMoq =  new Mock<IApplicationDao>();
            this.business = new ApplicationBusiness(repositoryMoq.Object);
        }
        
        [Fact]
        public void TestGetById()
        {
            Application one = new Application
            {
                Id = 1,
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application two = new Application
            {
                Id = 2,
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application three = new Application
            {
                Id = 3,
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application empty = default(Application);


            repositoryMoq.Setup(m => m.FindById(It.IsAny<int>(), It.IsAny<string>())).Returns(empty);
            repositoryMoq.Setup(m => m.FindById(1, It.IsAny<string>())).Returns(one);
            repositoryMoq.Setup(m => m.FindById(2, It.IsAny<string>())).Returns(two);
            repositoryMoq.Setup(m => m.FindById(3, It.IsAny<string>())).Returns(three);

            Assert.Equal(one, business.GetById(1));
            Assert.NotEqual(two, business.GetById(1));
            Assert.NotEqual(three, business.GetById(1));

            Assert.NotEqual(one, business.GetById(2));
            Assert.Equal(two, business.GetById(2));
            Assert.NotEqual(three, business.GetById(2));

            Assert.NotEqual(one, business.GetById(3));
            Assert.NotEqual(two, business.GetById(3));
            Assert.Equal(three, business.GetById(3));

            Assert.Null(business.GetById(0));
        }
        
        [Fact]
        public void TestAdd()
        {
            Application exists = new Application
            {
                Id = 1,
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application complete = new Application
            {
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application saved = new Application
            {
                Id = 2,
                DebuggingMode = complete.DebuggingMode,
                PathLocal = complete.PathLocal,
                Url = complete.Url
            };

            Application noPathLocal = new Application
            {
                DebuggingMode = true,
                Url = "http://localhost:8080"
            };

            Application noUrl = new Application
            {
                DebuggingMode = true,
                PathLocal = "/var/www",
            };

            Application empty = default(Application);


            repositoryMoq.Setup(m => m.FindById(It.IsAny<int>(), It.IsAny<string>())).Returns(empty);
            repositoryMoq.Setup(m => m.FindById(exists.Id, It.IsAny<string>())).Returns(exists);
            repositoryMoq.Setup(m => m.FindById(saved.Id, It.IsAny<string>())).Returns(saved);
            
            string message;
            repositoryMoq.Setup(m => m.Insert(complete, out message, It.IsAny<string>())).Returns(saved.Id);

            Assert.Throws<ArgumentNullException>(() => business.Add(empty));
            Assert.Throws<Exception>(() => business.Add(exists));
            Assert.Throws<Exception>(() => business.Add(noPathLocal));
            Assert.Throws<Exception>(() => business.Add(noUrl));

            Assert.Equal(saved, business.Add(complete));
        }
        
        [Fact]
        public void TestAlter()
        {
            Application original = new Application
            {
                Id = 1,
                DebuggingMode = true,
                PathLocal = "/var/www",
                Url = "http://localhost:8080"
            };

            Application updated = new Application
            {
                Id = original.Id,
                DebuggingMode = false,
                PathLocal = original.PathLocal,
                Url = original.Url,
            };


            Application empty = default(Application);


            repositoryMoq.Setup(m => m.FindById(It.IsAny<int>(), It.IsAny<string>())).Returns(empty);
            repositoryMoq.Setup(m => m.FindById(original.Id, It.IsAny<string>())).Returns(original);
            
            string message;
            repositoryMoq.Setup(m => m.Update(updated, out message, It.IsAny<string>()));

            Assert.Throws<ArgumentNullException>(() => business.Alter(original.Id , empty));
            Assert.Throws<Exception>(() => business.Alter(5, updated));
            Assert.Throws<Exception>(() => business.Alter(0, updated));
            Assert.Throws<Exception>(() => business.Alter(original.Id, new Application()));
        }
    }
}

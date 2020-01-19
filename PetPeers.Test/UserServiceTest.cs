using Moq;
using NUnit.Framework;
using PetPeers.Models;
using PetPeers.Repo.Service;
using System;
using System.Linq;

namespace PetPeers.Test
{
    public class UserServiceUnitTest
    {
        Mock<AquanautDBContext> AquanautDBContext { get; set; }
        private string userid="1";
        private string password = "abcd@12345";
        [SetUp]
        public void Setup()
        {
            AquanautDBContext = new Mock<AquanautDBContext>();
        }

        [Test]
        [Order(1)]
        public void ValidateUser_WithVaildData_ReturnsListOfPets()
        {
            var userService = new UserService();
            var user = new Users { Userid = userid, Password = password };
            var result = userService.validateUser(user);
            Assert.AreEqual(true, result);
        }
        [Test]
        [Order(2)]
        public void InValidateUser_WithVaildData_ReturnsListOfPets()
        {
            var userService = new UserService();
            var user = new Users { Userid = new Random().Next(90001, 190000).ToString(), Password = password };
            var result = userService.validateUser(user);
            Assert.AreEqual(false, result);
        }

        [Test]
        [Order(3)]
        public void RegisterUser_WithVaildData_ReturnsListOfPets()
        {
            var userService = new UserService();
            var user = new Users { Userid = new Random().Next(90001, 190000).ToString(), Password = password, FirstName="Nagendra", LastName="Reddy" };
            var result = userService.addUser(user);
            Assert.AreEqual(true, result);
        }

        [Test]
        [Order(3)]
        public void RegisterUser_WithInVaildData_ReturnsListOfPets()
        {
            var userService = new UserService();
            var user = new Users { Userid = new Random().Next(90001, 190000).ToString(), Password = password, FirstName = "Nagendra", LastName = "Reddy" };
            var result = userService.addUser(user);
            Assert.AreEqual(false, result);
        }

    }
}
using Moq;
using NUnit.Framework;
using PetPeers.Models;
using PetPeers.Repo.Service;
using System;
using System.Linq;

namespace PetPeers.Test
{
    public class PetServiceUnitTest
    {
        Mock<AquanautDBContext> AquanautDBContext { get; set; }
        private int PetID;
        [SetUp]
        public void Setup()
        {
            AquanautDBContext = new Mock<AquanautDBContext>();
        }

        [Test]
        [Order(1)]
        public void AddPets_WithVaildData_ReturnsListOfPets()
        {
            int PetId = new Random().Next(90001, 190000);
            var petService = new PetService();
            Pets petObject=new Pets() { Id=PetId, Name = "George", Age = 10, Place = "Hyd", IsAvailable = 1, OwnerId = "a" };
            petService.AddPet(petObject);
            Assert.Pass();
        }
        [Test]
        [Order(2)]
        public void GetPets_WithVaildData_ReturnsListOfPets()
        {
            
            var fakePets = new Pets[]
                    {
                                    new Pets() { Name = "George", Age = 10 , Place="Hyd", IsAvailable=1,OwnerId="a"},
                                    new Pets() { Name = "George", Age = 12 , Place="Hyd", IsAvailable=1,OwnerId="a"},
                    };
            AquanautDBContext.Setup(c => c.Pets).ReturnsDbSet(fakePets);
            //var hackathonRepository = new PetService(AquanautDBContext.Object);
            var petService = new PetService();
            var result = petService.GetAllPets();
            if(result.Count()>0)
            {
                PetID=result.FirstOrDefault().Id;
            }
            Assert.Greater(result.Count(),2);
            Assert.Pass();
        }
        [Test]
        [Order(3)]
        public void GetUserPetsByID__WithVaildData_ReturnsListOfPets()
        {
            var petService = new PetService();
            var result = petService.GetUserPets(PetID);
            Assert.AreEqual(result.Count(), 0);
        }
        [Test]
        [Order(4)]
        public void GetUserPetsByID__WithInVaildData_ReturnsListOfPets()
        {
            var petService = new PetService();
            int PetId = new Random().Next(10000, 90000);
            var result = petService.GetUserPets(PetId);
            Assert.AreEqual(result.Count(), 0);
        }
    }
}
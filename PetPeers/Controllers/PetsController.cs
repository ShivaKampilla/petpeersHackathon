using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetPeers.Models;
using PetPeers.Repo.Interfaces;

namespace PetPeers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pets>> Get()
        {
            return _petService.GetAllPets();
        }

        [HttpGet]
        [Route("GetUserPets")]
        public ActionResult<IEnumerable<Pets>> GetpetsById(int id)
        {
            return _petService.GetUserPets(id);
        }


        // POST api/values
        [HttpPost]
        [Route("AddNewPet")]
        public void AddPost([FromBody] Pets p)
        {
            _petService.AddPet(p);

        }

        // POST api/values
        [HttpPost]
        [Route("UpdatePet")]
        public void updatePet([FromBody] Pets p)
        {
            _petService.AddPet(p);

        }

    }
}

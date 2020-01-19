using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetPeers.Models;
using PetPeers.Repo.Interfaces;

namespace PetPeers.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService  _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;


        }

        // POST api/values
        [HttpPost]
        [Route("IsValidUser")]
        public void ValidateUser([FromBody] Users user)
        {
            _userService.validateUser(user);

        }


        // POST api/values
        [HttpPost]
        [Route("RegisteredUser")]
        public void RegisterUser([FromBody] Users user)
        {
            _userService.addUser(user);

        }
    }
}
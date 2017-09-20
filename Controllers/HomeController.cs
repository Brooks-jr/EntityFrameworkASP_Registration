using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using firstEntityASP.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// ========================================================================================
// ========================================================================================


namespace firstEntityASP.Controllers
{
    public class HomeController : Controller
    {

// ========================================================================================
// ========================================================================================

        private HomeContext _context;

        public HomeController(HomeContext context)
        {
            _context = context;
        }

// ========================================================================================
// ========================================================================================

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Person> AllUsers = _context.User.ToList();
            ViewBag.Users = AllUsers;
            return View();
        }
// ========================================================================================
// ========================================================================================
        
        [HttpPost]
        [Route("")]
        public IActionResult Index(ValidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person newUser = new Person
                {
                    firstName = model.firstName,
                    lastName = model.lastName,
                    email = model.email,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                    
                };
                PasswordHasher<Person> hasher = new PasswordHasher<Person>();
                newUser.password = hasher.HashPassword(newUser, model.password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("currentUserId", newUser.id);
                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }  
// ========================================================================================
// ========================================================================================
        
        [HttpGet]
        [Route("Users")]
        public IActionResult Users()
        {
            List<Person> users = _context.User.OrderBy(user => user.firstName).ToList();
            ViewBag.Users = users;
            return View("Users");
        }
// ========================================================================================
// ========================================================================================














        // ========================================================================================
        // ADDING AN OBJECT TO DB
        // ========================================================================================
        // [HttpPost]
        // [Route("")]
        // public IActionResult Create(string FirstName, string LastName, string Email, string Password)
        // {
        //     Person newPerson = new Person
        //     {
        //         firstName = FirstName,
        //         lastName = LastName,
        //         email = Email,
        //         password = Password,

        //     };

        //     // if(passes validations){}
        //     _context.Add(newPerson);
        //     _context.SaveChanges();
        //     return RedirectToAction("index");
        //     // else{send back with error messages}
        // }
        // ========================================================================================
        // SHOW ALL OBJECTS
        // ========================================================================================
      
        // ========================================================================================
        // RETRIEVING OBJECT FORM DB
        // ========================================================================================

        // public IActionResult AllAdults()
        // {
        //     List<Person> ReturnedValues = _context.Users.Where(user => user.lastName == "stark").ToList();
        //     // Other code
        //     return View("index");
        // }

        // // ========================================================================================

        // public IActionResult GetOneUser(string Email)
        // {
        //     Person ReturnedValue = _context.Users.SingleOrDefault(user => user.email == Email);
        //     // Other code
        //     return View("index");
        // }

        // ========================================================================================
        // UPDATING TO DB OBJECT
        // ========================================================================================
        // public IActionResult Update(string firstName, string lastName, string email, string password)
        // {
        //     Person RetrievedUser = _context.Users.SingleOrDefault(user => user.id == 1);
        //     RetrievedUser.firstName = "New name";
        //     _context.SaveChanges();

        //     return View();
        // }
        // // ========================================================================================
        // // REMOVING FROM DB OBJECT
        // // ========================================================================================
        // public IActionResult Remove(string firstName, string lastName, string email, string password)
        // {

        //     Person RetrievedUser = _context.Users.SingleOrDefault(user => user.id == 1);
        //     _context.Users.Remove(RetrievedUser);
        //     _context.SaveChanges();

        //     return View();
        // }
        // ========================================================================================


    }


}




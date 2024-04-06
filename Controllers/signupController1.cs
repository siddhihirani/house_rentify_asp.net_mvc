using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using project.Models;
using System;
using System.Collections.Generic; // Import this namespace for List
using Microsoft.Extensions.Logging;

namespace project.Controllers
{
    public class signupController1 : Controller
    {
        login1 loginObj = new login1();
        //Signup signupObj = new Signup();
        ListProperty listPropertyObj = new ListProperty(); // Instantiate ListProperty object


        //public IActionResult Index()
        //{
        //    List<login1> lst = loginObj.getData();

        //    List<Signup> signups = signupObj.getData();
        //    return View(lst);

        //}

        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult signup(Signup signupData)
        {
            if (ModelState.IsValid)
            {
                // Assuming successful signup without storing data to the database.
                TempData["msg"] = "Signup successful. Please proceed to login.";

                // Redirect to login action after signup.
                return RedirectToAction("login");
            }
            return View();
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(login1 emp1)
        {
            bool res;

            if (ModelState.IsValid)
            {
                res = loginObj.insert(emp1);
                if (res)
                {
                    return RedirectToAction("home");
                }
                else
                {
                    TempData["msg"] = "Login failed. Please try again later.";
                }
            }
            return View();
        }

        public IActionResult home()
        {
            return View();
        }

        public IActionResult listproperty()
        {
            return View();
        }
        [HttpPost]
        public IActionResult listproperty(ListProperty propertyData)
        {
            if (ModelState.IsValid)
            {
                // Check if PhotoUrl is null, and if so, assign an empty string
                if (propertyData.PhotoUrl == null)
                {
                    propertyData.PhotoUrl = ""; // Or set it to null as per your requirement
                }

                // Assuming insert method handles data insertion for property
                bool success = listPropertyObj.InsertProperty(propertyData);
                if (success)
                {
                    return RedirectToAction("home"); // Redirect to home or any other action after successful insertion
                }
                else
                {
                    TempData["msg"] = "Property listing failed. Please try again later.";
                }
            }
            return View();
        }

        public IActionResult referowner()
        {
            return View();
        }
        public IActionResult referandearn()
        {
            return View();
        }
        public IActionResult viewproperty()
        {

            List<ListProperty> properties = listPropertyObj.GetAllProperties();
            return View(properties);
           
        }
        public IActionResult aboutus()
        {
            return View();
        }
        public IActionResult faqs()
        {
            return View();
        }
        public IActionResult mycart()
        {
            return View();
        }
        public IActionResult propertyinfo()
        {
            return View();
        }

    }
}

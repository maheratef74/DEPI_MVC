﻿using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class PassDataController : Controller
    {
        #region Cookies
        public IActionResult SetCookie()
        {
            // Session Cookie ➡️➡️ Lifetime = Session Lifetime
            Response.Cookies.Append("name", "Alex");

            // Persistent Cookie ➡️➡️ Lifetime = Configurable
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(1);
            Response.Cookies.Append("department", ".Net", cookieOptions);

            return Content("Cookie saved");
        }
        public IActionResult GetCookie()
        {
            var name = Request.Cookies["name"];
            var department = Request.Cookies["department"];

            return Content($"Cookie : name = {name}, deparment = {department}");
        }
        #endregion
        #region Session
        public IActionResult SetSession()
        {
            // HttpContext.Session.Set()                                          ➡️➡️ < string , byte[] >
            HttpContext.Session.SetString("myKey", "Hiiiiiiiiiiiii"); //➡️➡️ < string , string >
            HttpContext.Session.SetInt32("myAge", 27);                //➡️➡️ < string , int >
            return Content("Session saved");
        }

        public IActionResult GetSession()
        {
            var msg = HttpContext.Session.GetString("myKey"); //➡️➡️ < string , string >
            var age = HttpContext.Session.GetInt32("myAge");                //➡️➡️ < string , int >
            return Content($"from session ➡️ myKey = {msg}, age = {age}");
        }
        #endregion
        #region Temp Data
        public IActionResult First()
        {
            TempData["msg"] = "Data Saved from First Action";

            return Content("First Request: ➡️➡️ Data Saved");
        }
        public IActionResult Second()
        {
            string message = "Empty";
            if(TempData.ContainsKey("msg"))
            {
                //message = TempData["msg"].ToString(); // 🚩 First Read  ➡️➡️ mark the key for deletion
                //TempData.Keep("msg");                 //                 ➡️➡️ mark the key for retention


                message = TempData.Peek("msg").ToString();  // maintains the key and the value
            }
            return Content("Second Request: " + message);
        }
        public IActionResult Third()
        {
            string message = "Empty";
            if (TempData.ContainsKey("msg"))
            {
                message = TempData["msg"].ToString(); // 🚩 First Read  ➡️➡️ mark the key for deletion
            }
            return Content("Third Request: " + message);
        }
        #endregion
    }
}

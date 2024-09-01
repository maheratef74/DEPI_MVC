using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class FirstController : Controller
    {
        // Routing      /Controller_Name/Action_Name

        // 🚩🚩Action 
        //===============================
        //      ✅ must be public
        //      ✅ cannot be static
        //      ✅ cannot be overloaded
        //      ✅ can return string , json , view , files
        //      ✅ Result Data Types
        //      ✅ String ==> ContentResult   ===> Content
        //      ✅ View ==> ViewResult   ===> View
        //      ✅ Javascript ==> JavascriptResult
        //      ✅ Json ==> JsonResult 
        //      ✅ Files ==> FileResult 
        //public string Welcome()
        //{
        //    return "Welcome from my first page";
        //}
        public ContentResult Welcome()
        {
            var result = new ContentResult();

            result.Content = "Welcome from my first page";

            return result;
        }

        // No Overloading ❌❌
        //=============================
        //public ContentResult Welcome(int id)
        //{
        //    var result = new ContentResult();

        //    result.Content = "Welcome from my first page";

        //    return result;
        //}

        public JsonResult getJson()
        {
            return new JsonResult(new { Id = 1, Name = "Ibrahim" });
        }

        // 🚀🚀 All Result Classes inherit from ActionResult and implement IActionResult
        public IActionResult getMix()
        {
            if (DateTime.Now.Day == 27)
            {
                //var result = new ContentResult();

                //result.Content = "Page Closed";

                //return result;


                return Content("Page Closed");
            }
            else
            {
                //return new JsonResult(new { Id = 1, Name = "Ibrahim" });

                return Json(new { Id = 1, Name = "Ibrahim" });
            }
        }

        public ViewResult getData()
        {
            //var result = new ViewResult();

            //result.ViewName = "MyView";

            //return result;


            // return View("MyView");


            // 🚩🚩 An unhandled exception occurred while processing the request.
            // 🚩🚩 InvalidOperationException: The view 'MyView' was not found.
            // The following locations were searched:
            // 🚩🚩 /Views/First/MyView.cshtml
            // 🚩🚩 /Views/Shared/MyView.cshtml
            return View("MyView2");
        }
        public IActionResult MyView()
        {
            return View();
        }
    }
}

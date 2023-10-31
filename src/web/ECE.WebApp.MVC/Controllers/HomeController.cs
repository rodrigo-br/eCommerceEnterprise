using ECE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id) 
        {
            var modelError = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelError.Message = "An unexpected error ocurred. Try again later or contact the owner";
                    modelError.Title = "Error!";
                    modelError.ErrorCode = id;
                    break;
                case 404:
					modelError.Message = "The page doesn't exist. If you believe it should, contact the owner";
					modelError.Title = "Page not found";
					modelError.ErrorCode = id;
                    break;
                case 403:
					modelError.Message = "Not allowed. You have no rights to continue";
					modelError.Title = "Access denied";
					modelError.ErrorCode = id;
					break;
                default:
                    return StatusCode(404);
			}

            return View("Error", modelError);
        }
    }
}
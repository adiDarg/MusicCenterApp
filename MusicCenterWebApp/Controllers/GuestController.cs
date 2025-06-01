using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using NuGet.Protocol;
using System.Net;
using System.Net.Mail;
using Utility;
using WebApiClient;

namespace MusicCenterWebApp.Controllers
{
    public class GuestController : Controller
    {
        //Default page of the website
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.SetString("userType", "Guest");
            TempData["userType"] = "Guest";
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] User user, [FromForm] IFormFile image)
        {
            if (user.Name == null || user.Password == null)
            {
                TempData["errorMessage"] = "Must enter username and password";
                return View("RegistrationForm");
            }
            if (await IsUsernameTaken(user.Name))
            {
                TempData["errorMessage"] = "Username already taken";
                DbContext.GetInstance().CloseConnection();
                return View("RegistrationForm");
            }
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), 
                    "wwwroot", "uploads", "images");
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                user.Image = "/uploads/images/" + fileName;
            }
            else
            {
                user.Image = "/uploads/images/placeholder.jpg";
            }

            user.Email = user.Email == null ? "" : user.Email;
            user.Address = user.Address == null ? "" : user.Address;
            user.PhoneNumber = user.PhoneNumber == null ? "" : user.PhoneNumber;

            WebClient<User> client = new WebClient<User>();
            client.port = 5004;
            client.Path = "api/Guest/AddUser";
            client.AddParams("user", user.ToJson());
            bool success = await client.PostAsync(user);
            TempData["isRegistrerSuccessful"] = success;
            if (!success)
            {
                return View("RegistrationForm");
            }

            DbContext.GetInstance().OpenConnection();
            while ((new RepositoryUOW()).GetUserRepository().GetByUsername(user.Name) == null)
            {
                Thread.Sleep(500);
            }
            User user1 = (new RepositoryUOW()).GetUserRepository().GetByUsername(user.Name);
            TempData["validationKeySuccess"] = EmailUtils.SendValidationKeyEmail(user.Email, user1.ValidationKey);
            DbContext.GetInstance().CloseConnection();
            return RedirectToAction("LoginForm");
        }

        [HttpGet]
        private async Task<bool> IsUsernameTaken(string username)
        {
            WebClient<bool> webClient = new WebClient<bool>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/IsUsernameTaken";
            webClient.AddParams("username", username);
            return await webClient.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]string username, [FromForm]string password)
        {
            WebClient<String> client = new WebClient<String>();
            client.port = 5004;
            client.Path = "api/Guest/Login";
            client.AddParams("username", username);
            client.AddParams("password", password);
            string? userID = await client.GetAsync();
            if (userID != null)
            {
                HttpContext.Session.SetString("userID", userID);
                TempData["userID"] = userID;
                string type = UserTypeGetter.GetUserType(userID);
                TempData["userType"] = type;
                HttpContext.Session.SetString("userType", type);
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "User");
            }
            ViewBag.Error = true;
            return View("LoginForm");
        }
    }
}

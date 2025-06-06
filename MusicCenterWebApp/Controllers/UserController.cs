using Microsoft.AspNetCore.Mvc;
using WebApiClient;
using MusicCenterWebService.Repositories;
using MusicCenterModels;
using MusicCenterWebService;
using NuGet.Protocol;
using Utility;
using System.Diagnostics.Metrics;

namespace MusicCenterWebApp.Controllers
{
   
    public class UserController : Controller
    {
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        private string? userID => HttpContext.Session.GetString("userID");

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdditionalActions()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RequestsSent(){
            WebClient<List<Request>> client = new WebClient<List<Request>>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/ShowRequestsSent";
            client.AddParams("userID", userID);
            List<Request>? requests = await client.GetAsync();
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> RequestsRecieved()
        {
            WebClient<List<Request>> client = new WebClient<List<Request>>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/ShowRequestsRecieved";
            client.AddParams("userID", userID);
            List<Request>? requests = await client.GetAsync();
            ViewBag.userID = userID;
            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> HandleRequest([FromForm] Request request)
        {
            WebClient<Request> webClient = new WebClient<Request>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/HandleRequest";
            webClient.AddParams("request", request.ToJson());
            TempData["requestHandled"] = await webClient.PostAsync(request);
            return RedirectToAction("RequestsRecieved");
        }

        [HttpGet]
        public async Task<IActionResult> SendRequest()
        {
            ViewBag.userID = userID;
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetUsers";
            List<User> users = await webClient.GetAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> HandleSendRequest(
            [FromForm] string RequestID,
            [FromForm] string Title,
            [FromForm] string Description,
            [FromForm] bool IsSeen,
            [FromForm] bool IsApproved,
            [FromForm] string SenderID,
            [FromForm] string RequestType,
            [FromForm] string RecieverID)
        {
            Request request = new Request();
            request.Id = RequestID;
            request.Title = Title;
            request.Description = Description;
            request.IsSeen = IsSeen;
            request.IsApproved = IsApproved;
            request.Sender = new User();
            request.Sender.Id = SenderID;
            request.RequestType = RequestType;
            request.Reciever = new User();
            request.Reciever.Id = RecieverID;
            WebClient<Request> client = new WebClient<Request>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/SendRequest";
            client.AddParams("request", request.ToJson());
            TempData["requestSentSuccessfully"] = await client.PostAsync(request);
            return RedirectToAction("RequestsSent");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            WebClient<User> webClient = new WebClient<User>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetUserById";
            webClient.AddParams("userID",userID);
            User user = await webClient.GetAsync();
            ViewBag.PostUpdate = false;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm]User user, [FromForm] IFormFile image) {
            if (image != null && image.Length > 0)
            {
                string? path = await CopyImage(image);
                if (path != null)
                {
                    user.Image = path;
                }
                else
                {
                    user.Image = GetUserImage();
                }
            }
            else
            {
                user.Image = GetUserImage();
            }

            user.Email = user.Email == null ? "" : user.Email;
            user.Address = user.Address == null? "" : user.Address;
            user.PhoneNumber = user.PhoneNumber == null ? "" : user.PhoneNumber;

            WebClient<User> client = new WebClient<User>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/UpdateProfile";
            client.AddParams("user", user.ToJson());
            bool isUpdated =  await client.PostAsync(user);
            ViewBag.PostUpdate = true;
            ViewBag.isUpdated = isUpdated;

            DbContext.GetInstance().OpenConnection();
            int counter = 0;
            while (!repositoryUOW.GetUserRepository().GetById(userID).Equals(user) && counter < 10){
                Thread.Sleep(500);
                counter++;
            }
            DbContext.GetInstance().CloseConnection();

            return Redirect("UpdateProfile");
        }

        [HttpPost]
        private async Task<string?> CopyImage(IFormFile image)
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/UploadImage";
            using (var stream = image.OpenReadStream())
            {
                return await webClient.PostAsync<string>(stream, image.FileName);
            }
        }
        
        [HttpGet]
        private string GetUserImage()
        {
            WebClient<User> webClient = new WebClient<User>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetUserById";
            webClient.AddParams("userID", userID);
            return webClient.GetAsync().Result.Image;
        }

        [HttpGet]
        public IActionResult EnterValidationKey()
        {
            ViewBag.userID = userID;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> HandleValidationKey(
            [FromForm] string userID,
            [FromForm] string key)
        {
            WebClient<User> webClient = new WebClient<User>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/EnterValidationKey";
            webClient.AddParams("userID", userID);
            webClient.AddParams("key", key);
            bool result = await webClient.PostAsync(new User());
            if (result)
            {
                HttpContext.Session.SetString("userType", "Registree");
                TempData["userType"] = "Registree";
            }
            TempData["isKeyValid"] = result;
            return Redirect("EnterValidationKey");
        }
        
        [HttpGet]
        public async Task<IActionResult> SendValidationKey()
        {
            WebClient<User> webClient = new WebClient<User>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetUserById";
            webClient.AddParams("userID", userID);
            User user = await webClient.GetAsync();
            bool? success = null;
            if (user.Email.Equals(""))
            {
                TempData["message"] = "Please enter Email address before trying to recieve validation key(to add email address, go to View Profile)";
            }
            else
            {
                success = EmailUtils.SendValidationKeyEmail(user.Email, user.ValidationKey);
            }
            if (success != null && !(bool)success)
            {
                TempData["message"] = "Validation key failed to send. Try checking if Email address is correct";
            }
            if (success != null && (bool)success)
            {
                TempData["message"] = "Validation key sent to your email!";
            }
            return RedirectToAction("AdditionalActions");
        }
        [HttpGet]
        public async Task<IActionResult> ShowMessages()
        {
            WebClient<List<Message>> client = new WebClient<List<Message>>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/ShowMessages";
            client.AddParams("userID", userID);
            List<Message>? messages = await client.GetAsync();
            return View(messages);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("userType", "Guest");
            TempData["userType"] = "Guest";
            HttpContext.Session.SetString("userID", "");
            TempData["userID"] = "";
            return RedirectToAction("Index","Guest");
        }
    }
}

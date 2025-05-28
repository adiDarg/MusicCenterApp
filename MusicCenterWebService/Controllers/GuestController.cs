using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicCenterModels;
using MusicCenterWebService.Repositories;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MusicCenterWebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private RepositoryUOW repositoryUOW;
        public GuestController()
        {
            repositoryUOW = new RepositoryUOW();
        }
        
        [HttpPost]
        public bool AddUser(User user)
        {
            if (repositoryUOW.GetUserRepository().GetByUsername(user.Name) != null)
            {
                return false;
            }
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder key = new StringBuilder();
            Random random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                char sample = chars.ToCharArray()[random.Next(chars.Length)];
                key.Append(sample);
            }
            user.ValidationKey = key.ToString();
            return repositoryUOW.GetUserRepository().Create(user);
        }

        [HttpGet]
        public IActionResult? Login(string username, string password)
        {
            try
            {
                User user = repositoryUOW.GetUserRepository().GetByUsername(username);
                if (!user.Password.Equals(password))
                {
                    return null;
                }
                return Content(JsonSerializer.Serialize(user.Id), "application/json");
            }
            catch
            {
                return null;
            }
        }
    }
}

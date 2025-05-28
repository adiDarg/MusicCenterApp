using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MusicCenterModels;
using System.Configuration;
using System.Text;
using WebApiClient;

namespace MusicCenterWebApp.Controllers
{
    public class RegistreeController : Controller
    {
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
        public async Task<IActionResult> GetSchedule()
        {
            WebClient<ScheduleViewModel> client = new WebClient<ScheduleViewModel>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Registree/GetSchedule";
            client.AddParams("RegistreeID", HttpContext.Session.GetString("userID").ToString());
            ScheduleViewModel events = await client.GetAsync();
            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> ViewTeachers()
        {
            WebClient<List<Teacher>> client = new WebClient<List<Teacher>>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Registree/ViewAllTeachers";
            List<Teacher> teachers = await client.GetAsync();
            return View(teachers);
        }
    }
}

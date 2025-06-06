using Microsoft.AspNetCore.Mvc;
using MusicCenterModels;
using MusicCenterWebService;
using WebApiClient;

namespace MusicCenterWebApp.Controllers
{
    public class TeacherController : Controller
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
        public async Task<IActionResult> AddInstrument()
        {
            WebClient<List<Instrument>> webClient = new WebClient<List<Instrument>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Teacher/GetNewInstruments";
            webClient.AddParams("teacherID", HttpContext.Session.GetString("userID"));
            List<Instrument> instruments = await webClient.GetAsync();
            return View(instruments);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewInstrument([FromForm] string instrumentID)
        {
            if (instrumentID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("AddInstrument");
            }
            WebClient<Instrument> webClient = new WebClient<Instrument>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Teacher/AddInstrument";
            webClient.AddParams("teacherID", HttpContext.Session.GetString("userID"));
            webClient.AddParams("instrumentID", instrumentID);
            bool success = await webClient.PostAsync(new Instrument());
            TempData["message"] = success ? "Instrument added." : "Failed to add instrument.";
            return Redirect("AddInstrument");
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule()
        {
            WebClient<ScheduleViewModel> client = new WebClient<ScheduleViewModel>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Teacher/GetSchedule";
            client.AddParams("TeacherID", HttpContext.Session.GetString("userID").ToString());
            ScheduleViewModel events = await client.GetAsync();
            return View(events);
        }
    }
}

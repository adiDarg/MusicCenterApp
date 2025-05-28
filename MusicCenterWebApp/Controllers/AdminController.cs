    using Microsoft.AspNetCore.Mvc;
using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using NuGet.Protocol;
using System.Text.Json;
using WebApiClient;

namespace MusicCenterWebApp.Controllers
{
    public class AdminController : Controller
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
        public async Task<IActionResult> CreateGroup()
        {
            WebClient<List<Instructor>> client = new WebClient<List<Instructor>>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Admin/GetInstructors";
            List<Instructor> instructors = await client.GetAsync();
            return View(instructors);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCreateGroup([FromForm]Group group, [FromForm] string instructorID)
        {
            DbContext.GetInstance().OpenConnection();
            Instructor instructor = new RepositoryUOW().GetInstructorRepository().GetById(instructorID);
            group.Instructor = instructor;
            DbContext.GetInstance().CloseConnection();
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateGroup";
            webClient.AddParams("group", group.ToJson());
            bool success = await webClient.PostAsync(group);
            TempData["groupSubmitSuccess"] = success;
            return Redirect("CreateGroup");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInstructorToGroup()
        {
            WebClient<UpdateGroupInstructorViewModel> client = new WebClient<UpdateGroupInstructorViewModel>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Admin/GetInstructorsAndGroups";
            UpdateGroupInstructorViewModel viewModel = await client.GetAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddInstructorToGroup([FromForm] string groupID, [FromForm] string instructorID)
        {
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AssignInstructorToGroup";
            webClient.AddParams("groupID", groupID);
            webClient.AddParams("instructorID", instructorID);
            bool success = await webClient.PostAsync(new Group());
            TempData["instructorUpdateSuccess"] = success;
            return Redirect("UpdateInstructorToGroup");
        }

        [HttpGet]
        public async Task<IActionResult> AddRegistreeToGroup()
        {
            WebClient<AddGroupRegistreeViewModel> client = new WebClient<AddGroupRegistreeViewModel>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/Admin/GetRegistreesAndGroups";
            AddGroupRegistreeViewModel viewModel = await client.GetAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAddRegistreeToGroup([FromForm] string groupID, [FromForm] string registreeID)
        {
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AddRegistreeToGroup";
            webClient.AddParams("groupID", groupID);
            webClient.AddParams("registreeID", registreeID);
            bool success = await webClient.PostAsync(new Group());
            TempData["AddRegistreeSuccess"] = success;
            return Redirect("AddRegistreeToGroup");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllMembers([FromQuery]string type)
        {
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = $"api/Admin/Get{type}s";
            List<User> members = await webClient.GetAsync();
            return View(members);
        }

        [HttpGet]
        public IActionResult ChooseMemberTypeToDisplay()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PromoteToTeacher()
        {
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetNonTeacherUsers";
            List<User> users = await webClient.GetAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> MakeTeacher([FromForm] string userID)
        {
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/MakeUserTeacher";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["isUserTeacher"] = success;
            return Redirect("PromoteToTeacher");
        }
        [HttpGet]
        public async Task<IActionResult> PromoteToInstructor()
        {
            WebClient<List<Teacher>> webClient = new WebClient<List<Teacher>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetNonInstructorTeachers";
            List<Teacher> teachers = await webClient.GetAsync();
            return View(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> MakeInstructor([FromForm] string userID)
        {
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/MakeUserInstructor";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["isTeacherInstructor"] = success;
            return Redirect("PromoteToInstructor");
        }
        [HttpGet]
        public async Task<IActionResult> DemoteFromTeacher()
        {
            WebClient<List<Teacher>> webClient = new WebClient<List<Teacher>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetNonInstructorTeachers";
            List<Teacher> teachers = await webClient.GetAsync();
            return View(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> DemoteTeacher([FromForm] string userID)
        {
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/DemoteTeacher";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["isTeacherDemoted"] = success;
            return Redirect("DemoteFromTeacher");
        }
        [HttpGet]
        public async Task<IActionResult> DemoteFromInstructor()
        {
            WebClient<List<Instructor>> webClient = new WebClient<List<Instructor>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetInstructors";
            List<Instructor> instructors = await webClient.GetAsync();
            return View(instructors);
        }

        [HttpPost]
        public async Task<IActionResult> DemoteInstructor([FromForm] string userID)
        {
            WebClient<Instructor> webClient = new WebClient<Instructor>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/DemoteInstructor";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Instructor());
            TempData["isInstructorDemoted"] = success;
            return Redirect("DemoteFromInstructor");
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleLesson()
        {
            WebClient<List<Registree>> webClient = new WebClient<List<Registree>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetRegistrees";
            List<Registree> registrees = await webClient.GetAsync();
            return View(registrees);
        }

        [HttpGet]
        public async Task<IActionResult> ChooseTeacherForLesson([FromQuery] string registreeID)
        {
            WebClient<List<Teacher>> webClient = new WebClient<List<Teacher>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetTeachersByRegistree";
            webClient.AddParams("registreeID", registreeID);
            List<Teacher> teachers = await webClient.GetAsync();
            ViewBag.registreeID = registreeID;
            return View(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(
            [FromForm] string registreeID,
            [FromForm] string teacherID,
            [FromForm] string room,
            [FromForm] string date)
        {
            RepositoryUOW repository = new RepositoryUOW();
            Lesson lesson = new Lesson();
            DbContext.GetInstance().OpenConnection();
            lesson.Student = repository.GetRegistreeRepository().GetById(registreeID);
            lesson.Teacher = repository.GetTeacherRepository().GetById(teacherID);
            DbContext.GetInstance().CloseConnection();
            lesson.Room = room;
            lesson.Date = date;
            
            WebClient<Lesson> webClient = new WebClient<Lesson>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateLesson";
            webClient.AddParams("lesson", lesson.ToJson());
            bool success = await webClient.PostAsync(lesson);
            TempData["isLessonCreated"] = success;
            return Redirect("ScheduleLesson");
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleMeeting()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetGroups";
            List<Group> groups = await webClient.GetAsync();
            return View(groups);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeeting(
            [FromForm] string groupID,
            [FromForm] string date,
            [FromForm] string room)
        {
            RepositoryUOW repository = new RepositoryUOW();
            Meeting meeting = new Meeting();
            DbContext.GetInstance().OpenConnection();
            meeting.Group = repository.GetGroupRepository().GetById(groupID);
            DbContext.GetInstance().CloseConnection();
            meeting.Room = room;
            meeting.Date = date;

            WebClient<Meeting> webClient = new WebClient<Meeting>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateMeeting";
            webClient.AddParams("meeting", meeting.ToJson());
            bool success = await webClient.PostAsync(meeting);
            TempData["isMeetingCreated"] = success;
            return Redirect("ScheduleMeeting");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveParticipantFromGroup()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetGroups";
            List<Group> groups = await webClient.GetAsync();
            DbContext.GetInstance().OpenConnection();
            RepositoryUOW repository = new RepositoryUOW();
            foreach (Group group in groups)
            {
                group.Participants = repository.GetRegistreeRepository().GetAllByGroupId(group.Id);
            }
            DbContext.GetInstance().CloseConnection();
            return View(groups);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStudentFromGroup(
            [FromForm] string groupID,
            [FromForm] string userID)
        {
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/RemoveStudentFromGroup";
            webClient.AddParams("groupID",groupID);
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Group());
            TempData["isParticipantRemoved"] = success;
            return Redirect("RemoveParticipantFromGroup");
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule()
        {
            WebClient<ScheduleViewModel> webClient = new WebClient<ScheduleViewModel>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetSchedule";
            ScheduleViewModel model = await webClient.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SendMessageForm()
        {
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetUsers";
            List<User> users = await webClient.GetAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] Message message, [FromForm]string recieversIDs)
        {
            List<string> ids = JsonSerializer.Deserialize<List<string>>(recieversIDs).ToList();
            DbContext.GetInstance().OpenConnection();
            Message? dbMessage = new RepositoryUOW().GetMessageRepository().GetByTitleAndDescription(message.Title, message.Description);
            DbContext.GetInstance().CloseConnection();
            bool success = true;
            if (dbMessage == null)
            {
                WebClient<Message> webClient = new WebClient<Message>();
                webClient.port = 5004;
                webClient.Host = "localhost";
                webClient.Path = "api/Admin/SendMessage";
                webClient.AddParams("message", message.ToJson());
                success = await webClient.PostAsync(message);
                DbContext.GetInstance().OpenConnection();
                while (dbMessage == null)
                {
                    dbMessage = new RepositoryUOW().GetMessageRepository().GetByTitleAndDescription(message.Title, message.Description);
                    Thread.Sleep(500);
                }
                DbContext.GetInstance().CloseConnection();
            }
            message = dbMessage;
            bool firstFail = true;
            TempData["errorMessage"] = success ? "" : "Failed to post message";
            if (!success)
            {
                TempData["messageSent"] = false;
                return Redirect("SendMessageForm");
            }
            foreach (string id in ids) {
                success &= await AddReceiver(message, id);
                if (!success)
                {
                    DbContext.GetInstance().OpenConnection();
                    TempData["errorMesssage"] += (firstFail? "" : ", \n") + "Failed to add " + new RepositoryUOW().GetUserRepository().GetById(id).Name;
                    DbContext.GetInstance().CloseConnection();
                    firstFail = false;
                }
            }
            TempData["messageSent"] = success;
            return Redirect("SendMessageForm");
        }
        [HttpPost]
        private async Task<bool> AddReceiver(Message message, string userID)
        {
            WebClient<Message> webClient = new WebClient<Message>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AddReceiver";
            webClient.AddParams("messageID", message.Id);
            webClient.AddParams("receiverID", userID);
            return await webClient.PostAsync(message);
        }
    }
}

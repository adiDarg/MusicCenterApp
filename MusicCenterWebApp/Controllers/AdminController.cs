    using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
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
        public async Task<IActionResult> SubmitCreateGroup([FromForm] Group group, [FromForm] string instructorID)
        {
            if (group.Name == null || group.Room == null || instructorID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("CreateGroup");
            }
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateGroup";
            group.Instructor = new Instructor { Id = instructorID };
            webClient.AddParams("group", group.ToJson());
            bool success = await webClient.PostAsync(group);
            TempData["message"] = success ? "Group created." : "Group creation failed.";
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
            if (groupID == "0" || instructorID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("UpdateInstructorToGroup");
            }
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AssignInstructorToGroup";
            webClient.AddParams("groupID", groupID);
            webClient.AddParams("instructorID", instructorID);
            bool success = await webClient.PostAsync(new Group());
            TempData["message"] = success ? "Instructor updated." : "Failed to update instructor.";
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
            if (groupID == "0" || registreeID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("AddRegistreeToGroup");
            }
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AddRegistreeToGroup";
            webClient.AddParams("groupID", groupID);
            webClient.AddParams("registreeID", registreeID);
            bool success = await webClient.PostAsync(new Group());
            TempData["message"] = success ? "Registree added." : "Failed to add registree.";
            return Redirect("AddRegistreeToGroup");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllMembers([FromQuery] string type)
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
            if (userID == "0") {
                TempData["message"] = "Please fill all fields";
                return Redirect("PromoteToTeacher");
            }
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/MakeUserTeacher";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["message"] = success ? "User promoted." : "Failed to promote.";
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
            if (userID == "0") {
                TempData["message"] = "Please fill all fields";
                return Redirect("PromoteToInstructor");
            }
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/MakeUserInstructor";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["message"] = success ? "Teacher promoted." : "Failed to promote teacher.";
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
            if (userID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("DemoteFromTeacher");
            }
            WebClient<Teacher> webClient = new WebClient<Teacher>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/DemoteTeacher";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["message"] = success ? "Teacher demoted." : "Failed to demote teacher.";
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
            if (userID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("DemoteFromInstructor");
            }
            WebClient<Instructor> webClient = new WebClient<Instructor>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/DemoteInstructor";
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Instructor());
            TempData["message"] = success ? "Demoted instructor." : "Failed to demote instructor.";
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
            if (registreeID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("ScheduleLesson");
            }
            WebClient<List<Teacher>> webClient = new WebClient<List<Teacher>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetTeachersByRegistree";
            webClient.AddParams("registreeID", registreeID);
            List<Teacher> teachers = await webClient.GetAsync();
            if (teachers.Count == 0)
            {
                TempData["message"] = "Cant schedule lesson for user with no teachers.";
                return Redirect("ScheduleLesson");
            }
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
            if (registreeID == "0" || teacherID == "0" || room == null || date == null)
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("ScheduleLesson");
            }
            Lesson lesson = new Lesson();
            lesson.Student = new Registree { Id = registreeID };
            lesson.Teacher = new Teacher { Id = teacherID };
            lesson.Room = room;
            lesson.Date = date;

            WebClient<Lesson> webClient = new WebClient<Lesson>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateLesson";
            webClient.AddParams("lesson", lesson.ToJson());
            bool success = await webClient.PostAsync(lesson);
            TempData["message"] = success ? "Lesson scheduled." : "Failed to schedule lesson.";
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
            if (groupID == "0" || date == null || room == null)
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("ScheduleMeeting");
            }
            RepositoryUOW repository = new RepositoryUOW();
            Meeting meeting = new Meeting();
            meeting.Group = new Group { Id = groupID };
            meeting.Room = room;
            meeting.Date = date;

            WebClient<Meeting> webClient = new WebClient<Meeting>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/CreateMeeting";
            webClient.AddParams("meeting", meeting.ToJson());
            bool success = await webClient.PostAsync(meeting);
            TempData["message"] = success? "Meeting created.":"Failed to create meeting.";
            return Redirect("ScheduleMeeting");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveParticipantFromGroup()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetGroupsWithParticipants";
            List<Group> groups = await webClient.GetAsync();
            return View(groups);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStudentFromGroup(
            [FromForm] string groupID,
            [FromForm] string userID)
        {
            if (userID == "0" || groupID == "0")
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("RemoveParticipantFromGroup");
            }
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/RemoveStudentFromGroup";
            webClient.AddParams("groupID",groupID);
            webClient.AddParams("userID", userID);
            bool success = await webClient.PostAsync(new Group());
            TempData["message"] = success? "Participant removed.":"Failed to remove participant.";
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
            if (message.Title == null || message.Description == null)
            {
                TempData["message"] = "Please fill all fields";
                return Redirect("SendMessageForm");
            }
            List<string> ids = JsonSerializer.Deserialize<List<string>>(recieversIDs).ToList();
            if (ids.Count == 0) {
                TempData["message"] = "Please include recievers";
                return Redirect("SendMessageForm");
            }
            Message? dbMessage = await GetMessageByTitleAndDescription(message.Title, message.Description);
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
            TempData["message"] = success ? "Message sent successfully." : "Failed to post message.";
            if (!success)
            {
                return Redirect("SendMessageForm");
            }
            foreach (string id in ids) {
                success &= await AddReceiver(message, id);
                if (!success)
                {
                    DbContext.GetInstance().OpenConnection();
                    TempData["messsage"] += (firstFail? "" : ", \n") + "Failed to add " + new RepositoryUOW().GetUserRepository().GetById(id).Name;
                    DbContext.GetInstance().CloseConnection();
                    firstFail = false;
                }
            }
            return Redirect("SendMessageForm");
        }
        [HttpGet]
        private async Task<Message> GetMessageByTitleAndDescription(string title, string description)
        {
            WebClient<Message> webClient = new WebClient<Message>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetMessageByTitleAndDescription";
            webClient.AddParams("title", title);
            webClient.AddParams("description", description);
            return await webClient.GetAsync();
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
        
        [HttpGet]
        public async Task<IActionResult> AddStudentTeacherPair()
        {
            WebClient<StudentTeacherPairViewModel> webClient = new WebClient<StudentTeacherPairViewModel> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetStudentTeacherPairViewModel"
            };
            StudentTeacherPairViewModel result = await webClient.GetAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostStudentTeacherPair(string registreeID, string teacherID)
        {
            if (registreeID == "0" || teacherID == "0")
            {
                TempData["message"] = "Please fill all fields.";
            }
            WebClient<Teacher> webClient = new WebClient<Teacher> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/AddTeacherStudentPair"
            };
            webClient.AddParams("teacherID", teacherID);
            webClient.AddParams("registreeID", registreeID);
            bool success = await webClient.PostAsync(new Teacher());
            TempData["message"] = success ? "Pair added." : "Failed to register user.";
            return Redirect("AddStudentTeacherPair");
        }
    }
}

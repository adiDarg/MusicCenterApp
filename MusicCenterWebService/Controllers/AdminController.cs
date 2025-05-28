using Microsoft.AspNetCore.Mvc;
using MusicCenterWebService.Repositories;
using MusicCenterModels;
using System.Linq;

namespace MusicCenterWebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private RepositoryUOW repositoryUOW;
        public AdminController()
        {
            repositoryUOW = new RepositoryUOW();
        }
        [HttpPost]
        public bool CreateGroup(Group group)
        {
            return repositoryUOW.GetGroupRepository().Create(group);
        }

        [HttpPost]
        public bool CreateLesson(Lesson lesson)
        {
            return repositoryUOW.GetLessonRepository().Create(lesson);
        }

        [HttpPost]
        public bool CreateMeeting(Meeting meeting)
        {
            return repositoryUOW.GetMeetingRepository().Create(meeting);
        }

        [HttpPost]
        public bool AssignInstructorToGroup(string groupID, string instructorID) {
            return repositoryUOW.GetGroupRepository().UpdateInstructor(groupID, instructorID);
        }

        [HttpPost]
        public bool AddRegistreeToGroup(string groupID, string registreeID)
        {
            return repositoryUOW.GetGroupRepository().AddRegistreeToGroup(groupID, registreeID);
        }

        [HttpGet]
        public List<User> GetUsers() {
            return repositoryUOW.GetUserRepository().GetAll();
        }

        [HttpGet]
        public List<User> GetRegistrees()
        {
            List<User> res = [.. repositoryUOW.GetRegistreeRepository().GetAll()];
            return res;
        }

        [HttpGet]
        public List<Teacher> GetTeachers() { 
            List<Teacher> res = [..repositoryUOW.GetTeacherRepository().GetAll()];
            return res;
        }

        [HttpGet]
        public List<Instructor> GetInstructors()
        {
            List<Instructor> res = [.. repositoryUOW.GetInstructorRepository().GetAll()];
            return res;
        }

        [HttpGet]
        public List<Admin> GetAdmins()
        {
            List<Admin> res = [.. repositoryUOW.GetAdminRepository().GetAll()];
            return res;
        }

        [HttpGet]
        public UpdateGroupInstructorViewModel GetInstructorsAndGroups()
        {
            List<Group> groups = [..repositoryUOW.GetGroupRepository().GetAll()];
            foreach (Group group in groups)
            {
                group.Instructor = repositoryUOW.GetInstructorRepository().GetByGroupId(group.Id);
            }
            List<Instructor> instructors = [.. repositoryUOW.GetInstructorRepository().GetAll()];
            UpdateGroupInstructorViewModel res = new UpdateGroupInstructorViewModel();
            res.Groups = groups;
            res.Instructors = instructors;
            return res;
        }

        [HttpGet]
        public AddGroupRegistreeViewModel GetRegistreesAndGroups()
        {
            List<Group> groups = [.. repositoryUOW.GetGroupRepository().GetAll()];
            foreach (Group group in groups)
            {
                group.Participants = repositoryUOW.GetRegistreeRepository().GetAllByGroupId(group.Id);
            }
            List<Registree> registrees = [.. repositoryUOW.GetRegistreeRepository().GetAll()];
            AddGroupRegistreeViewModel res = new AddGroupRegistreeViewModel();
            res.Groups = groups;
            res.Registrees = registrees;
            return res;
        }

        [HttpGet] 
        public List<User> GetNonTeacherUsers()
        {
            List<User> users = GetUsers();
            HashSet<String> teacherIDs = GetTeachers().Select(t => t.Id).ToHashSet<String>();
            List<User> res = new List<User>();
            foreach (User user in users)
            {
                if (!teacherIDs.Contains(user.Id))
                {
                    res.Add(user);
                }
            }
            return res;
        }

        [HttpGet]
        public List<Teacher> GetNonInstructorTeachers()
        {
            List<Teacher> teachers = GetTeachers();
            HashSet<String> instructorIDs = GetInstructors().Select(i => i.Id).ToHashSet<String>();
            List<Teacher> res = new List<Teacher>();
            foreach (Teacher teacher in teachers)
            {
                if (!instructorIDs.Contains(teacher.Id))
                {
                    res.Add(teacher);
                }
            }
            return res;
        }

        [HttpGet]
        public List<Teacher> GetTeachersByRegistree(string registreeID)
        {
            return repositoryUOW.GetTeacherRepository().GetByRegistreeID(registreeID);
        }

        [HttpGet]
        public List<Group> GetGroups()
        {
            return repositoryUOW.GetGroupRepository().GetAll();
        }

        [HttpGet]
        public ScheduleViewModel GetSchedule()
        {
            ScheduleViewModel result = new ScheduleViewModel();
            List<Lesson> lessons = repositoryUOW.GetLessonRepository().GetAll();
            foreach (Lesson lesson in lessons)
            {
                lesson.Student = repositoryUOW.GetRegistreeRepository().GetByLessonID(lesson.Id);
                lesson.Teacher = repositoryUOW.GetTeacherRepository().GetByLessonID(lesson.Id);
            }
            result.Lessons = lessons;
            List<Group> groups = repositoryUOW.GetGroupRepository().GetAll();
            result.Meetings = new List<Meeting>();
            foreach (Group group in groups)
            {
                foreach (var meeting in repositoryUOW.GetMeetingRepository().GetByGroupID(group.Id))
                {
                    meeting.Group = group;
                    result.Meetings.Add(meeting);
                }
            }
            return result;
        }
        [HttpGet]
        public Message? GetByTitleAndDescription(string title, string description)
        {
            return repositoryUOW.GetMessageRepository().GetByTitleAndDescription(title,description);
        }

        [HttpPost]
        public bool MakeUserTeacher(string userID)
        {
            Teacher teacher = new Teacher();
            teacher.Id = userID;
            return repositoryUOW.GetTeacherRepository().Create(teacher);
        }
        
        [HttpPost]
        public bool MakeUserInstructor(string userID) { 
            Instructor instructor = new Instructor();
            instructor.Id = userID;
            return repositoryUOW.GetInstructorRepository().Create(instructor);
        }

        [HttpPost]
        public bool DemoteTeacher(string userID)
        {
            Teacher teacher = new Teacher();
            teacher.Id = userID;
            return repositoryUOW.GetTeacherRepository().Delete(teacher);
        }

        [HttpPost]
        public bool DemoteInstructor(string userID)
        {
            Instructor instructor = new Instructor();
            instructor.Id = userID;
            return repositoryUOW.GetInstructorRepository().Delete(instructor);
        }

        [HttpPost]
        public bool RemoveStudentFromGroup(string groupID, string userID)
        {
            return repositoryUOW.GetGroupRepository().RemoveUser(groupID, userID);
        }

        [HttpPost]
        public bool SendMessage(Message message)
        {
            return repositoryUOW.GetMessageRepository().Create(message);
        }
        [HttpPost]
        public bool AddReceiver(string messageID, string receiverID)
        {
            return repositoryUOW.GetMessageRepository().AddReciever(messageID, receiverID);
        }
    }
}

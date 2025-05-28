using Microsoft.AspNetCore.Mvc;
using MusicCenterModels;
using MusicCenterWebService.Repositories;

namespace MusicCenterWebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistreeController : ControllerBase
    {
        private RepositoryUOW repositoryUOW;
        public RegistreeController() { 
            repositoryUOW = new RepositoryUOW();
        }
        
        [HttpGet]
        public ScheduleViewModel GetSchedule(string registreeID)
        {
            ScheduleViewModel result = new ScheduleViewModel();
            List<Lesson> lessons = repositoryUOW.GetLessonRepository().GetByStudentID(registreeID);
            foreach (Lesson lesson in lessons)
            {
                lesson.Student = repositoryUOW.GetRegistreeRepository().GetByLessonID(lesson.Id);
                lesson.Teacher = repositoryUOW.GetTeacherRepository().GetByLessonID(lesson.Id);
            }
            result.Lessons = lessons;
            List<Group> groups = repositoryUOW.GetGroupRepository().GetByUserID(registreeID);
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
        public List<Teacher> ViewAllTeachers() {
            List<Teacher> teachers =  repositoryUOW.GetTeacherRepository().GetAll();
            foreach (var teacher in teachers)
            {
                teacher.Instruments = repositoryUOW.GetInstrumentRepository().GetByTeacherID(teacher.Id);
            }
            return teachers;
        }
    }
}

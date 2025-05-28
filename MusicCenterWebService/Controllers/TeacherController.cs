using Microsoft.AspNetCore.Mvc;
using MusicCenterModels;
using MusicCenterWebService.Repositories;

namespace MusicCenterWebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private RepositoryUOW repositoryUOW;
        public TeacherController()
        {
            repositoryUOW = new RepositoryUOW();
        }

        [HttpPost]
        public bool AddInstrument(string teacherID, string instrumentID)
        {
            return repositoryUOW.GetTeacherRepository().AddInstrument(teacherID, instrumentID);
        }

        [HttpGet]
        public List<Instrument> GetNewInstruments(string teacherID)
        {
            List<Instrument> instruments = repositoryUOW.GetInstrumentRepository().GetAll();
            HashSet<String> used = (repositoryUOW.GetInstrumentRepository()
                .GetByTeacherID(teacherID)).Select(x => x.Id).ToHashSet<String>();
            List<Instrument> result = new List<Instrument>();
            foreach (Instrument instrument in instruments) {
                if (!used.Contains(instrument.Id)) {
                    result.Add(instrument);
                }
            }
            return result;
        }

        [HttpGet]
        public ScheduleViewModel GetSchedule(string teacherID)
        {
            ScheduleViewModel result = new ScheduleViewModel();
            List<Lesson> lessons = repositoryUOW.GetLessonRepository().GetByTeacherID(teacherID);
            foreach (Lesson lesson in lessons)
            {
                lesson.Student = repositoryUOW.GetRegistreeRepository().GetByLessonID(lesson.Id);
                lesson.Teacher = repositoryUOW.GetTeacherRepository().GetByLessonID(lesson.Id);
            }
            result.Lessons = lessons;
            List<Group> groups = repositoryUOW.GetGroupRepository().GetByInstructorID(teacherID);
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
    }
}

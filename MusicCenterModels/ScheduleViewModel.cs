using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenterModels
{
    public class ScheduleViewModel
    {
        public List<Meeting>? Meetings { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}

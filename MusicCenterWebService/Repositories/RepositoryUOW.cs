namespace MusicCenterWebService.Repositories
{
    public class RepositoryUOW
    {
        private AdminRepository? _adminRepository;
        private GroupRepository? _groupRepository;
        private InstructorRepository? _instructorRepository;
        private InstrumentRepository? _instrumentRepository;
        private LessonRepository? _lessonRepository;
        private MeetingRepository? _meetingRepository;
        private MessageRepository? _messageRepository;
        private RegistreeRepository? _registreeRepository;
        private RequestRepository? _requestRepository;
        private TeacherRepository? _teacherRepository;
        private UserRepository? _userRepository;
        
        //Seperate locks so Threads initializing different repositories won't block each other
        private readonly object _adminLock = new();
        private readonly object _groupLock = new();
        private readonly object _instructorLock = new();
        private readonly object _instrumentLock = new();
        private readonly object _lessonLock = new();
        private readonly object _meetingLock = new();
        private readonly object _messageLock = new();
        private readonly object _registreeLock = new();
        private readonly object _requestLock = new();
        private readonly object _teacherLock = new();
        private readonly object _userLock = new();

        public RepositoryUOW() {}

        // Admin Repository
        public AdminRepository GetAdminRepository()
        {
            if (_adminRepository == null)
            {
                lock (_adminLock)
                {
                    if (_adminRepository == null)
                    {
                        _adminRepository = new AdminRepository();
                    }
                }
            }
            return _adminRepository;
        }

        // Group Repository
        public GroupRepository GetGroupRepository()
        {
            if (_groupRepository == null)
            {
                lock (_groupLock)
                {
                    if (_groupRepository == null)
                    {
                        _groupRepository = new GroupRepository();
                    }
                }
            }
            return _groupRepository;
        }

        // Instructor Repository
        public InstructorRepository GetInstructorRepository()
        {
            if (_instructorRepository == null)
            {
                lock (_instructorLock)
                {
                    if (_instructorRepository == null)
                    {
                        _instructorRepository = new InstructorRepository();
                    }
                }
            }
            return _instructorRepository;
        }

        // Instrument Repository
        public InstrumentRepository GetInstrumentRepository()
        {
            if (_instrumentRepository == null)
            {
                lock (_instrumentLock)
                {
                    if (_instrumentRepository == null)
                    {
                        _instrumentRepository = new InstrumentRepository();
                    }
                }
            }
            return _instrumentRepository;
        }

        // Lesson Repository
        public LessonRepository GetLessonRepository()
        {
            if (_lessonRepository == null)
            {
                lock (_lessonLock)
                {
                    if (_lessonRepository == null)
                    {
                        _lessonRepository = new LessonRepository();
                    }
                }
            }
            return _lessonRepository;
        }

        // Meeting Repository
        public MeetingRepository GetMeetingRepository()
        {
            if (_meetingRepository == null)
            {
                lock (_meetingLock)
                {
                    if (_meetingRepository == null)
                    {
                        _meetingRepository = new MeetingRepository();
                    }
                }
            }
            return _meetingRepository;
        }

        // Message Repository
        public MessageRepository GetMessageRepository()
        {
            if (_messageRepository == null)
            {
                lock (_messageLock)
                {
                    if (_messageRepository == null)
                    {
                        _messageRepository = new MessageRepository();
                    }
                }
            }
            return _messageRepository;
        }

        // Registree Repository
        public RegistreeRepository GetRegistreeRepository()
        {
            if (_registreeRepository == null)
            {
                lock (_registreeLock)
                {
                    if (_registreeRepository == null)
                    {
                        _registreeRepository = new RegistreeRepository();
                    }
                }
            }
            return _registreeRepository;
        }

        // Request Repository
        public RequestRepository GetRequestRepository()
        {
            if (_requestRepository == null)
            {
                lock (_requestLock)
                {
                    if (_requestRepository == null)
                    {
                        _requestRepository = new RequestRepository();
                    }
                }
            }
            return _requestRepository;
        }

        // Teacher Repository
        public TeacherRepository GetTeacherRepository()
        {
            if (_teacherRepository == null)
            {
                lock (_teacherLock)
                {
                    if (_teacherRepository == null)
                    {
                        _teacherRepository = new TeacherRepository();
                    }
                }
            }
            return _teacherRepository;
        }

        // User Repository
        public UserRepository GetUserRepository()
        {
            if (_userRepository == null)
            {
                lock (_userLock)
                {
                    if (_userRepository == null)
                    {
                        _userRepository = new UserRepository();
                    }
                }
            }
            return _userRepository;
        }
    }
}
namespace MusicCenterFactories;

public class ModelFactory
{
    AdminCreator? adminCreator;
    GroupCreator? groupCreator;
    InstructorCreator? instructorCreator;
    InstrumentCreator? instrumentCreator;
    LessonCreator? lessonCreator;
    MeetingCreator? meetingCreator;
    MessageCreator? messageCreator;
    RegistreeCreator? registreeCreator;
    RequestCreator? requestCreator;
    TeacherCreator? teacherCreator;
    UserCreator? userCreator;

    public AdminCreator AdminCreator
    {
        get
        {
            if (this.adminCreator == null)
            {
                this.adminCreator = new AdminCreator();
            }
            return this.adminCreator;
        }
    }
    public GroupCreator GroupCreator
    {
        get
        {
            if (this.groupCreator == null)
            {
                this.groupCreator = new GroupCreator();
            }
            return this.groupCreator;
        }
    }
    public InstructorCreator InstructorCreator
    {
        get
        {
            if (this.instructorCreator == null)
            {
                this.instructorCreator = new InstructorCreator();
            }
            return this.instructorCreator;
        }
    }
    public InstrumentCreator InstrumentCreator
    {
        get
        {
            if (this.instrumentCreator == null)
            {
                this.instrumentCreator = new InstrumentCreator();
            }
            return this.instrumentCreator;
        }
    }
    public LessonCreator LessonCreator
    {
        get
        {
            if (this.lessonCreator == null)
            {
                this.lessonCreator = new LessonCreator();
            }
            return this.lessonCreator;
        }
    }
    public MessageCreator MessageCreator
    {
        get
        {
            if (this.messageCreator == null)
            {
                this.messageCreator = new MessageCreator();
            }
            return this.messageCreator;
        }
    }
    public MeetingCreator MeetingCreator
    {
        get
        {
            if (this.meetingCreator == null)
            {
                this.meetingCreator = new MeetingCreator();
            }
            return this.meetingCreator;
        }
    }
    public RegistreeCreator RegistreeCreator
    {
        get
        {
            if (this.registreeCreator == null)
            {
                this.registreeCreator = new RegistreeCreator();
            }
            return this.registreeCreator;
        }
    }
    public RequestCreator RequestCreator
    {
        get
        {
            if (this.requestCreator == null)
            {
                this.requestCreator = new RequestCreator();
            }
            return this.requestCreator;
        }
    }
    public TeacherCreator TeacherCreator
    {
        get
        {
            if (this.teacherCreator == null)
            {
                this.teacherCreator = new TeacherCreator();
            }
            return this.teacherCreator;
        }
    }
    public UserCreator UserCreator
    {
        get
        {
            if (this.userCreator == null)
            {
                this.userCreator = new UserCreator();
            }
            return this.userCreator;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MusicCenterFactories;
using MusicCenterModels;
using MusicCenterWebService.Repositories;

namespace MusicCenterWebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private RepositoryUOW repositoryUOW;
        public UserController()
        {
            repositoryUOW = new RepositoryUOW();
        }
        
        [HttpPost]
        public bool UpdateProfile([FromBody]User user)
        {
            return repositoryUOW.GetUserRepository().Update(user);
        }

        [HttpPost]
        public bool EnterValidationKey(string userID,string key)
        {
            User user = repositoryUOW.GetUserRepository().GetById(userID);
            if (user == null)
            {
                return false;
            }
            if (key.Equals(user.ValidationKey))
            {
                Registree registree = new Registree();
                registree.Id = userID;
                return repositoryUOW.GetRegistreeRepository().Create(registree);
            }
            return false;
        }

        [HttpPost]
        public bool HandleRequest(Request request)
        {
            return repositoryUOW.GetRequestRepository().Update(request);
        }
        
        [HttpPost]
        public bool SendRequest(Request request)
        {
            return repositoryUOW.GetRequestRepository().Create(request);
        }
        
        [HttpGet] 
        public List<Request> ShowRequestsSent(string userID)
        {
            List<Request> requests = repositoryUOW.GetRequestRepository().GetSentByUserID(userID);
            foreach (var request in requests)
            {
                request.Sender = repositoryUOW.GetUserRepository().GetById(userID);
                request.Reciever = repositoryUOW.GetUserRepository().GetRecieverByRequestID(request.Id);
            }
            return requests;
        }
        
        [HttpGet]
        public List<Request> ShowRequestsRecieved(string userID)
        {
            List<Request> requests = repositoryUOW.GetRequestRepository().GetRecievedByUserID(userID);
            foreach (var request in requests)
            {
                request.Sender = repositoryUOW.GetUserRepository().GetSenderByRequestID(request.Id);
                request.Reciever = repositoryUOW.GetUserRepository().GetById(userID);
            }
            return requests;
        }
        
        [HttpGet]
        public List<Message> ShowMessages(string userID) {
            return repositoryUOW.GetMessageRepository().GetByUserID(userID);
        }
        
        [HttpGet]
        public List<McEvent> GetSchedule(User user) {
            return new List<McEvent>();
        }
    }
}

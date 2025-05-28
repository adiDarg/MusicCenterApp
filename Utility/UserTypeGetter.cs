using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class UserTypeGetter
    {
        /*Check to see if User is special type
        (Registree,Instructor, Teacher, Admin)
        If so, Save his type in the session*/
        public static string GetUserType(string userID)
        {
            DbContext db = DbContext.GetInstance();
            db.OpenConnection();
            RepositoryUOW uow = new RepositoryUOW();

            //Check if user exists in DB
            if (uow.GetUserRepository().GetById(userID) == null)
            {
                return "Guest";
            }
            //Check if user is Admin by looking in Admin db to find the id
            if (uow.GetAdminRepository().GetById(userID) != null)
            {
                return "Admin";
            }
            /*Check if user is Instructor by looking in Instructor db
            to find the id*/
            if (uow.GetInstructorRepository().GetById(userID) != null)
            {
                return "Instructor";
            }
            //Check if user is Teacher by looking in Teacher db to find the id
            if (uow.GetTeacherRepository().GetById(userID) != null)
            {
                return "Teacher";
            }
            /*Check if user is Registree by looking in Registree db
            to find the id*/
            if (uow.GetRegistreeRepository().GetById(userID) != null)
            {
                return "Registree";
            }
            db.CloseConnection();
            return "User";
        }
    }
}

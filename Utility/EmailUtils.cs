using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using MusicCenterModels;

namespace Utility
{
    public static class EmailUtils
    {
        public static bool SendValidationKeyEmail(string receiver, string key)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("music.center@gmx.com", "Adi");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Nf2PM97ve5iUT9m";
                    var sub = "Your validation key for music center!";
                    var body = @$"your validation key for music center is: {key}.
In order to enter it, login and go to Additional Actions->Enter Validation Key.";
                    var smtp = new SmtpClient
                    {
                        Host = "mail.gmx.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sending validation key failed! " + ex.ToString());
            }
            return false;
        }
        public static async Task<bool> SendValidationKeyEmail(string userID)
        {
            WebClient<User> webClient = new WebClient<User> {
                port = 5004,
                Host = "localhost",
                Path = "api/User/GetUserById"
            };
            webClient.AddParams("userID", userID);
            User user = await webClient.GetAsync();
            return SendValidationKeyEmail(user.Email, user.ValidationKey);
        }
    }
}

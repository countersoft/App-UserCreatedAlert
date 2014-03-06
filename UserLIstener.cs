using Countersoft.Gemini.Extensibility.Apps;
using Countersoft.Gemini.Extensibility.Events;
using Countersoft.Gemini.Mailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countersoft.Gemini;

namespace UserCreatedAlert
{
    [AppType(AppTypeEnum.Event),
    AppGuid("034543A3-27D2-442A-A061-1BF6B57C34EC"),
    AppName("User Created Alert"),
    AppDescription("Sends an email to the Gemini administrator when a new user is created")]
    public class UserListener : IAfterUserListener
    {
        public void AfterUserCreated(UserEventArgs args)
        {
            if (!GeminiApp.Config.EmailAlertsEnabled) return;

            if (!GeminiApp.Config.GeminiAdmins.StartsWith(args.User.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                string log;
                
                if (!EmailHelper.Send(GeminiApp.Config, string.Concat("New user created - ", args.Entity.Username), 
                    string.Format("Just to let you know that a new user has been created\nUsername: {0}\n Name: {1}\n", args.Entity.Username, args.Entity.Fullname),
                    GeminiApp.Config.GeminiAdmins, string.Empty, false, out log))
                {
                    GeminiApp.LogException(new Exception(log) { Source = "Email on user creation plugin" }, false);
                }
            }
        }

        public void AfterUserUpdated(UserEventArgs args)
        {
            
        }

        public void AfterUserDeleted(UserEventArgs args)
        {
            
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public string AppGuid { get; set; }
    }
}

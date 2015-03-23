using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace ServiceBase.Pages
{
    public partial class NewUser : System.Web.UI.Page
    {
        static string userName;
        protected void Page_Load(object sender, EventArgs e)
        {
             string domainUsername = HttpContext.Current.User.Identity.Name;
             userName = domainUsername.Split('\\')[1]; //Split username from domain name
        }

        /// <summary>
        /// Adds user's first and last name to Network Login Table
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        [WebMethod]
        public static void addUserInfo(string firstName, string lastName)
        {  
            using (var curDB = new HelpDesk_DB())
            {
                var user = curDB.NetworkLogins.FirstOrDefault(p => p.ntID == userName);
                user.firstName = firstName;
                user.lastName = lastName;

                curDB.SaveChanges();
            }
 
        }

    }//end Main
}
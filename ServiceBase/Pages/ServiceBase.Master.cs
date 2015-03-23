using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.DirectoryServices.AccountManagement;

namespace ServiceBase
{
    public partial class ServiceBase : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {

            //Check if the user is logged in
            if (Session["username"] == null)
            {
                //TODO: Check that user is using IE 9 or above
                
                string userBrowser = Request.UserAgent;
                if (userBrowser.Contains("MSIE 7.0") && !(HttpContext.Current.Request.Url.AbsolutePath.IndexOf("CompatibilityIssue.aspx", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    Response.Redirect("CompatibilityIssue.aspx");
                }

                string domainUsername = HttpContext.Current.User.Identity.Name;
                string NTID = domainUsername.Split('\\')[1]; //Split username from domain name

               //check if user is New
                if(!existingUser(NTID))
                {
                    addNewUser(NTID);
                    Response.Redirect("NewUser.aspx");
                }
                else if(!registeredUser(NTID) )
                {   
                    if ( ! (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("NewUser.aspx", StringComparison.OrdinalIgnoreCase) >= 0 ) ) 
                    {
                        Response.Redirect("NewUser.aspx");
                    }
                }
                else
                {
                    storeUserSessionInfo(NTID);
                }

            }

            if (Session["username"] == null)
            {
                UserNameLabel.Text = "New User";                
            }
            else
            {
                UserNameLabel.Text = Session["username"].ToString();
            }

        }//End page_Init


        //Store Users ID for faster logging
        protected void storeUserSessionInfo(string NTID)
        {
            using (var curDB = new HelpDesk_DB())
            {
                NetworkLogin user = curDB.NetworkLogins.SingleOrDefault(p => p.ntID == NTID);
                Session["username"] = NTID;
                Session["userID"] = user.id;
                Session["firstName"] = user.firstName;
                Session["lastName"] = user.lastName;
            }
        }

        /// <summary>
        /// Checks if NTID exists in NetworkLogin Table, if not adds the user to the table and returns false
        /// </summary>
        protected Boolean registeredUser(string NTID)
        {
            using (var curDB = new HelpDesk_DB())
            {
                NetworkLogin user = curDB.NetworkLogins.SingleOrDefault(p => p.ntID == NTID);
                if (user.firstName == null || user.lastName == null)
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Checks if NTID exists in NetworkLogin Table, if not adds the user to the table and returns false
        /// </summary>
        protected Boolean existingUser(string NTID)
        {
            using (var curDB = new HelpDesk_DB())
            {
                //If user is new add them to NetworkLogin table
                NetworkLogin user = curDB.NetworkLogins.SingleOrDefault(p => p.ntID == NTID);
                if (user == null)
                {
                    return false;
                }

            }
            return true;
        }

        protected void addNewUser(string NTID)
        {
            using (var curDB = new HelpDesk_DB())
            {
                NetworkLogin newUser = new NetworkLogin();
                newUser.ntID = NTID;
                newUser.status = "Current";

                curDB.NetworkLogins.Add(newUser);
                curDB.SaveChanges();
            }
        }
    }
}
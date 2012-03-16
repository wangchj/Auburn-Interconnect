using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AUInterconnect
{
    public class PageHelper
    {
        private Page page;

        public PageHelper(Page page)
        {
            this.page = page;
        }

        public User Login(bool checkStudent)
        {
            return PageHelper.Login(page, checkStudent);
        }

        /// <summary>
        /// Gets the current logged in user; else null.
        /// </summary>
        /// <returns>Object of logged in user; else null.</returns>
        public User GetCurrentUser()
        {
            return GetCurrentUser(page);
        }

        /// <summary>
        /// Redirct to Login page if not already logged in. If logged
        /// in, a user object is placed in Session.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="checkStudent"></param>
        /// <return>Current logged in user if the user is logged in, else
        /// the request redirected to Login page. null if error occurs.</return>
        public static User Login(Page page, bool checkStudent)
        {
            try
            {
                //Auto-login for debugging
#if DEBUG
            if (page.Session[Const.User] == null)
                page.Session[Const.User] = new User(1, true);
#endif

                //Check if user is logged in
                User user = (User)page.Session[Const.User];
                if (user == null)
                {
                    string returnUrl = HttpUtility.UrlEncode(
                        page.Request.Url.ToString());
                    string url = "~/User/Login.aspx?ReturnUrl=" + returnUrl;
                    page.Response.Redirect(url, true);
                }

                return user;
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Requires the user to be logged in and be a host. If the user is not
        /// logged in, the page is redirected to the login page. If the user is
        /// not a host, the page is redirected to home page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="checkStudent"></param>
        /// <returns></returns>
        public static User LoginAsHost(Page page, bool checkStudent)
        {
            User user = Login(page, checkStudent);
            if (!Event.UserProposedEvent(user.Uid))
            {
                Nav.GoHome(page);
            }
            return user;
        }

        /// <summary>
        /// Requires the user to be logged in as an admin.
        /// </summary>
        /// <param name="page">Page instance for redirection</param>
        /// <param name="checkStudent">Login page should check for
        /// Auburn student</param>
        /// <returns>User object if user is an admin; redirect to home page if the
        /// user is not an admin; null if error occurred.</returns>
        public static User LoginAsAdmin(Page page, bool checkStudent)
        {
            User user = Login(page, checkStudent);
            if (!user.IsAdmin)
                Nav.GoHome(page);
            return user;
        }

        /// <summary>
        /// Gets the current logged in user; else null.
        /// </summary>
        /// <param name="page">The current page</param>
        /// <returns>Object of logged in user; else null.</returns>
        public static User GetCurrentUser(Page page)
        {
            return (User)page.Session[Const.User];
        }

        /// <summary>
        /// Encodes plain-text so that it can be properly displayed as HTML. All
        /// HTML reserved characters are replace with HTML entities, and \r\n
        /// (and \n) are replaced with HTML line break tag.
        /// </summary>
        /// <param name="text">Unencoded plain-text</param>
        /// <returns>HTML encoded text</returns>
        public static string TextToHtmlEncode(string text)
        {
            return HttpUtility.HtmlEncode(text).Replace(
                "\r\n", "<br />").Replace("\n", "<br />");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AUInterconnect
{
    public class RequestUtil
    {
        /// <summary>
        /// Get Event ID from request object.
        /// </summary>
        /// <returns>-1 if event ID is not found.</returns>
        public static int GetEventId(HttpRequest request)
        {
            int eid = -1;
            int.TryParse(request[Const.EventId], out eid);
#if DEBUG
            if(eid == -1 || eid == 0)
                eid = DevConf.DebugEventId;
#endif
            return eid;
        }

        /// <summary>
        /// Returns the application path with / appended at the end.
        /// </summary>
        /// <param name="page">The current request page.</param>
        /// <returns>The application path</returns>
        public static string AppPath(Page page)
        {
            return AppPath(page.Request);
        }

        /// <summary>
        /// Returns the application path with / appended at the end.
        /// </summary>
        /// <param name="request">The current request.</param>
        /// <returns>The application path</returns>
        public static string AppPath(HttpRequest request)
        {
            if (request.ApplicationPath.EndsWith("/"))
                return request.ApplicationPath;
            return request.ApplicationPath + "/";
        }
    }
}
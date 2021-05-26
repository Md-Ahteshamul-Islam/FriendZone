using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendZone.Utility
{
    public static class SessionVar
    {
        public static string UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] as string;
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] as string;
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
    }
}
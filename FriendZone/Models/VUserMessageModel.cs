using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendZone.Models
{
    public class VUserMessageModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }
}
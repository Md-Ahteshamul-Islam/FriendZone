using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendZone.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Message1 { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public int? SentBy { get; set; }
    }
}
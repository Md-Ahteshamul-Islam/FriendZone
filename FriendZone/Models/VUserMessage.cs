//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FriendZone.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VUserMessage
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }
}
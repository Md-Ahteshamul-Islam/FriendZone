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
    
    public partial class Message
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Message1 { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public Nullable<int> SentBy { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}

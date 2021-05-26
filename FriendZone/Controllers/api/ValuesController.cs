using FriendZone.Hubs;
using FriendZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using FriendZone.Utility;

namespace KothaBondhu.Controllers.api
{
    public class ValuesController : ApiController
    {
        private FriendZoneEntities context = new FriendZoneEntities();

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SendNotification(MessageViewModel obj)
        {
            //var a = SessionVar.UserId;
            var _User = context.Users.Where(u => u.UserName == obj.UserName).FirstOrDefault();
            NotificationHub objNotifHub = new NotificationHub();
            Message objNotif = new Message();
            objNotif.UserId = _User.Id;
            objNotif.Message1 = obj.Message1;
            objNotif.SentBy = obj.SentBy;
            //objNotif.SentBy = Convert.ToInt32(Session["UserId"].ToString());
            objNotif.dateCreated = DateTime.Now;

            //context.Configuration.ProxyCreationEnabled = false;
            context.Messages.Add(objNotif);
            context.SaveChanges();

            objNotifHub.SendNotification(obj.UserName);

            var query = (from t in context.Messages
                         select t).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { query });
        }
    }
}
using Microsoft.AspNet.SignalR;
using FriendZone;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FriendZone.Models;
using FriendZone.Utility;

namespace FriendZone.Hubs
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);

        private FriendZoneEntities context = new FriendZoneEntities();
        
        //int LoggedUserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());

        public void GetNotification()
        {
            //string LoggedUserName = HttpContext.Current.Session["UserName"].ToString();
            //string LoggedUserName = "Rocky";
            string connectionId = Context.ConnectionId;
            var User = Users.Where(u => u.Value.ConnectionIds.Contains(connectionId)).FirstOrDefault();
            string LoggedUserName = User.Value.UserName.ToString();
            try
            {
                string loggedUser = LoggedUserName;

                //Get TotalNotification
                List<VUserMessageModel> totalNotif = LoadNotifObject(LoggedUserName);

                //Send To
                UserHubModels receiver;
                if (Users.TryGetValue(loggedUser, out receiver))
                {
                    var cid = receiver.ConnectionIds.FirstOrDefault();
                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    context.Clients.Client(cid).broadcaastNotif(totalNotif);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Specific User Call
        public void SendNotification(string SentTo)
        {
            try
            {
                //Get TotalNotification
                List<VUserMessageModel> totalNotif = LoadNotifObject(SentTo);

                //Send To
                UserHubModels receiver;
                if (Users.TryGetValue(SentTo, out receiver))
                {
                    var cid = receiver.ConnectionIds.FirstOrDefault();
                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    context.Clients.Client(cid).broadcaastNotif(totalNotif);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private List<VUserMessageModel> LoadNotifObject(string UserName)
        {
            var query = context.VUserMessages
                .Where(n => n.UserName == UserName)
                .Select(n => new VUserMessageModel
                {
                    SenderName = n.SenderName,
                    Message = n.Message,
                })
                .ToList();
            return query;
        }

        public override Task OnConnected()
        {
            

            string userName = Context.QueryString["userName"].ToString();
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.QueryString["userName"].ToString();
            //string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserHubModels user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        UserHubModels removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
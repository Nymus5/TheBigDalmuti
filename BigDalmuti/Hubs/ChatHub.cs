using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BigDalmuti.Hubs
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, UserData> UserList = new ConcurrentDictionary<string, UserData>();

        public override Task OnDisconnected(bool stopCalled)
        {
            UserData Value;
            UserList.TryRemove(Context.ConnectionId, out Value);

            return Clients.All.showConnected(UserList);
        }

        public void AddPlayerTurnToPage(int thisPlayerTurn)
        {
            Clients.All.addPlayerTurnToPage(thisPlayerTurn);
        }
    }
}

public class UserData
{
    public string DisplayName { get; set; }
    public string Connected { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.AspNet.SignalR.WebSockets;

namespace BigDalmuti.Controllers
{
    public class ChatController : ApiController
    {
        public HttpResponseMessage Get(string username)
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(username));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        class ChatWebSocketHandler : WebSocketHandler
        {
            private static WebSocketCollection _chatClients = new WebSocketCollection();
            private string _username;

            public ChatWebSocketHandler(string username)
            {
                _username = username;
            }

            public override void OnOpen()
            {
                _chatClients.Add(this);
            }

            public override void OnMessage(string message)
            {
                _chatClients.Broadcast(_username + ": " + message);
            }
        }
    }
}
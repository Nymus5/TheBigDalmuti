using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigDalmuti.Models;
using BigDalmuti.Extensions;
using Microsoft.AspNet.SignalR;
using BigDalmuti.Hubs;
using Newtonsoft.Json;

namespace BigDalmuti.Controllers
{
    public class GameController : Controller
    {
        public List<Player> Players { get; set; }
        
        public ActionResult Index()
        {
             return View();
        }

        [HttpPost]
        public ActionResult AddPlayerToLobby(string displayName)
        {
            Game g = (Game)this.HttpContext.Application["mainGame"];

            bool playerExist = g.Players.Any(x => x.DisplayName == displayName);

            if (playerExist == false)
            {
                var playerNumber = g.AddPlayerToGame(displayName);
                Session.SetPlayerNumberToSession(playerNumber);
                this.HttpContext.Application["mainGame"] = g;
            }
            else
            {
                if (g.IsStarted)
                {
                    return RedirectToAction("PlayGame");
                }
            }
            return View(g.Players);
        }

        [HttpGet]       
        public ActionResult AddPlayerToLobby()
        {
            Game g = (Game)this.HttpContext.Application["mainGame"];

            if (g.IsStarted)
            {
                return RedirectToAction("PlayGame");
            }
            return View(g.Players);
        }

        public ActionResult PlayGame()
        {
            Game g = (Game)this.HttpContext.Application["mainGame"];

            if (g.Start())
            {
               return View(g);
            }
            else
            {
               return RedirectToAction("AddPlayerToLobby");
            }
        }

        public ActionResult Pass()
        {
            Game g = (Game)this.HttpContext.Application["mainGame"];
                   
            int playerNumber = Session.GetPlayerNumberFromSession();
            g.NextTurn(playerNumber);

            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.addPlayerTurnToPage(g.ThisPlayerTurn);

            return RedirectToAction("PlayGame");
        }

        [HttpPost]
        public ActionResult PlayCard(string cards)
        {
            Game g = (Game)this.HttpContext.Application["mainGame"];
            List<int> cardList = JsonConvert.DeserializeObject<List<int>>(cards);

            int playerNumber = Session.GetPlayerNumberFromSession();

            g.PlayCard(playerNumber, cardList);
            

            //To do 1: Kaarten ophalen
            //To do 2: Kaarten controleren
            //To do 3: Als kaarten gespeeld mogen worden, uit hand halen
            //To do 4: Toevoegen aan lijst CardsPlayed
      
            g.NextTurn(playerNumber);

            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.addPlayerTurnToPage(g.ThisPlayerTurn);

            return RedirectToAction("PlayGame");
        }
    }
}

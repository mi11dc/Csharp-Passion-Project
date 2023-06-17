using Csharp_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Csharp_Passion_Project.Controllers
{
    public class PlayerController : Controller
    {
        string APIURL = "PlayerData/";
        private APICall api = new APICall();

        // GET: Player
        public ActionResult Index()
        {
            string url = APIURL + "ListPlayers";
            HttpResponseMessage response = api.Get(url);
            List<PlayerDto> players = new List<PlayerDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                players = response.Content.ReadAsAsync<IEnumerable<PlayerDto>>().Result.ToList();

            ViewData["title"] = "Player List";
            return View(players);
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            string url = APIURL + "FindPlayer/" + id;
            HttpResponseMessage response = api.Get(url);
            PlayerDto selectedPlayer = new PlayerDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;

            ViewData["title"] = "Player Details";
            return View(selectedPlayer);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            ViewData["title"] = "Create Player";
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            try
            {
                // TODO: Add insert logic here
                string url = APIURL + "AddPlayer";

                HttpResponseMessage response = api.Post(url, player);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(String.Concat("/"));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            string url = APIURL + "FindPlayer/" + id;
            HttpResponseMessage response = api.Get(url);
            PlayerDto selectedPlayer = new PlayerDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;

            //selectedPlayer.DOB = selectedPlayer.DOB;

            ViewData["title"] = "Edit PLayer";

            return View(selectedPlayer);
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                // TODO: Add update logic here
                string url = APIURL + "UpdatePlayer/" + id;

                HttpResponseMessage response = api.Post(url, player);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(String.Concat("Details/", id));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public ActionResult ConfirmDelete(int id)
        {
            string url = APIURL + "FindPlayer/" + id;
            HttpResponseMessage response = api.Get(url);
            PlayerDto selectedPlayer = new PlayerDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;

            ViewData["title"] = "Confirm Delete Player";

            return View(selectedPlayer);
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                string url = APIURL + "DeletePlayer/" + id;
                Object obj = new Object();

                HttpResponseMessage response = api.Post(url, obj);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("/");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

using Csharp_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Csharp_Passion_Project.Controllers
{
    public class TeamController : Controller
    {
        string APIURL = "TeamData/";
        private APICall api = new APICall();

        // GET: Team
        public ActionResult Index()
        {
            string url = APIURL + "ListTeams";
            HttpResponseMessage response = api.Get(url);
            List<TeamDto> teams = new List<TeamDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();
            
            ViewData["title"] = "Team List";

            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            string url = APIURL + "FindTeam/" + id;
            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Team Details";

            return View(selectedTeam);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            ViewData["title"] = "Create Team";
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(Team team)
        {
            try
            {
                // TODO: Add insert logic here
                string url = APIURL + "AddTeam";

                team.FormedOn = DateTime.Now;

                HttpResponseMessage response = api.Post(url, team);

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

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            string url =  APIURL + "FindTeam/" + id;
            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Edit Team";

            return View(selectedTeam);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Team team)
        {
            try
            {
                // TODO: Add update logic here
                string url = APIURL + "UpdateTeam/" + id;

                HttpResponseMessage response = api.Post(url, team);

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

        // GET: Team/ConfirmDelete/5
        public ActionResult ConfirmDelete(int id)
        {
            string url =  APIURL + "FindTeam/" + id;
            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Confirm Delete Team";

            return View(selectedTeam);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                string url = APIURL + "DeleteTeam/" + id;
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

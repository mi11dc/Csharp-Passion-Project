using Csharp_Passion_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private General general = new General();

        // GET: Team
        public ActionResult Index(string Search)
        {
            string url = APIURL + "ListTeams";
            HttpResponseMessage response = api.Get(url);
            List<TeamDto> teams = new List<TeamDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();

            if (!String.IsNullOrEmpty(Search))
                teams = teams.Where(x => 
                    general.getLowerStringForSearch(x.Name).Contains(general.getLowerStringForSearch(Search)) ||
                    general.getLowerStringForSearch(x.Owner).Contains(general.getLowerStringForSearch(Search)) ||
                    general.getLowerStringForSearch(x.FormedOn.ToShortDateString()).Contains(general.getLowerStringForSearch(Search))
                ).ToList();

            ViewData["title"] = "Team List";
            ViewData["search"] = Search;

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
        public ActionResult Create(TeamDto teamDto)
        {
            try
            {
                // TODO: Add insert logic here
                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Id = teamDto.Id,
                    FormedOn = DateTime.Now,
                    Owner = teamDto.Owner
                };

                string url = APIURL + "AddTeam";
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
        public ActionResult Edit(int id, TeamDto teamDto)
        {
            try
            {
                // TODO: Add update logic here
                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Id = id,
                    FormedOn = Convert.ToDateTime(teamDto.SFormedOn),
                    Owner = teamDto.Owner
                };

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

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
    public class TeamPlayerController : Controller
    {

        string APIURL = "TeamPlayerData/";
        private APICall api = new APICall();

        // GET: TeamPlayer
        public ActionResult Index()
        {
            return View();
        }

        // GET: TeamPlayer/ReleasePlayer/5
        public ActionResult AddTeamPlayerToTeam(int id)
        {
            TeamPlayerDto teamPlayerDto = new TeamPlayerDto();
            teamPlayerDto.TeamId = id;

            string url = APIURL + "ListTeamPlayers";

            HttpResponseMessage response = api.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
                teamPlayerDto.Players = response.Content.ReadAsAsync<IEnumerable<PlayerDto>>().Result.ToList();

            ViewData["title"] = "Team Details";
            return View(teamPlayerDto);
        }

        [HttpPost]
        public ActionResult AddTeamPlayerToTeam(TeamPlayerDto teamPlayerDto)
        {
            TeamPlayer teamPLayer = new TeamPlayer()
            {
                PlayerId = teamPlayerDto.PlayerId,
                JoinedDate = DateTime.Now,
                JoinedPrice = teamPlayerDto.JoinedPrice,
                TeamId = teamPlayerDto.TeamId
            };

            string url = APIURL + "AddTeamPlayer/";
            HttpResponseMessage response = api.Post(url, teamPLayer);

            ViewData["title"] = "Team Details";

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(String.Concat("Details/", teamPlayerDto.TeamId), "Team");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: TeamPlayer/ReleasePlayer/5
        public ActionResult ReleasePlayer(int id)
        {
            string url = APIURL + "FineTeamPlayer/" + id;
            HttpResponseMessage response = api.Get(url);

            TeamPlayerDto selectedTeamPlayer = new TeamPlayerDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeamPlayer = response.Content.ReadAsAsync<TeamPlayerDto>().Result;

            ViewData["title"] = "Team Details";

            return View(selectedTeamPlayer);
        }

        // POST: TeamPlayer/ReleasePlayer/5
        [HttpPost]
        public ActionResult ReleaseTeamPlayer(int id, int teamId)
        {
            string url = APIURL + "ReleaseTeamPlayer/" + id;
            HttpResponseMessage response = api.Get(url);

            ViewData["title"] = "Team Details";

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(String.Concat("Details/", teamId), "Team");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
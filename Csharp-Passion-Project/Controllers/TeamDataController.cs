using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Csharp_Passion_Project.Models;

namespace Csharp_Passion_Project.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TeamPlayerDataController tpController = new TeamPlayerDataController();

        // GET: api/TeamData/ListTeams
        [HttpGet]
        public IEnumerable<TeamDto> ListTeams()
        {
            List<Team> teams = db.Teams.ToList();
            List<TeamDto> teamDtos = new List<TeamDto>();

            teams.ForEach(t => teamDtos.Add(new TeamDto()
            {
                Id = t.Id,
                Name = t.Name,
                Owner = t.Owner, 
                FormedOn = (DateTime)t.FormedOn,
                SFormedOn = t.FormedOn.ToString(),
                teamPlayers = new List<TeamPlayerDetailsWithPlayerDto>()
            }));

            return teamDtos;
        }

        // GET: api/TeamData/FindTeam/5
        [ResponseType(typeof(Team))]
        [HttpGet]
        public IHttpActionResult FindTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            TeamDto teamDto = new TeamDto()
            {
                Id = team.Id,
                Name = team.Name,
                Owner = team.Owner,
                FormedOn = (DateTime)team.FormedOn,
                SFormedOn = team.FormedOn.ToString(),
                teamPlayers = tpController.ListTeamPlayersByTeamId(team.Id).ToList()
            };

            return Ok(teamDto);
        }

        // PUT: api/TeamData/UpdateTeam/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Id)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TeamData/AddTeam
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult AddTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return Ok(team);  //  CreatedAtRoute("DefaultApi", new { id = team.Id }, team);
        }

        // DELETE: api/TeamData/DeleteTeam/5
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.Id == id) > 0;
        }
    }
}
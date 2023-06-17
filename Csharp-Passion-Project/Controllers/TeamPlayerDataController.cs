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
    public class TeamPlayerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TeamPlayerData/ListTeamPlayers
        [HttpGet]
        public IEnumerable<TeamPlayerDto> ListTeamPlayers()
        {
            List<TeamPlayer> teamPlayers = db.TeamPlayers.ToList();
            List<TeamPlayerDto> teamPlayerDtos = new List<TeamPlayerDto>();

            teamPlayers.ForEach(tp => teamPlayerDtos.Add(new TeamPlayerDto()
            {
                Id = tp.Id,
                PlayerName = String.Concat(tp.Players.FName, ' ', tp.Players.LName),
                TeamName = tp.Teams.Name,
                JoinedDate = tp.JoinedDate,
                JoinedPrice = tp.JoinedPrice,
                ReleaseDate = tp.ReleaseDate
            }));

            return teamPlayerDtos;
        }

        // GET: api/TeamPlayerData/FineTeamPlayer/5
        [ResponseType(typeof(TeamPlayer))]
        [HttpGet]
        public IHttpActionResult FineTeamPlayer(int id)
        {
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            TeamPlayerDto teamPlayerDto = new TeamPlayerDto()
            {
                Id = teamPlayer.Id,
                PlayerName = String.Concat(teamPlayer.Players.FName, ' ', teamPlayer.Players.LName),
                TeamName = teamPlayer.Teams.Name,
                JoinedDate = teamPlayer.JoinedDate,
                JoinedPrice = teamPlayer.JoinedPrice,
                ReleaseDate = teamPlayer.ReleaseDate
            };

            return Ok(teamPlayerDto);
        }

        // PUT: api/TeamPlayerData/UpdateTeamPlayer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeamPlayer(int id, TeamPlayer teamPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamPlayer.Id)
            {
                return BadRequest();
            }

            db.Entry(teamPlayer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamPlayerExists(id))
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

        // POST: api/TeamPlayerData/AddTeamPlayer
        [ResponseType(typeof(TeamPlayer))]
        [HttpPost]
        public IHttpActionResult AddTeamPlayer(TeamPlayer teamPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamPlayers.Add(teamPlayer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teamPlayer.Id }, teamPlayer);
        }

        // DELETE: api/TeamPlayerData/DeleteTeamPlayer/5
        [ResponseType(typeof(TeamPlayer))]
        [HttpPost]
        public IHttpActionResult DeleteTeamPlayer(int id)
        {
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            db.TeamPlayers.Remove(teamPlayer);
            db.SaveChanges();

            return Ok(teamPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamPlayerExists(int id)
        {
            return db.TeamPlayers.Count(e => e.Id == id) > 0;
        }
    }
}
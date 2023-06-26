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
        public IHttpActionResult ListTeamPlayers()
        {
            List<Player> players = db.Players.ToList();
            List<TeamPlayer> teamPlayers = db.TeamPlayers.ToList();
            List<PlayerDto> playerDtos = new List<PlayerDto>();

            players.ForEach(p =>
            {
                if (teamPlayers.Count(x => (x.PlayerId == p.Id) && (x.ReleaseDate == null)) == 0)
                    playerDtos.Add(new PlayerDto()
                    {
                        Id = p.Id,
                        FName = p.FName,
                        LName = p.LName
                    });
            });

            return Ok(playerDtos);
        }

        // GET: api/TeamPlayerData/ListTeamPlayersByTeamId
        [HttpGet]
        public IEnumerable<TeamPlayerDetailsWithPlayerDto> ListTeamPlayersByTeamId(int id) 
        {
            List<TeamPlayer> teamPlayers = db.TeamPlayers.ToList();
            List<TeamPlayerDetailsWithPlayerDto> playerDetails = new List<TeamPlayerDetailsWithPlayerDto>();

            teamPlayers = teamPlayers.Where(x => 
                (x.TeamId == id) 
                && (x.ReleaseDate == null)
            ).ToList();

            if (teamPlayers != null && teamPlayers.Count > 0)
                teamPlayers.ForEach(tp => playerDetails.Add(new TeamPlayerDetailsWithPlayerDto()
                {
                    Id = tp.Id,
                    PlayerId = tp.PlayerId,
                    PlayerName = String.Concat(tp.Players.FName, ' ', tp.Players.LName),
                    TeamId = tp.TeamId,
                    TeamName = tp.Teams.Name,
                    JoinedDate = tp.JoinedDate,
                    JoinedPrice = tp.JoinedPrice,
                    ReleaseDate = (tp.ReleaseDate != null) ? (DateTime)tp.ReleaseDate : DateTime.MinValue,
                    BasePrice = tp.Players.BasePrice,
                    Country = tp.Players.Country,
                    DOB = (DateTime)tp.Players.DOB,
                    SDOB = tp.Players.DOB.ToString()                    
                }));

            return playerDetails;
        }

        // GET: api/TeamPlayerData/ReleaseTeamPlayer/5
        [HttpGet]
        public IHttpActionResult ReleaseTeamPlayer(int id)
        {
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamPlayer.Id)
            {
                return BadRequest();
            }

            teamPlayer.ReleaseDate = DateTime.Now;

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
                PlayerId = teamPlayer.PlayerId,
                PlayerName = String.Concat(teamPlayer.Players.FName, ' ', teamPlayer.Players.LName),
                TeamId = teamPlayer.TeamId,
                TeamName = teamPlayer.Teams.Name,
                JoinedDate = teamPlayer.JoinedDate,
                JoinedPrice = teamPlayer.JoinedPrice,
                ReleaseDate = (teamPlayer.ReleaseDate != null) ? (DateTime)teamPlayer.ReleaseDate : DateTime.MinValue
            };

            return Ok(teamPlayerDto);
        }

        //// PUT: api/TeamPlayerData/UpdateTeamPlayer/5
        //[ResponseType(typeof(void))]
        //[HttpPost]
        //public IHttpActionResult UpdateTeamPlayer(int id, TeamPlayer teamPlayer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != teamPlayer.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(teamPlayer).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TeamPlayerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

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

            return Ok(teamPlayer);
        }

        //// DELETE: api/TeamPlayerData/DeleteTeamPlayer/5
        //[ResponseType(typeof(TeamPlayer))]
        //[HttpPost]
        //public IHttpActionResult DeleteTeamPlayer(int id)
        //{
        //    TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
        //    if (teamPlayer == null)
        //    {
        //        return NotFound();
        //    }

        //    db.TeamPlayers.Remove(teamPlayer);
        //    db.SaveChanges();

        //    return Ok(teamPlayer);
        //}

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
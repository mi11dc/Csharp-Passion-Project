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
    public class PlayerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PlayerData/ListPlayers
        [HttpGet]
        public IEnumerable<PlayerDto> ListPlayers()
        {
            List<Player> players = db.Players.ToList();
            List<PlayerDto> playerDtos = new List<PlayerDto>();

            players.ForEach(p => playerDtos.Add(new PlayerDto()
            {
                Id = p.Id,
                FName = p.FName, 
                LName = p.LName,
                BasePrice = p.BasePrice,
                Country = p.Country,
                DOB = (DateTime)p.DOB
            }));

            return playerDtos;
        }

        // GET: api/PlayerData/FindPlayer(/5
        [ResponseType(typeof(Player))]
        [HttpGet]
        public IHttpActionResult FindPlayer(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            PlayerDto playerDto = new PlayerDto()
            {
                Id = player.Id,
                FName = player.FName, 
                LName = player.LName,
                BasePrice = player.BasePrice,
                Country = player.Country,
                DOB = (DateTime)player.DOB
            };

            return Ok(playerDto);
        }

        // PUT: api/PlayerData/UpdatePlayer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlayer(int id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.Id)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/PlayerData/AddPlayer
        [ResponseType(typeof(Player))]
        [HttpPost]
        public IHttpActionResult AddPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = player.Id }, player);
        }

        // DELETE: api/PlayerData/DeletePlayer/5
        [ResponseType(typeof(Player))]
        [HttpPost]
        public IHttpActionResult DeletePlayer(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);
            db.SaveChanges();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}
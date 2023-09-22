using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Data;

namespace SuperHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroesController : ControllerBase
    {

        private static List<SuperHero> heros = new List<SuperHero>
            {
                new SuperHero{ Id = 1,Name="Super Man",FirstName="IndraSena",LastName="Revuri",Place="Hyderabad"}
                ,new SuperHero{ Id = 2,Name="Spider man",FirstName="Peter",LastName="Parker",Place="Newyork"}
                ,new SuperHero{ Id = 3,Name="Iron man",FirstName="Tony",LastName="Startk",Place="Washington"}

            };
        private readonly DataContext _context;
        public SuperHeroesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);

            await _context.SaveChangesAsync();
                      
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {

            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound("Hero not fond for id" + id.ToString());
            }

            return Ok(hero);

        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateSuperHero(int id, string place)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero != null) { hero.Place = place; await _context.SaveChangesAsync(); return Ok(hero); }
            else
            { return NotFound("Hero not found to update"); }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero != null)
            {
                _context.SuperHeroes.Remove(hero);
                await _context.SaveChangesAsync();
                return Ok(await _context.SuperHeroes.ToListAsync());

            }
            else { return NotFound("Hero not foud to delete"); }


        }
    

    }
}

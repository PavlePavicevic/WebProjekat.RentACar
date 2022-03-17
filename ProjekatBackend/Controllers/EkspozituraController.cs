using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EkspozituraController : ControllerBase
    {
        public RentACarContext Context {get; set; }

        public EkspozituraController(RentACarContext context)
        {
            Context = context;
        }

        [Route("PrikaziEkspoziture")]
        [HttpGet]
        public async Task<ActionResult> PrikaziEkspoziture()
        {
           var ekspoziture = await Context.Ekspoziture.ToListAsync();
           return Ok(ekspoziture.Select(p=> new
            {
               Ime=p.Ime,
               Grad=p.Grad,
            }).ToList()
           );
        }

        /*[Route("DodajEkspozituru")]
        [HttpPost]
        public async Task<ActionResult> DodajEkspozituru([FromBody] Ekspozitura ekspozitura){
             if(ekspozitura.Ime.Length>50)
                return BadRequest("Nevalidno ime ekspoziture!");
            if(ekspozitura.Grad.Length>50)
                return BadRequest("Nevalidno ime grada!");
            
            try
            {
                Context.Ekspoziture.Add(ekspozitura);
                await Context.SaveChangesAsync();

                return Ok("Ekspozitura uspesno dodata!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        

        

        /*[Route("UkloniEkspozituru/{ime}/{grad}")]
        [HttpDelete]
        public async Task<ActionResult> UkloniEkspozituru(string ime, string grad)
        {
            if(String.IsNullOrEmpty(ime) || ime.Length>50)
                return BadRequest("Nevalidno ime ekspoziture!");
            if(String.IsNullOrEmpty(grad) || grad.Length >50)
                return BadRequest("Nevalidno ime grada!");
            
            try
            {
                var ekspozitura = await Context.Ekspoziture.Where(p=> p.Ime == ime && p.Grad== grad).FirstOrDefaultAsync();
                Context.Ekspoziture.Remove(ekspozitura);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno je uklonjena ekspozitura sa imenom {ime} u gradu {grad}!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

    }
}
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
    public class KlijentController : ControllerBase
    {
        public RentACarContext Context {get; set; }

        public KlijentController(RentACarContext context)
        {
            Context = context;
        }

        [Route("DodajKlijenta/{ime}/{prezime}/{brojLicneKarte}")]
        [HttpPost]
        public async Task<ActionResult> DodajKlijenta(string ime, string prezime, string brojLicneKarte)
        {
            if(String.IsNullOrEmpty(ime)||ime.Length>50)
                return BadRequest("Nevalidno ime klijenta!");
            if(String.IsNullOrEmpty(prezime)||prezime.Length>50)
                return BadRequest("Nevalidno prezime klijenta!");
            if(brojLicneKarte.Length!=13)
                return BadRequest("Nevalidan broj licne karte!");
            var klijent = new Klijent
            {
                Ime=ime,
                Prezime=prezime,
                BrojLicneKarte=brojLicneKarte,
            };
            try
            {
                Context.Klijenti.Add(klijent);
                await Context.SaveChangesAsync();

                return Ok($"Uspesno je dodat klijent!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PrikaziKlijente")]
        [HttpGet]
        public async Task<ActionResult> PrikaziKlijente()
        {
            var klijenti = await Context.Klijenti.ToListAsync();
            try
            {
                return Ok(klijenti.Select(p=> new
                {
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    BrojLicneKarte=p.BrojLicneKarte
                }).ToList()
                );
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("UkloniKlijenta/{brojLicneKarte}")]
        [HttpDelete]
        public async Task<ActionResult> UkloniKlijenta(string brojLicneKarte)
        {
            if(String.IsNullOrEmpty(brojLicneKarte) || brojLicneKarte.Length!=13)
                return BadRequest("Nevalidan broj licne karte klijenta");
            
            try
            {
                var klijent = await Context.Klijenti.Where(p=> p.BrojLicneKarte==brojLicneKarte).FirstOrDefaultAsync();
                Context.Remove(klijent);
                await Context.SaveChangesAsync();
                
                return Ok($"Klijent sa licnom kartom broj {brojLicneKarte} je uspesno uklonjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        } 

        /*[Route("PrikaziKlijenta/{brojLicneKarte}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziKlijenta(string brojLicneKarte)
        {
            
            if(brojLicneKarte.Length!=13)
                return BadRequest("Nevalidan broj licne karte!");
            try
            {
                var klijent = await Context.Klijenti.Where(p=>p.BrojLicneKarte==brojLicneKarte).FirstOrDefaultAsync();
                var klijentVeza=Context.Klijenti.Include(p=> p.Iznajmljivanje);
                return Ok( new
                {
                    Ime=klijent.Ime,
                    Prezime=klijent.Prezime,
                    brojLicneKarte=klijent.BrojLicneKarte,
                    Iznajmljivanje=klijent.Iznajmljivanje.Select(p=> new
                    {
                        DatumIznajmljivanja=p.DatumIznajmljivanja,
                        BrojDana=p.BrojDana,
                    }).ToList()
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/


        /*[Route("IzmeniKlijenta")]
        [HttpPut]
        public async Task<ActionResult> IzmeniKlijenta([FromBody] Klijent klijent)
        {
            if(String.IsNullOrEmpty(klijent.Ime)||klijent.Ime.Length>50)
                return BadRequest("Nevalidno ime klijenta!");
            if(String.IsNullOrEmpty(klijent.Prezime)||klijent.Prezime.Length>50)
                return BadRequest("Nevalidno prezime klijenta!");
            if(klijent.BrojLicneKarte.Length!=13)
                return BadRequest("Nevalidan broj licne karte!");
            
            try
            {
                if (klijent != null)
                {
                    Context.Klijenti.Update(klijent);
                    await Context.SaveChangesAsync();
                }
                return Ok($"Klijent je uspesno dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        
    }
}

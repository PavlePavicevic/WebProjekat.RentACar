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
    public class AutoController : ControllerBase
    {
        public RentACarContext Context{get; set;}

        public AutoController(RentACarContext context)
        {
            Context=context;
        }


        [Route("DodajAuto/{registarskiBroj}/{markaAuta}/{godinaProizvodnje}/{kilometraza}/{cenaIznajmljivanja}/{lokacija}")]
        [HttpPost]
        public async Task<ActionResult> DodajAuto(string registarskiBroj, string markaAuta, int godinaProizvodnje, int kilometraza, int cenaIznajmljivanja, string lokacija )
        {
            if(godinaProizvodnje<2000 || godinaProizvodnje>2022)
                return BadRequest("Nevalidna godina proizvodnje!");
            
            if(kilometraza<0||kilometraza>250000)
                return BadRequest("Nevalidna kilometraza!");

            if (markaAuta.Length>50)
                return BadRequest("Nevalidna marka automobila!");
            try
            {
                
                var auto=new Auto
                {
                    MarkaAuta=markaAuta,
                    RegistarskiBroj=registarskiBroj,
                    GodinaProizvodnje=godinaProizvodnje,
                    Kilometraza=kilometraza,
                    CenaIznajmljivanja=cenaIznajmljivanja,
                    Lokacija=lokacija,
                    AutomobilDostupan=true,
                };
                
                Context.Automobili.Add(auto);
                await Context.SaveChangesAsync();
                
                return Ok("Auto odgovara specifikacijama!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("PrikaziAutomobile/{grad}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziAutomobile(string grad)
        {
            if(String.IsNullOrWhiteSpace(grad))
                return BadRequest("Nevalidan unos!");
            
            var automobili = await Context.Automobili.ToListAsync();

            return Ok(automobili
            .Where(p =>p.Lokacija==grad)
            .Select(p=> new
            {
                Marka=p.MarkaAuta,
                GodinaProizvodnje=p.GodinaProizvodnje,
                Kilometraza=p.Kilometraza,
                RegistracioniBroj= p.RegistarskiBroj,
                AutomobilJeDostupan=p.AutomobilDostupan,
                Lokacija=p.Lokacija,
                Cena=p.CenaIznajmljivanja
            }
            ).ToList()
            );
        }

        [Route("PrikaziAutomobil/{registracioniBroj}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziAutomobil(string registracioniBroj)
        {
            if(String.IsNullOrWhiteSpace(registracioniBroj))
                return BadRequest("Nevalidan unos!");

            var automobil= await Context.Automobili.Where(p=> p.RegistarskiBroj==registracioniBroj).FirstOrDefaultAsync();
            return Ok( new
            {
                Marka=automobil.MarkaAuta,
                GodinaProizvodnje=automobil.GodinaProizvodnje,
                Kilometraza=automobil.Kilometraza,
                RegistracioniBroj= automobil.RegistarskiBroj,
                AutomobilJeDostupan=automobil.AutomobilDostupan,
                Lokacija=automobil.Lokacija
            }
            
            );
        }

        [Route("PromeniPodatkeOAutu/{registracioniBroj}/{novaKilometraza}")]
        [HttpPut]
        public async Task<ActionResult> PromeniPodatkeOAutu(string registracioniBroj, int novaKilometraza)
        {
            if (novaKilometraza<0 || novaKilometraza >250000)
                return BadRequest("Nevalidna kilometraza!");
            
            
            if(String.IsNullOrWhiteSpace(registracioniBroj))
                return BadRequest("Nevalidan Registracioni Broj");
            
            
            try
            {
                var automobil = Context.Automobili.Where(p=>p.RegistarskiBroj == registracioniBroj).FirstOrDefault();

                if(automobil!=null)
                {
                    automobil.Kilometraza=novaKilometraza;
                    
                    
                    await Context.SaveChangesAsync();
                    return Ok($"Automobil sa registracijom {registracioniBroj} je uspesno izmenjen!");
                }

                else 
                    return BadRequest("Nevalidno uneseni podaci!");
            }


            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*[Route("UkloniAutomobil/{registarskiBroj}")]
        [HttpDelete]
        public async Task<ActionResult> UkloniAutomobil(string registarskiBroj)
        {
            try{
            var auto = await Context.Automobili.Where(p=> p.RegistarskiBroj==registarskiBroj).FirstOrDefaultAsync();
            Context.Remove(auto);
            await Context.SaveChangesAsync();

            return Ok($"Auto sa registarskom oznakom {registarskiBroj} je uspesno uklonjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/
        /*[Route("DodajAuto")]
        [HttpPost]
        public async Task<ActionResult> DodajAuto([FromBody] Auto auto)
        {
            if(auto.GodinaProizvodnje<2000 || auto.GodinaProizvodnje>2022)
                return BadRequest("Nevalidna godina proizvodnje!");
            
            if(auto.Kilometraza<0||auto.Kilometraza>250000)
                return BadRequest("Nevalidna kilometraza!");

            if (auto.MarkaAuta.Length>50)
                return BadRequest("Nevalidna marka automobila!");
            try
            {
                Context.Automobili.Add(auto);
                await Context.SaveChangesAsync();

                return Ok("Auto odgovara specifikacijama!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/
    }
}
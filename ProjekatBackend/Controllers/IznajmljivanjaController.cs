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
    public class IznajmljivanjaController : ControllerBase
    {
        public RentACarContext Context {get; set; }

        public IznajmljivanjaController(RentACarContext context)
        {
            Context = context;
        }

        [Route("DodajIznajmljivanje/{imeEkspoziture}/{registarskiBroj}/{klijentIme}/{klijentPrezime}/{brojDana}")]
        [HttpPost]
        public async Task<ActionResult> DodajIznajmljivanje(string imeEkspoziture, string registarskiBroj,string klijentIme,string klijentPrezime, int brojDana)
        {
            try
            {
                var ekspozitura = Context.Ekspoziture.Where(p=> p.Ime==imeEkspoziture).FirstOrDefault();
                var automobil= Context.Automobili.Where(p=> p.RegistarskiBroj==registarskiBroj).FirstOrDefault();
                var klijent=Context.Klijenti.Where(p=>p.Ime==klijentIme && p.Prezime==klijentPrezime).FirstOrDefault();

                if(ekspozitura==null||automobil==null||klijent==null)
                    return BadRequest("Nevalidan unos!");

                var iznajmljivanje = new Iznajmljivanja
                {
                    DatumIznajmljivanja=DateTime.Now,
                    BrojDana=brojDana,
                    Auto=automobil,
                    Klijent=klijent,
                    Ekspozitura=ekspozitura
                };

                Context.AutoEkspozitureKlijenti.Add(iznajmljivanje);
                await Context.SaveChangesAsync();

                return Ok(iznajmljivanje);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiIznajmljivanje/{imeEkspoziture}/{imeKlijent}/{prezimeKlijenta}/{registarskiBroj}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiIznajmljivanje(string imeEkspoziture, string imeKlijent, string prezimeKlijenta,string registarskiBroj)
        {
            try
            {
            var iznajmljivanje = Context.AutoEkspozitureKlijenti.Where(p=> p.Ekspozitura.Ime==imeEkspoziture 
            && p.Klijent.Ime == imeKlijent 
            && p.Klijent.Prezime==prezimeKlijenta
            && p.Auto.RegistarskiBroj==registarskiBroj).FirstOrDefault();
            Context.AutoEkspozitureKlijenti.Remove(iznajmljivanje);

            await Context.SaveChangesAsync();
            return Ok("Uspesno je uklonjeno iznajmljivanje!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PrikaziIznajmljivanja/{imeEkspoziture}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziIznajmljivanja(string imeEkspoziture)
        {
            if(String.IsNullOrWhiteSpace(imeEkspoziture))
                return BadRequest("Nevalidan unos!");
            
            var automobili = await Context.Automobili.ToListAsync();
            var ekspoziture = await Context.Ekspoziture.ToListAsync();
            var klijenti = await Context.Klijenti.ToListAsync();
            
            try
            {
                var podaciOIznajmljivanju = await Context.AutoEkspozitureKlijenti
                .Include(p=>p.Auto)
                .Include(p=> p.Ekspozitura)
                .Include(p=>p.Klijent)
                .Where(p =>p.Ekspozitura.Ime==imeEkspoziture)
                .Select(
                    p=>new 
                    {
                        Ekspozitura=p.Ekspozitura.Ime,
                        MarkaAuta=p.Auto.MarkaAuta,
                        RegistarskiBroj=p.Auto.RegistarskiBroj,
                        KlijentIme=p.Klijent.Ime,
                        KlijentPrezime=p.Klijent.Prezime,
                        DatumIznajmljivanja=p.DatumIznajmljivanja.ToShortDateString(),
                        BrojDana=p.BrojDana
                        
                    }
                ).ToListAsync();

                return Ok(podaciOIznajmljivanju);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
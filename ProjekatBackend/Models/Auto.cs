using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Auto
    {
        [Key]
        public int ID { get; set; }

        
        public string RegistarskiBroj {get; set;}

        public bool AutomobilDostupan {get; set; }

        [MaxLength(50)]
        public string MarkaAuta { get; set; }

        [Range(2000,2022)]
        public int GodinaProizvodnje { get; set; }

        [Range(0,250000)]
        public int Kilometraza { get; set; }

        public string Lokacija {get; set; }
        public int CenaIznajmljivanja { get; set; }

        [JsonIgnore]
        public virtual List<Iznajmljivanja> Iznajmljivanje { get; set; }


    }
}
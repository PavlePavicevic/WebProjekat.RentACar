using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Klijent
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime{get; set;}

        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        public string BrojLicneKarte { get; set; } 

        [JsonIgnore]
        public virtual List<Iznajmljivanja> Iznajmljivanje { get; set; }
        

    }
}
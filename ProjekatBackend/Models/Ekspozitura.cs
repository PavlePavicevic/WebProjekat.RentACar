using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Ekspozitura
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Ime { get; set; } 

        [MaxLength(50)]
        public string Grad { get; set; }

        [JsonIgnore]
        public virtual List<Iznajmljivanja> Iznajmljivanje { get; set; }

        


    }
}
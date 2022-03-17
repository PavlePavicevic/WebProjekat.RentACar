using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Iznajmljivanja
    {   
        [Key]
        public int ID { get; set; }

        public DateTime DatumIznajmljivanja { get; set; }

        public int BrojDana { get; set; }


        public virtual Auto Auto {get; set; }

        public virtual Klijent Klijent {get; set; }


        public virtual Ekspozitura Ekspozitura {get; set; }


    }
}
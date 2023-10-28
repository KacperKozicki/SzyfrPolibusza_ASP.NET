using System.ComponentModel.DataAnnotations;
using SzyfrPolibusza.Controllers;

namespace SzyfrPolibusza.Models
{
    public class Polibusz
    {
        [Required]
        public string? Tekst { get; set; }
        public string? ZaszyfrowanyTekst { get; set; }
        

        public string[][] Macierz { get; set; } =



              new string[5][]
              {
                  new string[7] { "A", "Ą", "B", "C", "Ć", "D", "E" },
                  new string[7] { "Ę", "F", "G", "H", "I", "J", "K" },
                  new string[7] { "L", "Ł", "M", "N", "Ń", "O", "Ó" },
                  new string[7] { "P", "Q", "R", "S", "Ś", "T", "U" },
                  new string[7] { "V", "W", "X", "Y", "Z", "Ź", "Ż" },
              };


    }
}


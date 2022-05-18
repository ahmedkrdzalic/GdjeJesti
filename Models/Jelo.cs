using System.ComponentModel.DataAnnotations;

namespace GdjeJesti.Models
{
    public class Jelo
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public int RestoranId { get; set; }
    }
}
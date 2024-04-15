using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Leagues.Models
{
    public class League
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
    }
}

namespace PokemonTcgOrderSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int ProductId { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineTicketSales.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public string DepartureLocation { get; set; }

        [Required]
        public string ArrivalLocation { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        public int SoldSeats { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}

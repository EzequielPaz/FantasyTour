using System.ComponentModel.DataAnnotations;
using static FT.Utils.Enums;

namespace FT.Domain.Entities
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }  
        public string? Description { get; set; }
        public DurationDays Duration { get; set; } = DurationDays.FullDay;
        public DateTime Created { get; set; } = DateTime.Now;
        public State State { get; set; } = State.Active;

    }
}

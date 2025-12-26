using System.ComponentModel.DataAnnotations.Schema;
using static FT.Utils.Enums;

namespace FT.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [NotMapped]
        public string Password { get; set; }

        public Profile Profile { get; set; } = Profile.User;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public State State { get; set; } = State.Active;
    }
}

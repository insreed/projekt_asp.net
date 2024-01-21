using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? UserId { get; set; }

        public IdentityUser? User { get; set; }
    }
}

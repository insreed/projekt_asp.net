using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Series { get; set; }

        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }

        public int SessionId { get; set; }
        public virtual Session? Session { get; set; }

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

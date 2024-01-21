using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{

    public class Goals
    {
        public int Id { get; set; }
        public int Mass { get; set; }
        public int MassGoal { get; set; }
        public int WeightGoal { get; set; }
        public int GoalType { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string GetGoalTypeName()
        {
            return GoalType == 1 ? "Podnieść Ciężar" : "Utrata wagi";
        }
    }
}

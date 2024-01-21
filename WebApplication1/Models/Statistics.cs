using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        [DisplayName("Największa wzięta waga:")]
        public int BestStat { get; set; }

        [DisplayName("Z dnia:")]
        public DateTime StartDate { get; set; }

        [DisplayName("Ilość sesji ćwiczenia:")]
        public int SessionCount { get; set; }

        [DisplayName("Nazwa ćwiczenia:")]
        public string ExcerciseType { get; set; }

        

    }
}

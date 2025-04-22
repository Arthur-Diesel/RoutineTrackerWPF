using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RoutineTracker.Classes
{
    internal class Routine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Breakfest { get; set; }
        public bool Lunch { get; set; }
        public bool EveningLunch { get; set; }
        public bool Dinner { get; set; }
        public bool OutOfDiet { get; set; }
        public bool Exercise { get; set; }
        public bool Alcoohol { get; set; }
        public bool Lecture { get; set; }
        public bool Study { get; set; }
        public bool Organization { get; set; }
        public DateTime Date { get; set; }
    }
}

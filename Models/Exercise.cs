using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ExerciseTypeId")]
        public int ExerciseTypeId { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Sets { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
    }
}

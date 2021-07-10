using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Models
{
    public class ExerciseType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Exercise Type")]
        [Required]
        public string ExerciseTypeName { get; set; }
    }
}

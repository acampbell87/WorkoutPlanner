using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Models.ViewModels
{
    public class ExerciseVM
    {
        public Exercise Exercise { get; set; }
        public IEnumerable<SelectListItem> ExerciseList { get; set; }
    }
}

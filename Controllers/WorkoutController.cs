using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.ViewModels;
using PagedList;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlanner.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WorkoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Index
        public async Task<IActionResult> Index(
            string sortOrder,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            //Creates Index Object from DB and joins exercise type
            //IEnumerable<Exercise> objList = _db.Exercises;
            var objList = _db.Exercises
                .Include(e => e.ExerciseType)
                .AsNoTracking();
            //Reorders the objList depending on the current sortOrder
            switch (sortOrder)
            {
                case "date_desc":
                    objList = objList.OrderByDescending(s => s.Date);
                    break;
                default:
                    objList = objList.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Exercise>.CreateAsync(objList.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        //GET-delete
        public IActionResult Delete(int? id)
        {
            ExerciseVM exerciseVM = new ExerciseVM()
            {
                Exercise = new Exercise(),
                ExerciseList = _db.ExerciseTypes.Select(i => new SelectListItem
                {
                    Text = i.ExerciseTypeName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            exerciseVM.Exercise = _db.Exercises.Find(id);
            if (exerciseVM.Exercise == null)
            {
                return NotFound();
            }
            return View(exerciseVM);
        }
        //POST-delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ExerciseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Exercises.Remove(obj.Exercise);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET Create
        public IActionResult Create()
        {

            ExerciseVM exerciseVM = new ExerciseVM()
            {
                Exercise = new Exercise(),
                ExerciseList = _db.ExerciseTypes.Select(i => new SelectListItem
                {
                    Text = i.ExerciseTypeName,
                    Value = i.Id.ToString()
                })
            };

            return View(exerciseVM);
        }

        //POST-create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExerciseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Exercises.Add(obj.Exercise);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-Update
        public IActionResult Update(int? id)
        {
            ExerciseVM exerciseVM = new ExerciseVM()
            {
                Exercise = new Exercise(),
                ExerciseList = _db.ExerciseTypes.Select(i => new SelectListItem
                {
                    Text = i.ExerciseTypeName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            exerciseVM.Exercise = _db.Exercises.Find(id);
            if (exerciseVM.Exercise == null)
            {
                return NotFound();
            }

            return View(exerciseVM);
        }

        //POST-update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExerciseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Exercises.Update(obj.Exercise);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}

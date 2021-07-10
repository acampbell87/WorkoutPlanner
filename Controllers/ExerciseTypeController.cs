using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.ViewModels;


namespace WorkoutPlanner.Controllers
{
    public class ExerciseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExerciseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Index
        public async Task<IActionResult> Index(string searchString)
        {
            var exercises = from e in _db.ExerciseTypes
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(i => i.ExerciseTypeName.Contains(searchString));
            }

            return View(await exercises.ToListAsync());
        }

        //GET Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExerciseType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ExerciseTypes.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExerciseTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ExerciseTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.ExerciseTypes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExerciseTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExerciseType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ExerciseTypes.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}

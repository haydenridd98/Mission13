using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    { 
        private IBowlingRepository _repo { get; set; }

        public HomeController(IBowlingRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int teamId)
        {
            var bowlers = _repo.Bowlers.ToList();

            if (teamId == 0)
            {
                ViewBag.Team = 0;
            }
            else
            {
                ViewBag.Team = teamId;
                bowlers = _repo.Bowlers.Where(x => x.TeamID == teamId).ToList();
            }
            return View(bowlers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bowler b)
        {
            _repo.CreateBowler(b);
            return RedirectToAction("Index", 0);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);
            return View(bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            _repo.SaveBowler(b);
            return RedirectToAction("Index", 0);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);
            return View(bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            Bowler bowler = _repo.Bowlers.Single(x => x.BowlerID == b.BowlerID);
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index", 0);
        }

    }
}

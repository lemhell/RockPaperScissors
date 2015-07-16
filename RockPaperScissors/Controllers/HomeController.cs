using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockPaperScissors.Models;

namespace RockPaperScissors.Controllers
{
    public class HomeController : Controller
    {
        GameContext db = new GameContext();
        RPSModel rps;
        Random rand = new Random();

        public HomeController()
        {
            rps = new RPSModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        private void setViewBag(RPSModel.Options playerOption, RPSModel.Options compOption, RPSModel.Result result)
        {
            ViewBag.SelectedOption = playerOption;
            ViewBag.compOption = compOption;
            ViewBag.result = result;
            ViewBag.GameID = RPSModel.GameID;
            ViewBag.GameNumber = RPSModel.GameNumber;
            ViewBag.CurrentWins = RPSModel.CurrentWins;
            ViewBag.AllWins = RPSModel.Wins;
            TempData["SelectedOption"] = ViewBag.SelectedOption;
            TempData["compOption"] = ViewBag.compOption;
            TempData["result"] = ViewBag.result;
            TempData["GameID"] = ViewBag.GameID;
            TempData["GameNumber"] = ViewBag.GameNumber;
            TempData["CurrentWins"] = ViewBag.CurrentWins;
            TempData["AllWins"] = ViewBag.AllWins;
        }

        private void process(RPSModel.Options playerOption)
        {
            RPSModel.Options compOption = RPSModel.getRandomOption(rand);
            RPSModel.Result result = RPSModel.getOutcome(playerOption, compOption);
            RPSModel.CurrentWins += result.toInt();
            db.Games.Add(new Game { GameID = RPSModel.GameID++, Result = result.ToString(), 
                Type = playerOption.ToString(), EnemyType = compOption.ToString(),  Number = RPSModel.GameNumber, 
                AllWins = RPSModel.Wins, Wins = RPSModel.CurrentWins});
            db.SaveChanges();
            setViewBag(playerOption, compOption, result);
        }

        public ActionResult Rock()
        {
            process(RPSModel.Options.ROCK);
            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("getStatistics");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Paper()
        {
            process(RPSModel.Options.PAPER);
            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("getStatistics");
            }
            else
            {
                return View("Index");
            }
        }
        
        public ActionResult Scissors()
        {
            process(RPSModel.Options.SCISSORS);
            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("getStatistics");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult EndCurrentGame()
        {
            RPSModel.CurrentWins = 0;
            RPSModel.GameNumber++;
            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("getStatistics");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Search(string number)
        {
            ViewBag.test = number;
            if (number == "" || Int32.Parse(number) > RPSModel.GameNumber) return View("Index");
            int num = Int32.Parse(number);
            ViewBag.gameNumberForHistory = num;
            var selection = (from p in db.Games where p.Number == num select p).ToList();
            ViewBag.selection = selection;
            return View("History");
        }

        public ActionResult ReturnToIndex()
        {
            return View("Index");
        }

        public ActionResult getStatistics()
        {
            ViewBag.Length = TempData["Length"];
            ViewBag.SelectedOption = TempData["SelectedOption"];  
            ViewBag.compOption = TempData["compOption"];      
            ViewBag.result = TempData["result"];          
            ViewBag.GameID = TempData["GameID"];          
            ViewBag.GameNumber = TempData["GameNumber"];      
            ViewBag.CurrentWins = TempData["CurrentWins"];
            ViewBag.AllWins = TempData["AllWins"];         
            return PartialView("getStatistics");
        }
    }
}
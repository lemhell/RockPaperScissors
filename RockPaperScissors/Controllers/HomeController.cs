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
        //
        // GET: /Home/

        GameContext db = new GameContext();
        RPSModel rps;
        IEnumerable<Game> games;
        Random rand = new Random();

        public HomeController()
        {
            rps = new RPSModel();
            games = db.Games;
        }

        public ActionResult Index()
        {
            db.Dispose();
            ViewBag.games = games;
            ViewBag.Greet = RPSModel.Losses.ToString();
            return View();
        }

        private void setViewBag(RPSModel.Options playerOption, RPSModel.Options compOption, RPSModel.Result result)
        {
 //           ViewBag.Length = db.Games.ToArray<Game>().Length;
            ViewBag.SelectedOption = playerOption;
            ViewBag.compOption = compOption;
            ViewBag.result = result;
            ViewBag.GameID = RPSModel.GAMEID;
            ViewBag.GameNumber = RPSModel.GAMENUMBER;
            ViewBag.CurrentWins = RPSModel.CURRENTWINS;
            ViewBag.AllWins = RPSModel.Wins;
//            TempData["Length"] = ViewBag.Length;
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
            RPSModel.CURRENTWINS += result.toInt();
            db.Games.Add(new Game { GameID = RPSModel.GAMEID++, Result = result.ToString(), 
                Type = playerOption.ToString(), EnemyType = compOption.ToString(),  Number = RPSModel.GAMENUMBER, 
                AllWins = RPSModel.Wins, Wins = RPSModel.CURRENTWINS});
            db.SaveChanges();
            setViewBag(playerOption, compOption, result);
        }

        public ViewResult Rock()
        {
            process(RPSModel.Options.ROCK);
            return View("Index");
        }

        public ActionResult Paper()
        {
            process(RPSModel.Options.PAPER);
            return View("Index");
        }
        
        public ActionResult Scissors()
        {
            process(RPSModel.Options.SCISSORS);
            return View("Index");
        }

        public ActionResult EndCurrentGame()
        {
            RPSModel.CURRENTWINS = 0;
            RPSModel.GAMENUMBER++;
            return View("Index");
        }

        public ActionResult Search(string number)
        {
            ViewBag.test = number;
            if (number == "") return View("Index");
            int num = Int32.Parse(number);
            if (num > RPSModel.GAMENUMBER)
            {
                return View("Index");
            }
            else
            {
                ViewBag.gameNumberForHistory = num;
                var selection = (from p in db.Games where p.Number == num select p).ToList();
                ViewBag.selection = selection;
                return View("History");
            }
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
            return View();
        }
    }
}
/*
@foreach (var p in ViewBag.selection) {
               @p.Type @p.Result
        }
*/
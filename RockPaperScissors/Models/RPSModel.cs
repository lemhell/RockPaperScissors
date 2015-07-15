using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissors.Models
{

    

    static class ResultMethods
    {
        public static int toInt(this RPSModel.Result res)
        {
            switch (res)
            {
                case RPSModel.Result.LOSS:
                    return 0;
                case RPSModel.Result.TIE:
                    return 0;
                case RPSModel.Result.WIN:
                    return 1;
                default:
                    return 0;
            }
        }
    }
    
    public class RPSModel
    {
        public static int GAMEID = 0, GAMENUMBER = 0, CURRENTWINS = 0;
        public static int Wins = 0;
        public static int Losses = 0;
        public static int Ties = 0;
        /*
        public int getWins() { return Wins; }
        public int getLosses() { return Losses; }
        public int getTies() { return Ties; }
        
        public static void setWins(int Wins) { this.Wins = Wins; }
        public void setLosses(int Losses) { this.Losses = Losses; }
        public void setTies(int Ties) { this.Ties = Ties; }
        */
        public enum Options { ROCK, PAPER, SCISSORS }
        public enum Result { WIN, LOSS, TIE }

        
        Random rand = new Random();

        public static Options getRandomOption(Random rand)
        {
            Array optionsArray = Enum.GetValues(typeof(Options));
            return (Options)optionsArray.GetValue(rand.Next(optionsArray.Length));
        }

        public static Result getOutcome(Options playerOption, Options compOption)
        {
            if (playerOption == Options.PAPER)
            {
                if (compOption == Options.ROCK)
                {
                    Wins++;
                    return Result.WIN;
                }
                else if (compOption == Options.SCISSORS)
                {
                    Losses++;
                    return Result.LOSS;
                }
            }
            else if (playerOption == Options.ROCK)
            {
                if (compOption == Options.PAPER)
                {
                    Losses++;
                    return Result.LOSS;
                }
                else if (compOption == Options.SCISSORS)
                {
                    Wins++;
                    return Result.WIN;
                }
            }
            else if (playerOption == Options.SCISSORS)
            {
                if (compOption == Options.PAPER)
                {
                    Wins++;
                    return Result.WIN;
                }
                else if (compOption == Options.ROCK)
                {
                    Losses++;
                    return Result.LOSS;
                }
            }
            Ties++;
            return Result.TIE;
        }
    
        
    }
}
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
        public static int GameID = 0, GameNumber = 0, CurrentWins = 0;
        public static int Wins = 0, Losses = 0, Ties = 0;
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
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumberLibrary
{
    public class GameLogic
    {
        public GameLogic(int maxGuesses)
        {
            TriesLeft = maxGuesses;
        }
        public void validateGuess(int numberToGuess, int guessedNumber)
        {
            //Comparar el numero a adivinar con el enviado.
            if (numberToGuess==guessedNumber)
            {
                ValidationResult = "m";
            }
            else if (numberToGuess > guessedNumber)
            {
                ValidationResult = "h";
                ReCalculateTriesLeft();
            }
            else
            {
                ValidationResult = "l";
                ReCalculateTriesLeft();
            }
        }

        public bool canContinue()
        {
            return TriesLeft >= 0 ? true : false;
        }

        private void ReCalculateTriesLeft()
        {
            TriesLeft -= 1;
        }

        public int TriesLeft { get; set; }

        public string ValidationResult { get; set; }

       
    }
}

using GuessTheNumberLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace GuessTheNumberUI
{
    public class Game
    {

        public Game(int maxGuesses, int numberToGuess)
        {
            GameLogic actualLogic = new GameLogic(maxGuesses);
            _logic = actualLogic;
            _numberToGuess = numberToGuess;
            TriesLeft = _logic.TriesLeft;

        }
        
        public string guessMessageResult(int guessedNumber)
        {

            string message="Keep Gaming";
            _logic.validateGuess(_numberToGuess, guessedNumber);
            switch (_logic.ValidationResult)
            {
                case "h":
                    message= "El numero es mas Alto";
                    break;
                case "l":
                    message = "El numero es mas Bajo";
                    break;
                case "m":
                    message = $"Ganaste!";
                    _isAWin = true;
                    break;
            };

            return message;
        }

        public bool isPossibleToCotninue() 
        {
            //even if its the same property maybe further treatment could be needed so that's why this property was created as a copy of the library property.
            TriesLeft = _logic.TriesLeft;
            if (_isAWin == false)
            {
                return _logic.canContinue();
                
            }
            else
            {
 
                return false;
            }

        }
        public int TriesLeft { get; set; }
        private GameLogic _logic;
        private int _numberToGuess;
        private bool _isAWin;
    }

    
}

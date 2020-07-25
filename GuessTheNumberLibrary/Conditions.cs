using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumberLibrary
{
    public class Conditions
    {

        public int UpNumber
        {
            get { return _upNumber; }
            set { _upNumber = value; }
        }

        public int LowNumber
        {
            get { return _lowNumber; }
            set { _lowNumber = value; }
        }

        public int NumberToGuess { get => _numberToGuess; }

        private int _numberToGuess;

        public void generateNumber(int low, int high)
        {
            Random myRandom = new Random();
            _numberToGuess=myRandom.Next(low,high);
        }

        private int _lowNumber;
        private int _upNumber;


    }
}

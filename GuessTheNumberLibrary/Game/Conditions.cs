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

        public int TriesToGuess
        {
            get { return _triesToGuess; }
            
        }

        public int NumberToGuess { get => _numberToGuess; }

        private void sortInterval()
        {
            //Sort the interval
            int[] interval = new int[2] { _lowNumber, _upNumber };
            Array.Sort(interval);
            _lowNumber = interval[0];
            _upNumber = interval[1];
        }

        public void generateNumber()
        {
            sortInterval();
            Random myRandom = new Random();
            _numberToGuess=myRandom.Next(_lowNumber,_upNumber);
        }


        public void calculateTries()
        {
            _triesToGuess = (int)Math.Ceiling(Math.Log2(_upNumber - _lowNumber)+1);
        }
        private int _triesToGuess;
        private int _lowNumber=0;
        private int _upNumber=0;
        private int _numberToGuess;

        public Conditions(int intervalMax, int intervalMin)
        {
            UpNumber = intervalMax;
            LowNumber = intervalMin;
        }
    }
}

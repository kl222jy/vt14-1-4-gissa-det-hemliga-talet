using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Steg1_4.Model
{
    public enum OutCome
    {
        Indefinite,
        Low,
        High,
        Correct,
        NoMoreGuesses,
        PreviousGuess
    }

    public class SecretNumber
    {
        private int _number;
        private List<int> _previousGuesses;
        public const int MaxNumberOfGuesses = 7;

        public bool CanMakeGuess
        {
            get
            {
                return (Count < MaxNumberOfGuesses);
            }
        }
        public int Count
        {
            get
            {
                return _previousGuesses.Count;
            }
        }
        public int? Number
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
            private set
            {
                if (value >= 1 && value <= 100)
                {
                    _number = (int)value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Värdet måste vara mellan 1 och 100");
                }
            }
        }
        public OutCome OutCome { get; private set; }
        public IEnumerable<int> PreviousGuesses
        {
            get
            {
                return _previousGuesses.AsReadOnly();
            }
        }

        public void Initialize()
        {
            Random rnd = new Random();
            Number = rnd.Next(1, 101);
            _previousGuesses.Clear();
            OutCome = OutCome.Indefinite;
        }
        public OutCome MakeGuess(int guess)
        {
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException("Otillåten gissning");
            }
            if (CanMakeGuess != true)
            {
                OutCome = OutCome.NoMoreGuesses;
                return OutCome;
            }
            if (PreviousGuesses.Contains(guess))
            {
                OutCome = OutCome.PreviousGuess;
                return OutCome;
            }
            if (guess < _number)
            {
                OutCome = OutCome.Low;
            }
            else if (guess > _number)
            {
                OutCome = OutCome.High;
            }
            else if (guess == _number)
            {
                OutCome = OutCome.Correct;
            }
            else
            {
                //OutCome = OutCome.Indefinite;
                throw new ApplicationException("Något gick snett");
            }

            _previousGuesses.Add(guess);
            return OutCome;
        }
        public SecretNumber()
        {
            _previousGuesses = new List<int>(MaxNumberOfGuesses);
            Initialize();
        }
    }
}
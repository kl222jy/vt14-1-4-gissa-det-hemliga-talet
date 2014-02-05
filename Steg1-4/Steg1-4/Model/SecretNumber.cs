using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Steg1_4.Model
{
    //Möjliga resultat
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

        //Får en gissning göras?
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

        //Det hemliga talet
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

        //Initieringslogik
        public void Initialize()
        {
            Random rnd = new Random();
            Number = rnd.Next(1, 101);
            _previousGuesses.Clear();
            OutCome = OutCome.Indefinite;
        }
        //Gissningslogik
        public OutCome MakeGuess(int guess)
        {
            //Kontrollera att talet är mellan 1 och 100
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException("Otillåten gissning");
            }
            //Kontrollera att en gissning får göras
            if (CanMakeGuess != true)
            {
                OutCome = OutCome.NoMoreGuesses;
                return OutCome;
            }
            //Har gissningen redan gjorts?
            if (PreviousGuesses.Contains(guess))
            {
                OutCome = OutCome.PreviousGuess;
                return OutCome;
            }
            //Är gissningen mindre än hemliga talet?
            if (guess < _number)
            {
                OutCome = OutCome.Low;
            }
            //Högre?
            else if (guess > _number)
            {
                OutCome = OutCome.High;
            }
            //Rätt?
            else if (guess == _number)
            {
                OutCome = OutCome.Correct;
            }
            else
            {
                //OutCome = OutCome.Indefinite;
                throw new ApplicationException("Något gick snett");
            }

            _previousGuesses.Add(guess);        //Lägg till gissningen i gissningshistorik
            return OutCome;                     //Returnera resultat
        }
        public SecretNumber()
        {
            _previousGuesses = new List<int>(MaxNumberOfGuesses);
            Initialize();
        }
    }
}
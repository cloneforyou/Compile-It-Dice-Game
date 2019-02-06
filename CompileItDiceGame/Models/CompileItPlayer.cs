﻿using CompileIt;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompileIt
{
    [Serializable]
    public class CompileItPlayer : Player, ITurnStatus
    {
        private const int NumberDiePerRoll = 3;
        private const int MaxErrorCount = 3;
        private const int WinningSuccessCount = 13;

        private bool _turnOver = true;
        private List<Die> _lastWarningsDie = new List<Die>();
        private Cup _cup = new Cup();

        public List<Die> RemainingDice
        {
            get
            {
                return _cup.RemainingDie;
            }
        }
        public int RoundCount { get; private set; }
        public int TotalSuccesses { get; private set; }
        public int TurnErrors { get; private set; }
        public int TurnSuccesses { get; private set; }
        public int TurnWarnings
        {
            get
            {
                return _lastWarningsDie.Count;
            }
        }
        public bool IsTurnOver
        {
            get
            {
                return TurnErrors >= MaxErrorCount || _turnOver;
            }
        }
        public bool IsWinner
        {
            get
            {
                return TotalSuccesses >= WinningSuccessCount;
            }
        }
        public double Odds
        {
            get
            {
                int numPotentialSuccesses = 0;
                int numPotentialWarnings = 0;
                int numPotentialErrors = 0;

                foreach(var die in _lastWarningsDie)
                {
                    if(die.Type == DieType.Green)
                    {
                        numPotentialSuccesses += 3;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 1;
                    }
                    else if(die.Type == DieType.Yellow)
                    {
                        numPotentialSuccesses += 2;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 2;
                    }
                    else if (die.Type == DieType.Red)
                    {
                        numPotentialSuccesses += 1;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 3;
                    }
                }

                var dieInCup = _cup.RemainingDie;
                foreach (var die in dieInCup)
                {
                    if (die.Type == DieType.Green)
                    {
                        numPotentialSuccesses += 3;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 1;
                    }
                    else if (die.Type == DieType.Yellow)
                    {
                        numPotentialSuccesses += 2;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 2;
                    }
                    else if (die.Type == DieType.Red)
                    {
                        numPotentialSuccesses += 1;
                        numPotentialWarnings += 2;
                        numPotentialErrors += 3;
                    }
                }

                //Odds the next roll is a success
                var nextRollOdds = 1.0 - ((double)numPotentialErrors / (numPotentialSuccesses + numPotentialWarnings + numPotentialErrors));

                //Generate odds that next roll will end turn
                for(int i = 0; i < TurnErrors; i++)
                {
                    nextRollOdds *= nextRollOdds;
                }

                return nextRollOdds;
            }
        }

        public CompileItPlayer(string name) : base(name)
        {
            ResetGame();
        }

        public void ResetGame()
        {
            ResetTurn();
            RoundCount = 1;
        }

        private void ResetTurn()
        {
            // Reset turn info
            TurnSuccesses = 0;
            TurnErrors = 0;
            _cup.Reset();
            _lastWarningsDie.Clear();
            _turnOver = false;
        }

        public void TakeRoll(Random dieRoller)
        {
            if (!IsWinner && !IsTurnOver)
            {
                // Collect the dice to roll
                List<Die> pulledDice = new List<Die>();
                for (int i = 0; i < (NumberDiePerRoll - _lastWarningsDie.Count); i++)
                {
                    pulledDice.Add(_cup.PullDie(dieRoller));
                }
                foreach (var die in _lastWarningsDie)
                {
                    pulledDice.Add(die);
                }
                _lastWarningsDie.Clear();

                // Roll the dice
                foreach (var die in pulledDice)
                {
                    var side = die.Roll(dieRoller);
                    if (side == CompileType.Error)
                    {
                        TurnErrors++;
                    }
                    else if (side == CompileType.Warning)
                    {
                        // Save the number of warnings rolled
                        _lastWarningsDie.Add(die);
                    }
                    else
                    {
                        TurnSuccesses++;
                    }
                }

                // Check to see if turn is already over
                if (IsTurnOver)
                {
                    TurnSuccesses = 0;
                }
            }
        }

        public void PassTurn()
        {
            if (!IsWinner)
            {
                RoundCount++;
                if (!IsTurnOver)
                {
                    TotalSuccesses += TurnSuccesses;
                }
                ResetTurn();
            }
        }
    }
}

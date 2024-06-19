// Created by: Matthew Steffan

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    /* Structure ********************************************************
     *         Structure representing a square on the board
     *******************************************************************/
    struct Square
    {
        // Holds the status of the square (X, O, "")
        private string _status;

        /* Property *********************************************************
         *           Gets and sets the status of each square
         *******************************************************************/
        public string Status
        {
            get => _status;
            set
            {
                // Prevents changing filled cells but allows us to reset the board
                if (_status != Game.X || _status != Game.O)
                    _status = value;
            }
        }

        /* Constructor ******************************************************
         *       Constructor initilizing the square to the value given 
         *******************************************************************/
        public Square(string s)
        {
            _status = s;
        }
    }

    /* Class ************************************************************
     *         Class that performs the actions of the game
     *******************************************************************/
    class Game
    {
        #region Variables, Constants, Declarations
        // Constants representing the game letters
        public const string X = "X";
        public const string O = "O";

        // Constants representing player numbers
        public const int P1 = 1;
        public const int P2 = 2;
        public const int TIE = 3;

        // List holding all of the squares (1-9 in the shape of a standard keypad)
        private Square[] grid = new Square[9];

        // Holds whose turn it is and initilizes it at the beginning to player 1
        private int _CurrentPlayer = P1;

        // Safes the state of the current game
        private bool _GameNotOver = true;

        // Variable that holds the line that was used to win the game 
        // ( horizontal is 1, 2, 3, vertical is 4, 5, 6, diagonal is 7,8 )
        private int _winLine = 0;

        #endregion Variables, Constants, Declarations

        /* Constructor ******************************************************
         *    Parameterless constructor that fills the array with
         *    empty squares
         *******************************************************************/
        public Game()
        {
            // Adds empty square to each part of grid
            for (int i = 0; i < grid.Count(); i++)
            {
                grid[i] = new Square(String.Empty);
            }
        }

        #region Properties
        /* Property *********************************************************
         *              Returns whose turn it currently is
         *******************************************************************/
        public int CurrentPlayer
        {
            get         => _CurrentPlayer;
            private set => _CurrentPlayer = value;
        }

        /* Property *********************************************************
         *              Gets and sets the game over status
         *******************************************************************/
        public bool GameNotOver
        {
            get         => _GameNotOver;
            private set => _GameNotOver = value;
        }

        /* Property *********************************************************
         *          Gets and sets the line the game was won by
         *******************************************************************/
        public int WinLine
        {
            get => _winLine;
            private set => _winLine = value;
        }

        #endregion Properties

        #region Methods
        /* Method ***********************************************************
         *   Changes the square selected to the players game letter (X, O)
         *******************************************************************/
        public void changeBoxValue(string squareToChange)
        {
            // Gets the cell number from the lable name. This way we can still 
            // have good naming of labels. -1 becuase humans count from 1
            int cellNo = Convert.ToInt32(Regex.Match(squareToChange, @"\d+").ToString()) - 1;

            // Player 1 gets X; player 2 gets O
            if (GameNotOver)
            {
                if (_CurrentPlayer == 1)
                    grid[cellNo].Status = X;
                else
                    grid[cellNo].Status = O;
            }

            // Changes which players turn it is
            changePlayerTurn();
        }

        /* Method ***********************************************************
         *   Returns the value of the square at the location given after 
         *   pulling the cell number from the name of the label
         *******************************************************************/
        public string getBoxValue(string square) => grid[Convert.ToInt32
            (Regex.Match(square, @"\d+").ToString()) - 1].Status;


        /* Method ***********************************************************
         *                  Changes the current player
         *******************************************************************/
        private void changePlayerTurn() 
        {
            if (GameNotOver)
            {
                if (_CurrentPlayer == 1)
                    _CurrentPlayer = 2;
                else
                    _CurrentPlayer = 1;
            }
        }

        /* Method ***********************************************************
         *              Resets all game values for a new game
         *******************************************************************/
        public void newGame()
        {
           for(int i = 0; i < grid.Count(); i++)
           {
                grid[i].Status = string.Empty;
           }

           GameNotOver = true;
           CurrentPlayer = P1;
           WinLine = 0;
        }

        /* Method ***********************************************************
         *           Checks if a winner or tie has been made
         *******************************************************************/
        public int checkForWin()
        {
            // Declares no winner
            int winner        = 0;
            int squaresFilled = 0;

            // Counts how many squares are filled
            for (int i = 0; i < grid.Length; i++)
                if (grid[i].Status != "")
                    squaresFilled++;

            // Checks if all squares are filled, if so, declares a tie
            if (squaresFilled == grid.Length)
                  winner = TIE;

            // Checks board for a win. If already decared as a tie but a win is found, override
            // X Horizontal
            if (grid[0].Status == X && grid[1].Status == X && grid[2].Status == X)
            {
                // Sets who the winner was
                winner = P1;

                // Used for highlighting the winning line of x or o's
                WinLine = 1;
            }
            else if (grid[3].Status == X && grid[4].Status == X && grid[5].Status == X)
            {
                winner  = P1; 
                WinLine = 2;
            }

            else if (grid[6].Status == X && grid[7].Status == X && grid[8].Status == X)
            {
                winner = P1;
                WinLine = 3;
            }
            // X Vertical
            else if (grid[0].Status == X && grid[3].Status == X && grid[6].Status == X)
            {
                winner = P1;
                WinLine = 4;
            }
            else if (grid[1].Status == X && grid[4].Status == X && grid[7].Status == X)
            {
                winner = P1;
                WinLine = 5;
            }
            else if (grid[2].Status == X && grid[5].Status == X && grid[8].Status == X)
            {
                winner = P1;
                WinLine = 6;
            }

            // X Diagonal
            else if (grid[0].Status == X && grid[4].Status == X && grid[8].Status == X)
            {
                winner = P1;
                WinLine = 7;
            }
            else if (grid[2].Status == X && grid[4].Status == X && grid[6].Status == X)
            {
                winner = P1;
                WinLine = 8;
            }

            // O Horizontal
            else if (grid[0].Status == O && grid[1].Status == O && grid[2].Status == O)
            {
                winner = P2;
                WinLine = 1;
            }
            else if (grid[3].Status == O && grid[4].Status == O && grid[5].Status == O)
            {
                winner = P2;
                WinLine = 2;
            }
            else if (grid[6].Status == O && grid[7].Status == O && grid[8].Status == O)
            {
                winner = P2;
                WinLine = 3;
            }

            // O Vertical
            else if (grid[0].Status == O && grid[3].Status == O && grid[6].Status == O)
            {
                winner = P2;
                WinLine = 4;
            }
            else if (grid[1].Status == O && grid[4].Status == O && grid[7].Status == O)
            {
                winner = P2;
                WinLine = 5;
            }
            else if (grid[2].Status == O && grid[5].Status == O && grid[8].Status == O)
            {
                winner = P2;
                WinLine = 6;
            }

            // O Diagonal
            else if (grid[0].Status == O && grid[4].Status == O && grid[8].Status == O)
            {
                winner = P2;
                WinLine = 7;
            }
            else if (grid[2].Status == O && grid[4].Status == O && grid[6].Status == O)
            {
                winner = P2;
                WinLine = 8;
            }

            // Stops the game if a winner is found
            if (winner != 0)
                GameNotOver = false;

            // If no wins are found it returns 0, if winner was found, it returns which player (3 is tie)
            return winner;
        }
        #endregion Methods
    }
}

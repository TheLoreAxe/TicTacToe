// Created by: Matthew Steffan

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        // Creates the Game object
        Game game;

        // Creates a list of the labels acting as buttons for easy manipulation
        // ( I used labels instead of buttons becuase of the diffuculty in 
        //   removing the hover highlight color of buttons )
        List<Label> listOfButtons = new List<Label>();

        // List of the win lines to loop through to avoid large switch statements
        List<Label> listOfWinLines = new List<Label>();

        // Variables holding the scores for the game
        private int p1Score  = 0,
                    p2Score  = 0,
                    tieScore = 0;

        #region Methods
        /* Method ***********************************************************
         *             Initilizes everything at the beginning
         *******************************************************************/
        public MainWindow()
        {
            InitializeComponent();

            // Adds each button to a list for looping/manupulation purposes
            listOfButtons.Add(square1);
            listOfButtons.Add(square2);
            listOfButtons.Add(square3);
            listOfButtons.Add(square4);
            listOfButtons.Add(square5);
            listOfButtons.Add(square6);
            listOfButtons.Add(square7);
            listOfButtons.Add(square8);
            listOfButtons.Add(square9);

            // Adds all the red win lines to a list for looping/Manipulation purposes
            listOfWinLines.Add(wLine1);
            listOfWinLines.Add(wLine2);
            listOfWinLines.Add(wLine3);
            listOfWinLines.Add(wLine4);
            listOfWinLines.Add(wLine5);
            listOfWinLines.Add(wLine6);
            listOfWinLines.Add(wLine7);
            listOfWinLines.Add(wLine8);

            // Init the changing text
            p1Text.Text = "Player one's turn!";
            p2Text.Text = "Player two's turn!";

            // Calls the constructor of the game
            game = new Game();
        }

        /* Method ***********************************************************
         *           Called when a player clicks a grid square
         *******************************************************************/
        private void squareClicked(object sender, RoutedEventArgs e)
        {
            // Changes the value of the box selected
            game.changeBoxValue(((Label)sender).Name);

            // If the game is not over, check for winner
            // else, game over so no need to check for winner
            if (game.GameNotOver)
            {
                // Increase the winning players score
                switch (game.checkForWin())
                {
                    case (Game.P1): // Player 1 won
                        p1Score++;
                        break;
                    case (Game.P2): // Player 2 won
                        p2Score++;
                        break;
                    case (Game.TIE): // Tie
                        tieScore++;
                        break;
                }
            }

            // Refreshes the screen to show the updated grid
            RefreshDisplay();
        }

        /* Method ***********************************************************
         *           Called when a player clicks a menu button
         *******************************************************************/
        private void menuPressed(object sender, RoutedEventArgs e)
        {
            switch(((Button)sender).Name)
            {
                // Resets game and scores
                case ("resetButton"):
                    resetScores();
                    game.newGame();
                    p1Text.Text = "Player one's turn!";
                    p2Text.Text = "Player two's turn!";
                    RefreshDisplay();
                    break;
                
                // Starts a new game
                case ("newGameButton"):

                    // Starts a new Game
                    game.newGame();

                    p1Text.Text = "Player one's turn!";
                    p2Text.Text = "Player two's turn!";

                    // Refreshes the display
                    RefreshDisplay();
                    break;

                // Quits the application
                case ("quitButton"):
                    this.Close();
                    break;
            }
        }
        #endregion Methods
        #region Listenners
        /* Listenner ********************************************************
         *                       Resets the scores 
         *******************************************************************/
        private void resetScores()
        {
            p1Score  = 0;
            p2Score  = 0;
            tieScore = 0;

            p1ScoreLabel.Content  = p1Score;
            p2ScoreLabel.Content  = p2Score;
            tieScoreLabel.Content = tieScore;
        }

        /* Listenner ********************************************************
         *         checks status of items then refreshes the display 
         *******************************************************************/
        private void RefreshDisplay()
        {
            // Refreshes the Grid
            foreach (Label iLabel in listOfButtons)
            {
                iLabel.Content = game.getBoxValue(iLabel.Name);
            }

            // Clears any red win lines 
            foreach (Label line in listOfWinLines)
            {
                line.Visibility = Visibility.Hidden;
            }

            // Refreshes the score labels
            p1ScoreLabel.Content = p1Score;
            p2ScoreLabel.Content = p2Score;
            tieScoreLabel.Content = tieScore;

            if (game.GameNotOver)
            {
                // Refreshes the player highlighted
                switch (game.CurrentPlayer)
                {
                    case (1):
                        // Changes the underline of the player
                        p1Underline.Visibility = Visibility.Visible;
                        p2Underline.Visibility = Visibility.Hidden;

                        // Changes the players turn text
                        p1TextBox.Visibility = Visibility.Visible;
                        p2TextBox.Visibility = Visibility.Hidden;

                        break;

                    case (2):
                        // Changes the underline of the player
                        p2Underline.Visibility = Visibility.Visible;
                        p1Underline.Visibility = Visibility.Hidden;

                        // Changes the players turn text
                        p2TextBox.Visibility = Visibility.Visible;
                        p1TextBox.Visibility = Visibility.Hidden;

                        break;
                }

                // Hides the end game text
                newGameText.Visibility = Visibility.Hidden;
            }

            // if game over
            else
            {
                // Hides the underline of the player
                p2Underline.Visibility = Visibility.Hidden;
                p1Underline.Visibility = Visibility.Hidden;

                // Changes the text to the winner
                switch (game.checkForWin())
                {
                    case (Game.P1): // Player 1 won
                        p1Text.Text = "Player one wins!";
                        p2TextBox.Visibility = Visibility.Hidden;
                        break;
                    case (Game.P2): // Player 2 won
                        p2Text.Text = "Player two wins!";
                        p1TextBox.Visibility = Visibility.Hidden;
                        break;
                    case (Game.TIE): // Tie
                        p1Text.Text = "Wow, you both lost";
                        break;
                }

                // Holds the value of the line that the player won
                int currentWinLine = game.WinLine;

                // Get which line was a win and makes it visible
                foreach (Label line in listOfWinLines)
                {
                    if (Convert.ToInt32(Regex.Match(line.Name, @"\d+").ToString()) == currentWinLine)
                        line.Visibility = Visibility.Visible;
                }

                // Displays the end game text
                newGameText.Visibility = Visibility.Visible;
            }
        }
        #endregion Listenners
    }
}

using GuessTheNumberLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuessTheNumberUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Iniciar dialogo para preguntar el nombre
            MyInputBox ingresaNombre = new MyInputBox(actualPlayer);
            ingresaNombre.ShowDialog();
            if (actualPlayer.Name != null)
            {
                //Inicializar jugador y bloquear creacion de nuevo jugador
                playerName.Text = actualPlayer.Name;
                playerName.Foreground = new SolidColorBrush(Colors.BlueViolet);
                startGameButton.IsEnabled = false;
                gameScoreText.Text = UpdateScore.updatePlayerScore(actualPlayer);
                gameStageSelection(1);
            }
        }

        private void gameConditions_Click(object sender, RoutedEventArgs e)
        {
            //Garantizar que el juego se puede jugar
            if (Regex.IsMatch(lowerNumberTextBox.Text, @"\D") || lowerNumberTextBox.Text.Length == 0 || Regex.IsMatch(upperNumberTextBox.Text, @"\D") || upperNumberTextBox.Text.Length == 0)
            {
                MessageBox.Show("Intervalos mal definidos o numeros incorrectos, revisar");
            }
            else if (lowerNumberTextBox.Text == upperNumberTextBox.Text)
            {
                MessageBox.Show("No se puede jugar con 2 numeros identicos, revisar");
            }
            else
            {
                //Iniciar Juego
                actualGameConditions = new Conditions(Int32.Parse(lowerNumberTextBox.Text), Int32.Parse(upperNumberTextBox.Text));
                actualGameConditions.generateNumber();
                actualGameConditions.calculateTries();
                gameStageSelection(2);
                updateGameConditionsTexts();
                lowerNumberTextBox.Text = upperNumberTextBox.Text = "0";
            }
        }

        private void gameStageSelection(int selection)
        {

            switch (selection)
            {
                //Stage 1 --- Just Game Conditions interval enabled
                case 1:
                    gameConditions.IsEnabled = true;
                    gameInterval.Visibility = Visibility.Visible;
                    lowerNumberTextBox.IsEnabled = upperNumberTextBox.IsEnabled = true;
                    gameStatusPanel.Visibility = gameIntervalData.Visibility = gameIntervalText.Visibility = Visibility.Collapsed;
                    PlayAgainButton.Visibility = Visibility.Collapsed;
                    break;
                //Stage 2 --- Enable Guessing Disable other buttons and visibility panel!
                case 2:
                    gameConditions.IsEnabled = lowerNumberTextBox.IsEnabled = upperNumberTextBox.IsEnabled = false;
                    gameInterval.Visibility = Visibility.Collapsed;
                    gameStatusPanel.Visibility = gameIntervalData.Visibility = gameIntervalText.Visibility = Visibility.Visible;
                    numeroSupuestoBox.IsEnabled = testNumberButton.IsEnabled = true;
                    break;
            }
        }

        private void updateGameConditionsTexts()
        {
            triesLeftText.Text = string.Format("{0}", actualGameConditions.TriesToGuess);
            gameIntervalText.Text = string.Format("Adivina el numero entre {0} y {1}", actualGameConditions.LowNumber, actualGameConditions.UpNumber);
        }

        private void testNumberButton_Click(object sender, RoutedEventArgs e)
        {
            //Test numbers
            if (testValidGuessTry()) { 
                //Creates a new Game with the conditions addressed.
                if (actualGame == null)
                {
                    CreateGame();
                }
                GuessResult.Text = actualGame.guessMessageResult((Int32.Parse(numeroSupuestoBox.Text)));
                triesLeftText.Text = actualGame.TriesLeft.ToString();
                //Test if the player can continue triying
                TestContinuityAndEnd();
            }

        }

        private bool testValidGuessTry() 
        {
            if (Regex.IsMatch(numeroSupuestoBox.Text, @"\D") || numeroSupuestoBox.Text.Length == 0)
            {
                MessageBox.Show("No es un numero valido");
                return false;
            }
            else if (Int32.Parse(numeroSupuestoBox.Text)>actualGameConditions.UpNumber|| Int32.Parse(numeroSupuestoBox.Text) < actualGameConditions.LowNumber)
            {
                MessageBox.Show("Numero a suponer por fuera del intervalo.");
                return false;
            }
            return true;
        }

        //Update the player according if user has guessed correctly or run out of tries
        private void UpdatePlayerScore(bool isAWin) 
        {
            //Using discard because an if statement was not as clear as this
            _ = isAWin==true? (actualPlayer.Score["Wins"] += 1) : (actualPlayer.Score["Loses"] += 1);
            //Then update the board.
            gameScoreText.Text = UpdateScore.updatePlayerScore(actualPlayer);
        }

        private void EndGame()
        {
            //End the game
            actualGame = null;
            //Empty Conditions
            actualGameConditions = null;
            PlayAgainButton.Visibility = Visibility.Visible;
            gameIntervalData.Visibility = Visibility.Collapsed;
            numeroSupuestoBox.IsEnabled = false;
            numeroSupuestoBox.Text = "0";
            
        }

        private void CreateGame() 
        {
            Game newGame = new Game(actualGameConditions.TriesToGuess, actualGameConditions.NumberToGuess);
            actualGame = newGame;
            GuessResult.Text = "";
            GuessResult.Visibility = Visibility.Visible;
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            GuessResult.Text = "";
            gameStageSelection(1);
        }

        private void TestContinuityAndEnd()
        {
            if (!actualGame.isPossibleToCotninue())
            {
                testNumberButton.IsEnabled = false;
                if (actualGame.TriesLeft <= 0)
                {
                    GuessResult.Text = $"Perdiste el numero era el {actualGameConditions.NumberToGuess}";
                    UpdatePlayerScore(isAWin: false);

                }
                else
                {
                    UpdatePlayerScore(isAWin: true);
                }
                EndGame();
            }
        }

        private Game actualGame;
        private Conditions actualGameConditions;
        private Player actualPlayer = new Player();

      
    }
}

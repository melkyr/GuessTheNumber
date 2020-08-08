using GuessTheNumberLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GuessTheNumberUI
{
    /// <summary>
    /// Interaction logic for MyInputBox.xaml
    /// </summary>
    public partial class MyInputBox : Window
    {
        public MyInputBox(Player jugador)
        {
            InitializeComponent();
            actualPlayer = jugador;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(inputBox.Text, @"\W")|| inputBox.Text.Length==0)
            { 
                MessageBox.Show("El Nombre contiene caracteres especiales o no es un nombre valido");
            }
            else
            {
                actualPlayer.Name = inputBox.Text;
                this.Close();
            }
         
        }
        public static Player actualPlayer;
    }
}

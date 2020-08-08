using GuessTheNumberLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheNumberUI
{
    public class UpdateScore
    {
        public static string updatePlayerScore(Player jugador)
        {
            string mensaje = string.Format("Victorias: {0} Derrotas: {1}", jugador.Score["Wins"], jugador.Score["Loses"]);
            return mensaje;
        }
    }
}

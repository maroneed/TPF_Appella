using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoIA
{
    public class DatosJugada
    {


        public int carta { get; set; }
        public int limiteActual { get; set; }
        public int valorDeConveniencia { get; set; }
        public bool esAI { get; set; }

        public DatosJugada(int carta, int limiteActual, bool esAI)
        {
            this.carta = carta;
            this.limiteActual = limiteActual;
            //this.esAI = esAI;
        }



    }
}

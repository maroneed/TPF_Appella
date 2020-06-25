using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoIA
{
    public class Movimiento
    {
        private int carta { get; set; }
        private int limiteActual { get; set; }
        private int ganadas { get; set; }
        private bool esAi { get; set; }

        public Movimiento(int carta, int limiteActual, int ganadas, bool esAi)
        {
            this.carta = carta;
            this.limiteActual = limiteActual;
            this.ganadas = ganadas;
            this.esAi = esAi;
        }
    }
}

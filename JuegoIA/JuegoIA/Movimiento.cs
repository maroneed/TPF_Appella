using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoIA
{
    public class Movimiento
    {
        public int carta { get; set; }
        public int limiteActual { get; set; }
        public int ganadas { get; set; }
        public bool esAi { get; set; }

        public Movimiento(int carta, int limiteActual, int ganadas, bool esAi)
        {
            this.carta = carta;
            this.limiteActual = limiteActual;
            this.ganadas = ganadas;
            this.esAi = esAi;
        }
    }
}

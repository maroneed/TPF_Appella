
using JuegoIA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		private ArbolGeneral<Movimiento> arbol;
		public ComputerPlayer()
		{
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			Movimiento comienzo = new Movimiento(-1,limite,0,true);
			arbol = new ArbolGeneral<Movimiento>(comienzo);

			generarArbol(this.arbol, cartasPropias, cartasOponente, limite);
			Console.WriteLine(this.arbol.getHijos().Count);

		}
		
		
		public override int descartarUnaCarta()
		{
			//Implementar
			return 0;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//implementar
			
		}

		public void generarArbol(ArbolGeneral<Movimiento> nodoCarta, List<int>cartasPropias, List<int> cartasOponente,int limite) 
		{
			List<int> cartasPropiasSinJugar = new List<int>(cartasPropias);
			cartasPropiasSinJugar.Remove(nodoCarta.getDatoRaiz().carta);
			int limiteActualizado = nodoCarta.getDatoRaiz().limiteActual - nodoCarta.getDatoRaiz().carta;

			if (limiteActualizado < 0)
			{
				if (nodoCarta.getDatoRaiz().esAi)
				{
					nodoCarta.getDatoRaiz().ganadas = -1;
				}
				else
				{
					nodoCarta.getDatoRaiz().ganadas = 1;
				}
			}
			else {
				foreach (int cartaOponente in cartasOponente)
				{
					Movimiento movimientoOponente = new Movimiento(cartaOponente, limiteActualizado, 0, nodoCarta.getDatoRaiz().esAi);
					ArbolGeneral<Movimiento> nodoCartaOponente = new ArbolGeneral<Movimiento>(movimientoOponente);
					generarArbol(nodoCartaOponente, cartasOponente, cartasPropiasSinJugar,limiteActualizado);
					nodoCarta.agregarHijo(nodoCartaOponente);
					nodoCarta.getDatoRaiz().ganadas += nodoCartaOponente.getDatoRaiz().ganadas;



				}
			}
		}
	}
}

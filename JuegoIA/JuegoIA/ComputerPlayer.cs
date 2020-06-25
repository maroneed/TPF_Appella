
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
			Console.WriteLine(this.arbol.getHijos().count);

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
		
	}
}

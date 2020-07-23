
using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{

	public class HumanPlayer : Jugador
	{
		private List<int> naipes = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool random_card = false;
        Consultas consulta = new Consultas();


		public HumanPlayer(){}
		
		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.WriteLine("Naipes disponibles (Usuario):");
			for (int i = 0; i < naipes.Count; i++) {
				Console.Write(naipes[i].ToString());
				if (i<naipes.Count-1) {
					Console.Write(", ");
				}
			}
			Console.Write(" ");
			Console.Write("");
			Console.WriteLine(" ");


			//esto solo es para comprobar si elige bien la maquina
			Console.WriteLine("Naipes disponibles (Computer):");
			for (int i = 0; i < naipesComputer.Count; i++)
			{
				Console.Write(naipesComputer[i].ToString());
				if (i < naipesComputer.Count - 1)
				{
					Console.Write(", ");
				}
			}

			Console.WriteLine();
			if (!random_card) {
				Console.WriteLine(" ");
                Console.WriteLine(">>Ingrese p(A)ra consultar posibles resultados desde el escenario actual:");
				Console.WriteLine(">>Ingrese (P)ara simular jugadas:");
				Console.WriteLine(">>Ingrese pa(R)a chequear una profundidad:");
				Console.WriteLine(">>Ingrese naipe:");




				string entrada = Console.ReadLine().ToUpper();

				while (entrada == "A" || entrada == "P" || entrada == "R")
				{
					consulta.menu(entrada);
					Console.WriteLine();
					Console.WriteLine("Naipes disponibles (Usuario):");
					for (int i = 0; i < naipes.Count; i++)
					{
						Console.Write(naipes[i].ToString());
						if (i < naipes.Count - 1)
						{
							Console.Write(", ");
						}
					}
					Console.WriteLine();

					Console.WriteLine("Ingrese naipe o las respectivas teclas para consutlas");



					entrada = Console.ReadLine().ToUpper();
				}

				Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) {
					Console.WriteLine("Opcion Invalida.Ingrese otro naipe:");
					entrada = Console.ReadLine();
					Int32.TryParse(entrada, out carta);
				}
			} else {
				var random = new Random();
				int index = random.Next(naipes.Count);
				carta = naipes[index];
				Console.WriteLine("Ingrese naipe:" + carta.ToString());
			}
			
			return carta;
		}
		
		public override void cartaDelOponente(int carta){
		}
		
	}
}

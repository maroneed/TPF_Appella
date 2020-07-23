
using JuegoIA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
        private static ArbolGeneral<DatosJugada> minimax;

        private static ArbolGeneral<DatosJugada> referencia = new ArbolGeneral<DatosJugada>(null);

        private static List<int> naipes = new List<int>();

        int UltimaCartaHumano = 0;
        public ComputerPlayer()
        {
        }

        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
        {
            DatosJugada datosJugada = new DatosJugada(0, limite, true);
            minimax = new ArbolGeneral<DatosJugada>(datosJugada);
            bool turno = true; //esta variable se debe declarar como atributo de clase
            naipes = cartasPropias;

            crearArbol(cartasPropias, cartasOponente, limite, turno, minimax);
            Console.WriteLine(minimax.getHijos().Count);
        }


        public override int descartarUnaCarta()
        {
            int cartaJugada = 0;
            //la primera vez referencia es null(recien arranca el arbol minimax)
            foreach (ArbolGeneral<DatosJugada> cartas in referencia.getHijos())//itera el nivel de cartas del humano (cuando referencia es el nivel de cartas de cumputer, 
                                                                               //referencia.getHijos sera de nuevo un nivel de cartas del humano)
            {
                if (cartas.getDatoRaiz().carta == UltimaCartaHumano) //busca la arbol que tenga como dato la carta que tiro el humano y se poseciona ahí
                {
                    referencia = cartas;                         //Nos ubicamos en los hijos del arbol correspondiente a la carta que tiro el humano
                    foreach (ArbolGeneral<DatosJugada> cartatirar in cartas.getHijos()) //itera el nivel de cartas de computer que corresponde al arbol de la carta que tiro el humano.
                    {
                        if (cartatirar.getDatoRaiz().valorDeConveniencia == 1)
                        {
                            referencia = cartatirar;            //deja ubicado el arbol en el nivel de computer
                            cartaJugada = cartatirar.getDatoRaiz().carta;

                            return cartaJugada;
                        }
                        else
                        {
                            //Si no encuentra con valor 1 tirara la primera que encuentre
                        }
                        {
                            referencia = cartatirar;   //deja ubicado el arbol en el nivel de computer
                            cartaJugada = cartatirar.getDatoRaiz().carta;


                        }
                    }
                }
            }
            //referencia.porNiveles();

            return cartaJugada;

        }

        public override void cartaDelOponente(int carta)
        {
            this.UltimaCartaHumano = carta;

        }

        private void crearArbol(List<int> cartasPropias, List<int> cartasOponente, int limite, bool turno, ArbolGeneral<DatosJugada> aux)
        {
            List<int> cartas = new List<int>(); //creo una lista vacia, que se llenara de cartas
            //al determinar que primero juegue el humano, siempre se ejecutará el IF
            if (turno)
            {
                // Si es el turno del usuario agrego las cartas del oponente
                cartas.AddRange(cartasOponente);
            }
            else
            {
                // Si es el turno de la IA agrego las cartas propias
                cartas.AddRange(cartasPropias);
            }

            //comienzo a llenar el arbol minimax con las cartas del humano (cada una sera un subarbol)

            foreach (int carta in cartas)  //crea, por ende, 6 hijos a la raíz null del minmax
            {
                //-----------------------------------------------------------------------------------
                DatosJugada jugada = new DatosJugada(0, 0, true);
                // Creo subarboles hijos PARA cada carta
                ArbolGeneral<DatosJugada> hijo = new ArbolGeneral<DatosJugada>(jugada);  //agrego la carta a la raiz
                hijo.getDatoRaiz().carta = carta;
                // Agrego los hijos al arbol minmax
                aux.agregarHijo(hijo);

                // Disminuyo el limite para cada carta
                int nuevoLimite = limite - carta;

                // Tendremos una lista de cartas auxiliares para hacer la llamada recursiva sin la carta que ya se jugo
                List<int> cartasAux = new List<int>();
                cartasAux.AddRange(cartas);
                cartasAux.Remove(carta); //saca la primer carta que juega el humano y a partir de ahí sigue


                // Chequeo el limite, si no se supero hago las llamadas recursivas y cambio el turno
                if (nuevoLimite >= 0)
                {
                    bool Noencontro_Humano = false;
                    //Variables que serviran para la busqueda heuristica
                    bool Noencontro_Computer = false;


                    //---------------------------crea un nuevo nivel o no (dependiendo del if anterior) invirtiendo el turno----------------------------------------


                    // Si es turno del usuario, paso las cartas auxiliares como cartas del oponente

                    if (turno)
                    {
                        crearArbol(cartasPropias, cartasAux, nuevoLimite, !turno, hijo);  //paso como arbol, a los arboles hijos de la raiz. sucesivamente

                        // Comparo las funciones heuristicas de los hijos
                        foreach (var hijos in hijo.getHijos())
                        {
                            // Si hay una con valor 1, se la seteo al padre
                            //esto hace que el -1 o 1 vaya subiendo y se setee desde la hoja hasta los hijos del primer nivel
                            if (hijos.getDatoRaiz().valorDeConveniencia == -1)
                                hijo.getDatoRaiz().valorDeConveniencia = -1;
                            else
                                Noencontro_Humano = true;
                        }
                        if (Noencontro_Humano)
                        {
                            Noencontro_Humano = !Noencontro_Humano;             //Si no encuentra quiere decir que no hay un hijo con valor -1 y entonces se le sete 1 al padre.
                            hijo.getDatoRaiz().valorDeConveniencia = 1;
                        }

                    }

                    // Si es el turno de la IA paso cartas auxiliares como cartas propias
                    else
                    {
                        crearArbol(cartasAux, cartasOponente, nuevoLimite, !turno, hijo);

                        // Comparo las funciones heuristicas de los hijos
                        foreach (var hijos in hijo.getHijos())
                        {
                            // Si hay una con valor -1, se la seteo al padre
                            if (hijos.getDatoRaiz().valorDeConveniencia == 1)
                                hijo.getDatoRaiz().valorDeConveniencia = 1;
                            else
                                Noencontro_Computer = true;
                        }
                        if (Noencontro_Computer)
                        {
                            Noencontro_Computer = !Noencontro_Computer;     //Si no encuentra quiere decir que no hay un hijo con valor 1 y entonces se le sete 1 al padre.
                            hijo.getDatoRaiz().valorDeConveniencia = -1;
                        }
                    }
                }
                //----------------------------------------------------------------------------------
                // Si se supera el limite verifico quien gano (-1 gana humano, 1 gana pc)  
                // acá se asigna el valor a las hojas ...
                else
                {
                    if (turno)
                    {
                        hijo.getDatoRaiz().valorDeConveniencia = 1;  //se pasa de limite el humano
                    }
                    else
                    {
                        hijo.getDatoRaiz().valorDeConveniencia = -1; //se pasa de limite la computadora
                    }
                }
                referencia = minimax;                //Referencia la vamos a usar para saber en que cartas estamos.

            }
        }

        
		public void porNiveles()
		{
			Cola<ArbolGeneral<DatosJugada>> c = new Cola<ArbolGeneral<DatosJugada>>();
			ArbolGeneral<DatosJugada> arbolAux;
			int contador = 0;
			c.encolar(minimax);
			c.encolar(null);
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				if (arbolAux != null)
				{
					Console.Write(arbolAux.getDatoRaiz().carta + " ");            //Modificado para mostrrar niveles. B V
					Console.Write("[" + arbolAux.getDatoRaiz().limiteActual + "]" + " ");
				}
				if (arbolAux == null)
				{
					if (!c.esVacia())
					{
						contador++;
						Console.WriteLine();
						Console.WriteLine("*****Nivel ******");
						c.encolar(null);
					}
				}
				else
				{
					if (!minimax.esHoja())
					{
						foreach (var hijo in arbolAux.getHijos())
							c.encolar(hijo);
					}
				}

			}
		}

        public void imprimirNivel(int nivel, ArbolGeneral<DatosJugada> arbol)
        {
            Cola<ArbolGeneral<DatosJugada>> c = new Cola<ArbolGeneral<DatosJugada>>();
            //ArbolGeneral<DatosJugada> aux;

            int contador = 0;
            bool NivelEncontrado = false;

            c.encolar(referencia);
            c.encolar(null);
            while (!c.esVacia())
            {
                arbol = c.desencolar();
                if (arbol != null)

                {
                    if (contador == nivel)
                    {
                        NivelEncontrado = true;
                        if (arbol.getDatoRaiz().valorDeConveniencia == 1)
                            Console.Write(arbol.getDatoRaiz().carta + " " + "[Pierde], ");
                        if (arbol.getDatoRaiz().valorDeConveniencia == -1)
                            Console.Write(arbol.getDatoRaiz().carta + " " + "[Gana], ");
                    }
                }
                if (arbol == null)
                {
                    if (!c.esVacia())
                        c.encolar(null);
                    contador++;
                }
                else
                {
                    if (!referencia.esHoja())
                    {
                        foreach (var hijo in arbol.getHijos())
                            c.encolar(hijo);
                    }
                }

            }
            if (NivelEncontrado == false)
            {
                Console.WriteLine("No se encontro la profunidad. Regresando al juego....");
            }
        }

        public void pResultados(ArbolGeneral<DatosJugada> aux) //metodo que imprime posibles resultados.
        {
            ImprimirHojas(aux);
        }
        public ArbolGeneral<DatosJugada> getMinimax()
        {

            return minimax;
        }
        public ArbolGeneral<DatosJugada> getReferencia()
        {
            return referencia; //Consigo la referencia para las consultas.
        }
        public void Niveles(int dato, ArbolGeneral<DatosJugada> arbol)
        {
            imprimirNivel(dato, arbol);
        }
        public static void eliminarMinimax()
        {
            minimax.limpiar();
            referencia.limpiar();
        }
        public static void eliminarNaipes()
        {
            naipes.Clear();
        }
        public void ImprimirHojas(ArbolGeneral<DatosJugada> arbol)
        {
            Cola<ArbolGeneral<DatosJugada>> c = new Cola<ArbolGeneral<DatosJugada>>();
            ArbolGeneral<DatosJugada> aux;
            c.encolar(arbol);  //encola minimax
            while (!c.esVacia())
            {
                aux = c.desencolar();
                if (!aux.esHoja())
                {
                    foreach (var nodo in aux.getHijos())
                    {
                        c.encolar(nodo);
                    }
                }
                if (aux.esHoja() && aux.getDatoRaiz().valorDeConveniencia == 1)
                {

                    Console.Write("|(" + aux.getDatoRaiz().carta + ")[Pierde]|, ");
                }
                if (aux.esHoja() && aux.getDatoRaiz().valorDeConveniencia == -1)
                {
                    Console.Write("|(" + aux.getDatoRaiz().carta + ")[Gana], ");
                }
            }
        }

        public void ImprimirCartas()
        {
            Console.WriteLine("Las cartas de la pc son: ");
            foreach (int cartas in naipes)
            {

                Console.Write(cartas + "" + ",");
            }
            Console.WriteLine();
        }

    }
}

using JuegoIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace juegoIA
{
     public class Consultas
    {
        ComputerPlayer computer = new ComputerPlayer();
        
        public Consultas()
        {
           
        }
        public  void menu(string entrada)
        
        {
           

            switch (entrada)
            {

                case "A":
                    Console.WriteLine();
                    Console.WriteLine("Desde el punto en el que se encuentra la partida, estos son los escenarios posibles: ");
                    ConsultaA();
                    Console.WriteLine();
                    Console.WriteLine();
                    break;

                case "P":
                    ConsultaB();
                    
                    break;
                case "R":
                    ConsultaC();
                    
                    break;
            }
        }
        private void ConsultaA()
        {
            ArbolGeneral<DatosJugada> aux = computer.getReferencia();

            computer.pResultados(aux);
        }
        private  void ConsultaB()
        {
            ArbolGeneral<DatosJugada> referencia = computer.getReferencia(); //Uso de referencia a la carta en la posicion que se encuentra el juego.
            bool dato = true;                    //Condicion para terminar las jugadas
            bool turno= true;
            computer.ImprimirCartas();
            while (dato)
            {
                if (turno==true)
                {
                    Console.WriteLine("Ingresa la carta que tirarias:");
                    string carta = Console.ReadLine();                   
                    bool aux = false;
                    while (aux == false)
                    {
                        try
                        {
                            Int32.Parse(carta);
                            aux = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Valor incorrecto.");
                            carta = Console.ReadLine();
                        }

                    }
                    foreach (var cartas in referencia.getHijos())
                    {
                        if (cartas.getDatoRaiz().carta == Int32.Parse(carta))
                        {
                            referencia = cartas;
                            turno = false;
                            
                            Console.WriteLine("Ok");
                            break;                     //Una vez que encuentra la carta pasada se rompe el foreach y evaulua el turno

                        }                        
                    }
                    if (referencia.getDatoRaiz().carta != Int32.Parse(carta))     //Si donde esta ubicado el arbol en ese momento no coincide con la carta entonces quiere decir que un valor es incorrecto.
                                                                            // O quiere decir que se paso del limite.
                    {
                        Console.WriteLine("Carta no disponible");
                        return;
                    }
                }
                if(turno==false)
                {
                    Console.WriteLine("Ingrese la carta que tiraria ComputerPlayer:");
                    string carta = Console.ReadLine();                   
                    bool aux = false;
                    while (aux == false)
                    {
                        try
                        {
                            Int32.Parse(carta);
                            aux = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Valor incorrecto.");
                            carta = Console.ReadLine();
                        }
                    }
                    foreach (var cartas in referencia.getHijos())
                    {
                        if (cartas.getDatoRaiz().carta == Int32.Parse(carta))     
                        {
                            referencia = cartas;
                            turno = true;
                            Console.WriteLine("Ok");
                            break;               //Una vez que encuentra la carta pasada se rompe el foreach y consulta si quiere terminar la jugada o seguir agregando mas jugadas.
                        }
                    }
                    if (referencia.getDatoRaiz().carta != Int32.Parse(carta))     //Si donde esta ubicado el arbol en ese momento no coincide con la carta entonces quiere decir que un valor es incorrecto.
                    {
                        Console.WriteLine("Carta no disponible");
                        return;
                    }                    
                }
                Console.WriteLine("Continuar jugada?: precione N para salir");
                string dato2 = Console.ReadLine().ToUpper();
                if (dato2 == "N")
                    dato = !dato;                                                       
            }
            Console.WriteLine("Posibles resultados para dicha jugada:");
            Console.WriteLine();
            computer.ImprimirHojas(referencia);                 //Imprime posibles resultados
        }
        private void ConsultaC()
        {
            ArbolGeneral<DatosJugada> minimaxAux = new ArbolGeneral<DatosJugada>(null);
            minimaxAux = computer.getMinimax();            
            Console.WriteLine();
            Console.WriteLine("Indique profundidad");
            string dato = Console.ReadLine();
            bool aux = false;
            while (aux == false)                        //Se comprueba que haya ingresado un valor coherente
            {
                try
                {
                    Int32.Parse(dato);
                     aux=true;              //Si puede parsear el valor sale del while e imprime en dicha profundidad
                }
                catch (Exception)
                {
                    Console.WriteLine("Valor incorrecto");
                    dato = Console.ReadLine();
                }
                
            }
            Console.WriteLine("Te mostraremos las cartas en el nivel: " + dato + ". Toca cualquier tecla para continuar !");
            Console.ReadKey();
            computer.imprimirNivel(Int32.Parse(dato),minimaxAux);            
        }
        public void reinicio()
        {
            Console.WriteLine("Queres la revancha?.     Y/N");
            string rta = Console.ReadLine().ToUpper();
            while (rta != "Y" && rta != "N")
            {
                Console.WriteLine("Error. Ingrese de nuevo");
                rta = Console.ReadLine().ToUpper();

            }
            switch (rta)
            {
                case "Y":
                   ComputerPlayer.eliminarMinimax();            
                   ComputerPlayer.eliminarNaipes();
                   Console.Clear();                    
                   Game juego = new Game();
                   juego.play();
                   break;
                case "N":
                    //minmax.limpiar();
                    Console.WriteLine("Fin del juego");                   
                    break;
            }
        }

    }
}

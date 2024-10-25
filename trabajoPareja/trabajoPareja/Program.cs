using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using trabajoPareja;

struct Jugador //Aqui se  define el struct Jugador que contiene los atributos 
                //Nombre y puntuacion donde se almacenaran los respectivos datos
    {
    public string Nombre;
    public int Puntuacion;

    public Jugador(string nombre, int puntuacion) //Cnstructor para inicializar valores
        {
        Nombre = nombre; 
        Puntuacion = puntuacion;
        }
    }

class SistemaParaPuntuaciones //Esta es la clase responsable de gestionar el sistema de puntuaciones
                                //Aqui se deben de almacenar las puntuaciones en el archivo binario
{
    private const string archivoPuntuaciones = "puntuaciones.bin"; //Esta es una constante donde se almacena el nombre del archivo binario
    private List<Jugador> jugadores; //Lista de jugadores donde se contienen las puntuaciones ya sean las nuevas o las cargadas anteriormente 

    public SistemaParaPuntuaciones() //Creamos una instancia del sistema de puntuaciones, aqui se inicializa la lista de jugadores
                                    //Y llamamos al metodo cargar puntuaciones
    {
        jugadores = new List<Jugador>();
        CargarPuntuaciones();
    }

    private void CargarPuntuaciones() //En este metodo se verifica que el archivo de puntuaciones exista
                                        //Si existe pues se abre con FileStream y BinaryReader para leer los datos
    {
        if (File.Exists(archivoPuntuaciones)) 
        {
            using (FileStream fs = new FileStream(archivoPuntuaciones, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        string nombre = reader.ReadString();
                        int puntuacion = reader.ReadInt32();
                        jugadores.Add(new Jugador(nombre, puntuacion));
                    }
                }
            }
        }
    }
    private void GuardarPuntuaciones()//En este metodo se guarda las puntuaciones actuales en el archivo binario
                                        //Utilizamos FileStream y BinaryWriter para sobreescribir el archivo cuando lo volvamos a llamar
    {
        using (FileStream fs = new FileStream(archivoPuntuaciones, FileMode.Create))
        {
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (Jugador jugador in jugadores)
                {
                    writer.Write(jugador.Nombre);
                    writer.Write(jugador.Puntuacion);
                }
            }
        }
    }

    public void AgregarPuntuacion(string nombre, int puntuacion)//Este metodo permite agregar un nuevo jugador
                                                        //El jugador se agrega a la lista junto con su puntuacion y estos archivos se guardan en el archivo binario
    {
        jugadores.Add(new Jugador(nombre, puntuacion));
        GuardarPuntuaciones();
        Console.WriteLine("Puntuacion agregada con exito");
    }

    public void EliminarPuntuacion(string nombre)//Este metodo busca y elimina a un jugador que se ingrese, si el jugador existe pues se elimina y si no existe dice que no se encontro
    {
        Jugador jugadorAEliminar = jugadores.Find(j => j.Nombre == nombre);
        if (!jugadorAEliminar.Equals(default(Jugador)))
        {
            jugadores.Remove(jugadorAEliminar);
            GuardarPuntuaciones();
            Console.WriteLine($"Puntuacion de {nombre} eliminada con exito");

        }
        else
        {
            Console.WriteLine("Jugador no encontrado.");
        }
    }

    public void MostrarPuntuaciones()// Aqui se presenta el menu interactivo que usaremos en el programa principal, como siempre uso el mismo bucle de siempre con la misma estructura KAJVKS
    {
        if (jugadores.Count == 0)
        {
            Console.WriteLine("No hay puntuaciones registradas.");
        }
        else
        {
            Console.WriteLine("Puntuaciones:");
            foreach (Jugador jugador in jugadores)
            {
                Console.WriteLine($"Jugador: {jugador.Nombre}, Puntuación: {jugador.Puntuacion}");
            }
        }
    }
    public void MostrarMenu()
    {
        int option /*= 0*/; //Aqui se define la variable que almacenara la opcion que decida el usuario 
        do //Este ciclo es para mostrar el menu hasta que el usuario decida salir 
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 * SISTEMA DE PUNTUACIONES *             ║");
            Console.WriteLine("╠═════════════════════════════╦═══════════════════════════╣");
            Console.WriteLine("║  1. Agregar Puntuacion      ║  2. Eliminar Puntuacion   ║");
            Console.WriteLine("╠═════════════════════════════╬═══════════════════════════╣");
            Console.WriteLine("║  3. Mostrar Puntuaciones    ║  4. Salir                 ║");
            Console.WriteLine("╚═════════════════════════════╩═══════════════════════════╝");
            Console.Write("Selecciona la opción a seguir: ");

            option = int.Parse(Console.ReadLine()); //Aqui se lee la opcion que desee el usuario

            Console.Clear();

            switch (option)
            {
                case 1: 
                    Console.Clear();

                    Console.WriteLine("Ingrese el nombre del jugador: ");
                    Console.Write("Ingrese el nombre del jugador: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese la puntuación: ");
                    int puntuacion = int.Parse(Console.ReadLine());
                    AgregarPuntuacion(nombre, puntuacion);
                    Console.ReadKey();
                    Console.Clear();
                    break;


                case 2: 
                    Console.Clear();

                    Console.Write("Ingrese el nombre del jugador a eliminar: ");
                    string nombreEliminar = Console.ReadLine();
                    EliminarPuntuacion(nombreEliminar);

                    Console.ReadKey();
                    Console.Clear();
                    break;


                case 3: 
                    Console.Clear();

                    MostrarPuntuaciones();

                    Console.ReadKey();
                    Console.Clear();
                    break;


                case 4: 
                    Console.Clear();

                    Console.WriteLine("Adios mundo");

                    Console.ReadKey();
                    Console.Clear();
                    break;


                    default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        } while (option != 4);
    }
}



namespace trabajoPareja // Diseña un sistema de almacenamiento para los datos de un juego (como puntuaciones de jugadores)
                        // en un archivo binario. Permite agregar nuevas puntuaciones, eliminar algunas y mostrar la lista completa. 
{
    internal class Program //Creamos instancias de la clase sistema para puntuaciones y llamamos al metodo mostrar menu
                        //Para poder interactuar con el usuario
    {
        static void Main(string[] args)
        {
            SistemaParaPuntuaciones sistema = new SistemaParaPuntuaciones();
            sistema.MostrarMenu();
        }
    }
}

//Para completar el codigo de 200 lineas 
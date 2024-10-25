using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listintelefono2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListinManager listinManager = new ListinManager();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("Gestor de Listín Telefónico");
                Console.WriteLine("1. Consultar teléfono de un cliente");
                Console.WriteLine("2. Añadir teléfono de un nuevo cliente");
                Console.WriteLine("3. Eliminar teléfono de un cliente");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nombre del cliente: ");
                        string nombreConsulta = Console.ReadLine();
                        listinManager.ConsultarTelefono(nombreConsulta);
                        break;
                    case 2:
                        Console.Write("Ingrese el nombre del cliente: ");
                        string nombreNuevo = Console.ReadLine();
                        Console.Write("Ingrese el teléfono del cliente: ");
                        string telefonoNuevo = Console.ReadLine();
                        listinManager.AñadirCliente(nombreNuevo, telefonoNuevo);
                        break;
                    case 3:
                        Console.Write("Ingrese el nombre del cliente a eliminar: ");
                        string nombreEliminar = Console.ReadLine();
                        listinManager.EliminarCliente(nombreEliminar);
                        break;
                    case 4:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            } while (opcion != 4);

            listinManager.GuardarListin();
        }
    }
}
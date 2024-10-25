using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Clase que representa a un cliente con nombre y teléfono
[Serializable]
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    class Program
    {
        // Ruta del archivo donde se guardará el listín
        static string filePath = "listin.dat";
        // Lista para almacenar los clientes en memoria
        static List<Cliente> listin = new List<Cliente>();

        static void Main(string[] args)
        {
            // Cargar los datos del listín al iniciar el programa
            CargarListin();
            int opcion;

            // Bucle que muestra el menú hasta que el usuario elija salir
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

                // Ejecuta la opción seleccionada por el usuario
                switch (opcion)
                {
                    case 1:
                        ConsultarTelefono();
                        break;
                    case 2:
                        AñadirCliente();
                        break;
                    case 3:
                        EliminarCliente();
                        break;
                    case 4:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                // Espera a que el usuario presione una tecla antes de volver al menú
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            } while (opcion != 4); // Continúa hasta que el usuario elija salir

            // Guarda los datos del listín antes de salir
            GuardarListin();
        }

        // Carga el listín desde el archivo si existe
        static void CargarListin()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    listin = (List<Cliente>)formatter.Deserialize(fs);
                }
            }
        }

        // Guarda el listín en el archivo
        static void GuardarListin()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, listin);
            }
        }

        // Consulta el teléfono de un cliente
        static void ConsultarTelefono()
        {
            Console.Write("Ingrese el nombre del cliente: ");
            string nombre = Console.ReadLine();
            // Busca el cliente en la lista
            Cliente cliente = listin.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            // Si se encuentra al cliente, muestra su teléfono
            if (cliente != null)
            {
                Console.WriteLine($"Teléfono de {cliente.Nombre}: {cliente.Telefono}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        // Añade un nuevo cliente al listín
        static void AñadirCliente()
        {
            Cliente nuevoCliente = new Cliente();
            Console.Write("Ingrese el nombre del cliente: ");
            nuevoCliente.Nombre = Console.ReadLine();
            Console.Write("Ingrese el teléfono del cliente: ");
            nuevoCliente.Telefono = Console.ReadLine();

            // Añade el nuevo cliente a la lista
            listin.Add(nuevoCliente);
            Console.WriteLine("Cliente añadido exitosamente.");
        }

        // Elimina un cliente del listín
        static void EliminarCliente()
        {
            Console.Write("Ingrese el nombre del cliente a eliminar: ");
            string nombre = Console.ReadLine();
            // Busca el cliente en la lista
            Cliente cliente = listin.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            // Si se encuentra al cliente, lo elimina
            if (cliente != null)
            {
                listin.Remove(cliente);
                Console.WriteLine("Cliente eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
    }

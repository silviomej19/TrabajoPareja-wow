using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Listintelefono2
{
    internal class Serializable
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class ListinManager
    {
        private string filePath = "listin.dat";
        private List<Serializable> listin = new List<Serializable>();

        public ListinManager()
        {
            CargarListin();
        }

        public void CargarListin()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    listin = (List<Serializable>)formatter.Deserialize(fs);
                }
            }
        }

        public void GuardarListin()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, listin);
            }
        }

        public void ConsultarTelefono(string nombre)
        {
            Serializable cliente = listin.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (cliente != null)
            {
                Console.WriteLine($"Teléfono de {cliente.Nombre}: {cliente.Telefono}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public void AñadirCliente(string nombre, string telefono)
        {
            Serializable nuevoCliente = new Serializable { Nombre = nombre, Telefono = telefono };
            listin.Add(nuevoCliente);
            Console.WriteLine("Cliente añadido exitosamente.");
        }

        public void EliminarCliente(string nombre)
        {
            Serializable cliente = listin.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
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
}
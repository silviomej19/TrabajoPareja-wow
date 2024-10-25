using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EjemploBinariaBusqueda
{
    internal class Busqueda
    {
        private int[] vector;

        public void Cargar()
        {
            Console.WriteLine("Busqueda Binaria");
            Console.WriteLine("Ingrese 10 elementos");
            vector = new int[10];
            for (int f = 0; f < vector.Length; f++)
            {
                Console.Write("Ingrese elemento " + (f + 1) + ": ");
                vector[f] = int.Parse(Console.ReadLine());
            }
        }

        public void OrdenarBurbuja()
        {
            int n = vector.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (vector[j] > vector[j + 1])
                    {
                        //Intercambiar vector[j] y vector [j+1]
                        int temp = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j + 1] = temp;
                    }
                }
            }
        }

        public void busqueda(int num)
        {
            int l = 0, h = 9;
            int m = 0;
            bool found = false;
            int contador = 0; //Aqui agrega variable para contar ocurrencias

            while (l <= h && found == false)
            {
                m = (l + h) / 2;
                if (vector[m] == num)
                    found = true;
                if (vector[m] > num)
                    h = m - 1;
                else 
                    l= m + 1; 
            }
            if (found == false)
            { Console.Write($"\nEl elemento {num} no esta en el arreglo"); }
            else
            {
                //Contar ocurrencias 
                contador = 1; //Aqui inicializamos variable en 1 porque ya encontramos la primer ocurrencia 
                //Buscar hacia la izq el primer hallazgo 
                int izquierda = m - 1;
                while (izquierda >= 0 && vector[izquierda] == num)
                {
                    contador++;
                    izquierda--;
                }

                //Buscar hacia la derecha del primer hallazho
                int derecha = m +1;
                while (derecha < vector.Length && vector[derecha] == num)
                {
                    contador++;
                    derecha++;
                }
                
                
                Console.Write($"\nEl elemento {num} esta en la posicion: {m + 1}");  
                Console.Write($"\nEl elemento {num} aparece {contador} veces en el arreglo.");
            }
        }

        public void Imprimir()
        {
            for (int f = 0; f < vector.Length; f++)
            {
                Console.Write(vector[f] + " ");
            }
        }   
        static void Main(string[] args)
        {
            Busqueda pv = new Busqueda();
            pv.Cargar();
            pv.OrdenarBurbuja(); //LLama al metodo de ordenamiento burbuja
            pv.Imprimir();
            Console.Write("\n\nIngresa elemento a buscar: ");
            int num = int.Parse(Console.ReadLine());
            pv.busqueda(num); //Llama al metodo de busqueda
            Console.ReadKey();
        }
    }
}

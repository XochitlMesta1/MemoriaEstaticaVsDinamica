using System;
using System.Text;

namespace MemoriaEstaticaVsDinamica
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("======================================================================");
                Console.WriteLine("  PRÁCTICA 1.2: MEMORIA ESTÁTICA VS. MEMORIA DINÁMICA (C# CONSOLA)    ");
                Console.WriteLine("======================================================================");
                Console.WriteLine("1. Programa 1: Generación y redimensionamiento de arreglos en ejecución");
                Console.WriteLine("2. Programa 2: Instancias dinámicas, referencias en Heap y GC");
                Console.WriteLine("0. Salir");
                Console.Write("\nSelecciona una opción: ");

                string? opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Programa1_ArreglosDinamicos.Ejecutar();
                        Pausar();
                        break;
                    case "2":
                        Programa2_InstanciasEnHeap.Ejecutar();
                        Pausar();
                        break;
                    case "3":
                        Programa1_ArreglosDinamicos.Ejecutar();
                        Console.WriteLine(new string('-', 70));
                        Programa2_InstanciasEnHeap.Ejecutar();
                        Pausar();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intenta nuevamente.");
                        Pausar();
                        break;
                }
            }
        }

        private static void Pausar()
        {
            Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
            Console.ReadKey(intercept: true);
        }
    }
}
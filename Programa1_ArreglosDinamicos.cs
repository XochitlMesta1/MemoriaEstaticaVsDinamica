using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoriaEstaticaVsDinamica
{
    
    internal static class Programa1_ArreglosDinamicos
    {
        public static void Ejecutar()
        {
            Console.WriteLine("=== PROGRAMA 1: REDIMENSIONAMIENTO DE ARREGLOS EN EJECUCIÓN ===\n");

            Console.Write("Ingresa el tamaño inicial del arreglo (ej. 4): ");
            if (!int.TryParse(Console.ReadLine(), out int tamanoInicial) || tamanoInicial <= 0)
            {
                tamanoInicial = 4;
                Console.WriteLine($"Entrada inválida. Se asigna tamaño inicial por defecto: {tamanoInicial}");
            }

            int[] vector = new int[tamanoInicial];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = (i + 1) * 10;
            }

            ImprimirVector("Vector inicial", vector);

            Console.Write("\nIngresa el nuevo tamaño para redimensionar el arreglo (ej. 8 o 2): ");
            if (!int.TryParse(Console.ReadLine(), out int nuevoTamano) || nuevoTamano < 0)
            {
                nuevoTamano = tamanoInicial * 2;
                Console.WriteLine($"Entrada inválida. Se redimensionará a: {nuevoTamano}");
            }

            Stopwatch cronometro = Stopwatch.StartNew();

            Array.Resize(ref vector, nuevoTamano);

            for (int i = tamanoInicial; i < vector.Length; i++)
            {
                vector[i] = (i + 1) * 10;
            }

            cronometro.Stop();

            ImprimirVector("Vector tras Array.Resize", vector);
            Console.WriteLine($"Tiempo de redimensionamiento: {cronometro.Elapsed.TotalMicroseconds:F2} µs");

            Console.WriteLine("\n--- Crecimiento dinámico automático con List<int> (capacidad inicial = 2) ---");
            List<int> listaDinamica = new List<int>(capacity: 2);
            int capacidadPrevia = listaDinamica.Capacity;

            for (int i = 1; i <= 10; i++)
            {
                listaDinamica.Add(i * 5);
                if (listaDinamica.Capacity != capacidadPrevia)
                {
                    Console.WriteLine(
                        $"  [Reasignación] Al insertar elemento #{i}: Capacidad {capacidadPrevia} -> {listaDinamica.Capacity}");
                    capacidadPrevia = listaDinamica.Capacity;
                }
            }

            Console.WriteLine(
                $"Resultado final List<int> -> Elementos (Count): {listaDinamica.Count}, Capacidad reservada (Capacity): {listaDinamica.Capacity}\n");
        }

        private static void ImprimirVector(string titulo, int[] arreglo)
        {
            Console.WriteLine($"{titulo} [Length = {arreglo.Length}]: [{string.Join(", ", arreglo)}]");
        }
    }
}
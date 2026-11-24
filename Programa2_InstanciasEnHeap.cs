using System;
using System.Collections.Generic;

namespace MemoriaEstaticaVsDinamica
{
  
    internal sealed class NodoSensor
    {
        public int Id { get; }
        public string Zona { get; set; }
        public double LecturaTemperatura { get; set; }

        public NodoSensor(int id, string zona, double lecturaTemperatura)
        {
            Id = id;
            Zona = zona;
            LecturaTemperatura = lecturaTemperatura;
        }

        public override string ToString() =>
            $"Instancia #{Id} | Zona: {Zona} | Temperatura: {LecturaTemperatura:F1} °C";
    }

    internal static class Programa2_InstanciasEnHeap
    {
        public static void Ejecutar()
        {
            Console.WriteLine("=== PROGRAMA 2: MEMORIA DINÁMICA (INSTANCIAS Y REFERENCIAS EN HEAP) ===\n");

            long bytesIniciales = GC.GetTotalMemory(forceFullCollection: false);
            Console.WriteLine($"1. Memoria administrada inicial en Heap: {bytesIniciales:N0} bytes");

            NodoSensor? refPrincipal = new NodoSensor(101, "Servidor-Norte", 23.4);

            NodoSensor? refSecundaria = refPrincipal;

            Console.WriteLine("\n2. Demostración de referencias compartidas (Aliasing):");
            Console.WriteLine($"   refPrincipal  -> {refPrincipal}");
            Console.WriteLine($"   refSecundaria -> {refSecundaria}");
            Console.WriteLine($"   ¿Apuntan a la misma instancia en Heap? {ReferenceEquals(refPrincipal, refSecundaria)}");

            refSecundaria.LecturaTemperatura = 31.2;
            Console.WriteLine("\n   Tras modificar LecturaTemperatura desde refSecundaria:");
            Console.WriteLine($"   refPrincipal  -> {refPrincipal}");
            Console.WriteLine($"   refSecundaria -> {refSecundaria}");

            Console.Write("\n3. ¿Cuántas instancias dinámicas adicionales deseas crear en el Heap? (ej. 5000): ");
            if (!int.TryParse(Console.ReadLine(), out int cantidadObjetos) || cantidadObjetos <= 0)
            {
                cantidadObjetos = 5000;
                Console.WriteLine($"   Entrada inválida. Se crearán {cantidadObjetos} instancias por defecto.");
            }

            List<NodoSensor> coleccionHeap = new List<NodoSensor>(cantidadObjetos);
            for (int i = 1; i <= cantidadObjetos; i++)
            {
                coleccionHeap.Add(new NodoSensor(i, $"Rack-{i}", 20.0 + (i % 15) * 0.5));
            }

            long bytesTrasAsignacion = GC.GetTotalMemory(forceFullCollection: false);
            Console.WriteLine($"   Memoria en Heap tras crear {cantidadObjetos:N0} objetos: {bytesTrasAsignacion:N0} bytes " +
                              $"(Incremento: +{bytesTrasAsignacion - bytesIniciales:N0} bytes)");

            refPrincipal = null;
            refSecundaria = null;
            coleccionHeap.Clear();
            coleccionHeap = null!;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long bytesTrasGC = GC.GetTotalMemory(forceFullCollection: true);
            Console.WriteLine($"\n4. Memoria en Heap tras anular referencias y ejecutar GC: {bytesTrasGC:N0} bytes\n");
        }
    }
}
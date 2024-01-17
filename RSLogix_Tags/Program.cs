using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1_Libplctag_Test2
{
    internal class Program
    {
        private const int DataTimeout = 5000;           // variable que determina el tiempo máximo que se permite para la execucion de lectura de datos

        static void Main(string[] args)
        {
            try
            {
                //Crear tags - tag obj
                var tag1 = new Tag("192.168.1.1", CpuType.SLC, "N7:0", DataType.Int16, 1);
                var tag2 = new Tag("192.168.1.1", CpuType.SLC, "N7:90", DataType.Int16, 1);
                var tag101 = new Tag("192.168.1.1", CpuType.SLC, "N7:50", DataType.Int16, 1);


                using (var client = new Libplctag()) //Lectura de tags
                {

                    // añade el tag
                    client.AddTag(tag1); //tags de lectura
                    client.AddTag(tag2);
                    client.AddTag(tag101); //tags de escritura


                    // verifica que el tag haya sido añadido, si devuelve PENDING hay que reintentarlo
                    while (client.GetStatus(tag1) == Libplctag.PLCTAG_STATUS_PENDING)
                    {
                        Thread.Sleep(100);
                    }
                    while (client.GetStatus(tag2) == Libplctag.PLCTAG_STATUS_PENDING)
                    {
                        Thread.Sleep(100);
                    }


                    // si el estatus de conexion esta OK, el PLC se habra enlazado exitosamente
                    if (client.GetStatus(tag1) == Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"Exito al configurar el estado interno de la etiqueta. Status: {client.DecodeError(client.GetStatus(tag1))}\n");
                        //return;       //habilitado solo en prueba
                    }
                    if (client.GetStatus(tag2) == Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"Exito al configurar el estado interno de la etiqueta. Status: {client.DecodeError(client.GetStatus(tag2))}\n");
                        //return;       //habilitado solo en prueba
                    }

                    // si el status de conexion no esta OK, tenemos que tratar el error
                    if (client.GetStatus(tag1) != Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"Error setting up tag internal state. Error{client.DecodeError(client.GetStatus(tag1))}\n");
                        return;
                    }
                    if (client.GetStatus(tag2) != Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"Error setting up tag internal state. Error{client.DecodeError(client.GetStatus(tag2))}\n");
                        return;
                    }

                    //  =   =   LECTURA DE TAGS =   =
                    // Ejecuta la lectura
                    var result1 = client.ReadTag(tag1, DataTimeout);        //Declaracion de variables de resultado en la lectura
                    var result2 = client.ReadTag(tag2, DataTimeout);
                    // Comprobacion del resultado de la operación de lectura
                    if (result1 != Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"ERROR: Unable to read the data! Got error code {result1}: {client.DecodeError(result1)}\n");
                        return;
                    }
                    if (result2 != Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"ERROR: Unable to read the data! Got error code {result2}: {client.DecodeError(result2)}\n");
                        return;
                    }
                    // Conversion de datos
                    var N7_0 = client.GetInt16Value(tag1, 0 * tag1.ElementSize);              //multiplica con tag.ElementSize para mantener los índices consistentes con los índices en el plc
                    var N7_90 = client.GetInt16Value(tag2, 0 * tag2.ElementSize);
                    // Imprime en consola leidos
                    Console.WriteLine("Datos leidos del Micrologix1400");
                    Console.WriteLine("N7:0 <> " + N7_0);
                    Console.WriteLine("N7:90 <> " + N7_90);

                    var suma = (Int16)(N7_0 + N7_90);


                    //  =   =   ESCRITURA DE TAGS =   =
                    // establece valores en el búfer de tags de escritura
                    client.SetInt16Value(tag101, 0 * tag101.ElementSize, suma); // escribe un Int16, sea variable o constante
                    // escribe los valores
                    var result101 = client.WriteTag(tag101, DataTimeout);      //Declaracion de variables de resultado en la escritura
                    // comnprueba el resultado
                    if (result101 != Libplctag.PLCTAG_STATUS_OK)
                    {
                        Console.WriteLine($"ERROR: Unable to read the data! Got error code {result101}: {client.DecodeError(result101)}\n");
                        return;
                    }
                    //Imprime los valores escritos
                    Console.WriteLine("Datos escritos del Micrologix1400");
                    Console.WriteLine("N7:50 <> " + suma);
                }
            }
            finally
            {
                Console.ReadKey();
            }

        }
    }
}

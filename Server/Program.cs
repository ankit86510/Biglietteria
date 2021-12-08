using MySql.Data.MySqlClient;
using System;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creo il servizio WCF
                ServiceHost svc = new ServiceHost(typeof(Service1));
                //Rendo disponibile il servizio WCF
                svc.Open();
                Console.WriteLine("Server in ascolto!");
                Connessioni.Connect();

                Console.ReadLine();

                //Chiudo il servizio WCF
                svc.Close();
                Console.WriteLine("Server spento!");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: {0}", ex.Message);
            }
        }
    }


}

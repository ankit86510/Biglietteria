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

                ServiceHost svc = new ServiceHost(typeof(Service1));
                svc.Open();
                Console.WriteLine("Server in ascolto!");
                Connessioni.Connect();

                Console.ReadLine();

                svc.Close();
                Console.WriteLine("Server spento!");


                //    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=Biglietteria;SSL Mode=None";
                //    MySqlConnection conn = new MySqlConnection(connectionString);
                //    conn.Open();
                //    Console.WriteLine("MENU'");
                //    Console.WriteLine("1. Registrazione");
                //    Console.WriteLine("2. LOGIN");
                //    var scelta = Console.ReadLine();

                //    if (scelta == "1")
                //    {

                //        Console.WriteLine("Inserisci il nome:");
                //        var Nome = Console.ReadLine();
                //        Console.WriteLine("Inserisci il cognome:");
                //        var Cognome = Console.ReadLine();
                //        Console.WriteLine("Inserisci  dataNascita: dd/mm/aaaa");
                //        var dataNa = Console.ReadLine();

                //        if (dataNa.Substring(2, 1) != "/" || dataNa.Substring(5, 1) != "/")
                //        {
                //            throw new Exception("Formato della data non valido! inserire una data nel formato dd/mm/aaaa");
                //        }
                //        int giorni = 0;
                //        if (int.TryParse(dataNa.Substring(0, 2), out giorni))
                //        {
                //            if (giorni < 0 || giorni > 31)
                //            {
                //                throw new Exception("Inserire un valore per i giorni compreso tra 1 e 31");
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("Giorni errato");
                //        }
                //        int mese = 0;
                //        if (int.TryParse(dataNa.Substring(3, 2), out mese))
                //        {
                //            if (mese < 0 || mese > 12)
                //            {
                //                throw new Exception("Inserire un valore per i mese compreso tra 1 e 12");
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception("mese errato");
                //        }
                //        int anno = 0;
                //        if (int.TryParse(dataNa.Substring(6, 4), out anno))
                //        {

                //        }
                //        else
                //        {
                //            throw new Exception("anno errato");
                //        }


                //        DateTime dataNascita = new DateTime(anno, mese, giorni);
                //        string dataDB = anno.ToString() + "-" + mese.ToString().PadLeft(2, '0') + "-" + giorni.ToString().PadLeft(2, '0');
                //        Console.WriteLine("Inserisci  Email ");
                //        var Email = Console.ReadLine();
                //        Console.WriteLine("Inserisci  password ");
                //        var Password = Console.ReadLine();
                //        Utente ut = new Utente(Nome, Cognome, Email, Password, dataNascita);

                //        //var connectionString = "Server=sg-sql-03;Database = SiTemplate2018;Integrated Security=SSPI";
                //        //conn = new SqlConnection(connectionString);

                //        //conn.Open();
                //        using (MySqlCommand command1 = conn.CreateCommand())
                //        {

                //            // SELECT - ResultSet --> ExecuteReader
                //            command1.CommandText = "INSERT INTO Utente (Nome, Cognome, Email, Psw, DataNascita) values (" + "'" + ut.getNome() + "','" + ut.getCognome() + "','" + ut.getEmail() + "','" + ut.getPsw() + "','" + dataDB + "');";
                //            var result = command1.ExecuteNonQuery();
                //            if (result != 0)
                //            {
                //                Console.WriteLine("Inserimento effettuato");
                //            }

                //        }
                //    }
                //    else if (scelta == "2")
                //    {
                //        Console.WriteLine("Inserisci  username ");
                //        var Email = Console.ReadLine();
                //        Console.WriteLine("Inserisci  password ");
                //        var Psw = Console.ReadLine();

                //        using (MySqlCommand command1 = conn.CreateCommand())
                //        {

                //            // SELECT - ResultSet --> ExecuteReader
                //            command1.CommandText = "select * from Utente where Email ='" + Email + "' and Psw = '" + Psw + "';";
                //            var resultSet = command1.ExecuteReader();
                //            if (resultSet.HasRows)
                //            {
                //                Console.WriteLine("OK ACCESSO EFFETTUATO!");
                //            }
                //            else
                //                Console.WriteLine("CREDENZIALI NON CORRETTE!");



                //        }
                //    }
                //    else
                //    {
                //        Console.WriteLine("Scelta non valida! ");
                //    }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore: {0}", ex.Message);
            }
        }
    }


}

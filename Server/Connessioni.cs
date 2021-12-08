using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
// add data function classes
using System.Data;

namespace Server
{
    public static class Connessioni
    {
        private static string ConectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=Biglietteria;SSL Mode=None";  // save connection string
        private static MySqlConnection conn;
        private static MySqlTransaction tx = null;

        static Connessioni() { conn = new MySqlConnection(ConectionString); }

        // funzioni per la connessione e la disconessione alla database
        public static void Connect()
        {
            conn.Open();
            Console.WriteLine(Stato());
        }
        public static void Disconnect()
        {
            conn.Open();
            Console.WriteLine(Stato());
        }
        public static string Stato()
        {
            return ("Stato connessione: " + conn.State.ToString());
        }

		public static void Insert(string q)
		{
			MySqlCommand newcomando = new MySqlCommand(q, conn); //Creazione comando x query
			newcomando.ExecuteNonQuery(); //Risultato della query
		}
		public static void Delete(string q)
		{
			MySqlCommand newcomando = new MySqlCommand(q, conn); //Creazione comando x query
			newcomando.ExecuteNonQuery(); //Risultato della query
		}
		public static MySqlDataReader Select(string q)
		{
			MySqlCommand newcomando = new MySqlCommand(q, conn); //Creazione comando x query
			var risultato = newcomando.ExecuteReader(); //Risultato della query
			return risultato;
		}

		//Getter
		public static MySqlConnection getconn() { return conn; }

		//Transazioni
		public static void InizioTransax()
		{
			tx = conn.BeginTransaction();
			return;
		}
		public static void CommitTransax()
		{
			tx.Commit();
			return;
		}
		public static void RollbackTransax()
		{
			tx.Rollback();
			return;
		}
	}
}


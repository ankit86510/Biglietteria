﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Server
{
    
    public class Service1 : IService1
    {
        //Funzioni di appoggio al server
        FunzioniServer fun = new FunzioniServer();

        //Metodi esposti dal WCF
        public Utente LogInUtente(string User, string Password)
        {
            //Ritorna True se riesce ad accedere, False se prova ad accedere con credenziali non valide

            //Esegui e incapsula query
            List<Utente> l = fun.SelectUtenti();
            foreach (var x in l)
                if (x.getEmail() == User && x.getPsw() == Password)
                    return x;
            return null;
        }
        public Admin LogInAdmin(string User, string Password)
        {
            //Ritorna True se riesce ad accedere, False se prova ad accedere con credenziali non valide

            //Esegui e incapsula query
            List<Admin> a = fun.SelectAdmin();
            foreach (var x in a)
                if (x.getEmail() == User && x.getPsw() == Password)
                    return x;
            return null;
        }
        public bool RegistrazioneUtente(string Nome, string Cognome, string Email, string Psw, string DataNascita)
        {
            try
            {
                // Ritorna True se l'email inserita per la registrazione non esiste nel DB, altrimenti False
                List<Utente> u = fun.SelectUtenti();
                List<string> email = new List<string>();
                foreach (var x in u)
                    email.Add(x.getEmail());
                if (email.Contains(Email))
                    return false;
                try
                {
                    Connessioni.Insert("INSERT INTO Utente (Nome, Cognome, Email, Psw, DataNascita) values ('" + Nome + "', '" + Cognome + "', '" + Email + "', '" + Psw + "', '" + DataNascita + "')");
                    return true;

                }
                catch (MySqlException)
                {
                    Connessioni.RollbackTransax();
                    Console.WriteLine("Errore nella transazione(clienti)");
                    return false;
                }

            }

            catch (Exception)
            {
                Connessioni.RollbackTransax();
                Console.WriteLine("Errore nel recuperare la lista dei clienti");
                return false;
            }
        }

        //Funzione restituisce lista nomi stadi in formato DataTable
        public DataTable RicercaStadio()
        {
            DataTable dt = new DataTable();
            string query = string.Empty;
            try
            {
                query = "SELECT Nome FROM biglietteria.stadio";
                var adpt = new MySqlDataAdapter(query, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "ListaStadi";
            }
            catch
            {
                Console.WriteLine("Errore nel caricamento lista stadi");
                return null;
            }
            return dt;
        }

        //Funzione inserisce nuovo evento nella tabella Partita, cercando prima il corrispettivo IDStadio della stringa Stadio recuperata da form
        public bool InsNuovaPartita(DateTime Data, DateTime Ora, string Incontro, string Stadio)
        {
            try
            {
                int IDStadio = 0;
                //Esegui e incapsula query
                using (var result = Connessioni.Select("SELECT ID FROM Stadio WHERE Nome =" + '"' + Stadio + '"'))
                    while (result.Read())
                        IDStadio = result.GetInt32("ID");
                if (fun.VerficaConcomittanzeInsPartita("Nuova", Data, Ora, Incontro, IDStadio))
                {
                    Connessioni.Insert("INSERT INTO Partita (DataPartita, OraInizioPartita, Incontro, IDStadio) " +
                    " values ('" + Data.ToString("yyyy'-'MM'-'dd") + "', '" + Ora.ToString("HH':'mm':'ss") + "', '" + Incontro + "', '" + IDStadio + "')");
                    return true;
                }
                else
                    return false;


            }
            catch (Exception)
            {
                Connessioni.RollbackTransax();
                Console.WriteLine("Errore nell'inserimento nuova partita");
                return false;
            }
        }

        //Funzione per modificare evento nella tabella Partita, cercando prima il corrispettivo IDStadio della stringa Stadio recuperata da form
        public bool ModificaPartita(int CodicePartita, DateTime Data, DateTime Ora, string Incontro, string Stadio)
        {
            try
            {
                int IDStadio = 0;
                //Esegui e incapsula query
                using (var result = Connessioni.Select("SELECT ID FROM Stadio WHERE Nome =" + '"' + Stadio + '"'))
                    while (result.Read())
                        IDStadio = result.GetInt32("ID");
                if (fun.VerficaConcomittanzeInsPartita("Modifica " + CodicePartita, Data, Ora, Incontro, IDStadio))
                {
                    Connessioni.Insert("UPDATE Partita SET DataPartita = " + "'" +  Data.ToString("yyyy'-'MM'-'dd") + "'"  +
                    " , OraInizioPartita = " + "'" + Ora.ToString("HH':'mm':'ss") + "'" + ", Incontro = " + '"' + Incontro  + '"' + ", IDStadio = " + IDStadio +
                    " WHERE Codice = " + CodicePartita);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella modifica della partita");
                return false;
            }


        }
        //Funzione per eliminare un evento nella tabella Partita
        public bool EliminaPartita(int CodicePartita)
        {
            try
            {
                Connessioni.Delete("DELETE FROM Partita WHERE Codice =" + CodicePartita);
            }
            catch (Exception)
            {
                Connessioni.RollbackTransax();
                Console.WriteLine("Errore nella modifica della partita");
                return false;
            }
            return true;

        }
        //Funzione ritorna la tabella delle partite e instazia gli oggetti per la classe partita
        public DataTable ListaPartite()
        {
            var dt = new DataTable();
            string querry = string.Empty;
            try
            {
                querry = "SELECT Partita.Codice,Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, Stadio.Citta FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID";
                var adpt = new MySqlDataAdapter(querry, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "ListaPartite";
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella creazione delle partite");
                return null;
            }
            return dt;

        }
        
        //Funzione ritorna tabella con prossimi eventi in base a ricerca scelta (Squadra - Stadio - Data)
        public List<DataTable> RicercaPer(string scelta, string value)
        {
            DataTable dt1 = new DataTable();
            dt1.TableName = "RicercaComboBox";
            DataTable dt2 = new DataTable();
            dt2.TableName = "RicercaTabella";
            List<DataTable> dt = new List<DataTable>();
            string querrydt1 = string.Empty;
            string querrydt2 = string.Empty;
            try
            {
                switch (scelta)
                {
                    case "Squadra":
                        var lsPartite = fun.SelectPartite();
                        var lsStadii = fun.SelectStadio();
                        List<string> squadre = new List<string>();
                        dt1.Columns.Add("Squadra", typeof(String));
                        dt2.Columns.Add("Codice", typeof(String));
                        dt2.Columns.Add("Incontro", typeof(String));
                        dt2.Columns.Add("DataPartita", typeof(String));
                        dt2.Columns.Add("OraInizioPartita", typeof(String));
                        dt2.Columns.Add("Luogo", typeof(String));
                        dt2.Columns.Add("Citta", typeof(String));

                        foreach (var partita in lsPartite)
                        {
                            string[] subs = partita.getIncontro().Split('-');
                            if (!squadre.Contains(subs[0]))
                                squadre.Add(subs[0]);
                            if (!squadre.Contains(subs[1]))
                                squadre.Add(subs[1]);
                            if (partita.getIncontro().Contains(value))
                            {
                                foreach (var stadio in lsStadii)
                                    if (stadio.getID() == partita.getIDStadio())
                                        dt2.Rows.Add(partita.getCodice(), partita.getIncontro(), partita.getDataPartita().ToString("dd-MMM-yy"), partita.getOraInizioPartia(), stadio.getNome(), stadio.getCitta());
                            }
                        }
                        foreach (var row in squadre)
                            dt1.Rows.Add(row);
                        dt.Add(dt1);
                        dt.Add(dt2);
                        return dt;

                    case "Data Partita":
                        DateTime dateResult = DateTime.Now;
                        querrydt1 = "SELECT Partita.DataPartita FROM Partita GROUP BY DataPartita";
                        if (value != string.Empty)
                        {
                            DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateResult);
                            querrydt2 = "SELECT Partita.Codice,Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo," +
                                    "Stadio.Citta FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID WHERE DataPartita = '"
                                    + dateResult.ToString("yyyy'-'MM'-'dd") + "'";
                        }
                        break;

                    case "Stadio":
                        querrydt1 = "SELECT Stadio.Nome FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID GROUP BY IDStadio";
                        querrydt2 = "SELECT Partita.Codice,Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, " +
                            "Stadio.Citta FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID WHERE Nome = " + '"' + value + '"';
                        break;

                    default:
                        break;
                }
                var adpt1 = new MySqlDataAdapter(querrydt1, Connessioni.getconn());
                adpt1.Fill(dt1);
                if (value != string.Empty)
                {
                    var adpt2 = new MySqlDataAdapter(querrydt2, Connessioni.getconn());
                    adpt2.Fill(dt2);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella ricerca");
                return null;
            }
            dt.Add(dt1);
            dt.Add(dt2);
            return dt;
        }

        //Ritorna la tabella delle partite selezionate per il biglietto
        public DataTable CarrelloTabelle(int v, decimal q)
        {
            var dt = new DataTable();
            var dtCloned = new DataTable();
            string querry = string.Empty;
            string data = string.Empty;
            try
            {
                //Esegui e incapsula query
                querry = "SELECT Partita.Codice,Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, " +
                    "Stadio.Citta FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID WHERE Codice = '" + v + "'";
                var adpt = new MySqlDataAdapter(querry, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "Carrello";
                dt.Columns.Add("N° Biglietti", typeof(int));
                dt.Rows[0][dt.Columns["N° Biglietti"]] = q.ToString();
                dt.Columns.Add("N° Posti", typeof(string));
                List<int> ListaPosti = fun.AssegnaPosti(v, Decimal.ToInt32(q)); 
                if (ListaPosti.Count > 20)
                    dt.Rows[0][dt.Columns["N° Posti"]] = ListaPosti[0] + ",...," + ListaPosti[ListaPosti.Count - 1];
                else
                    dt.Rows[0][dt.Columns["N° Posti"]] = string.Join(",", ListaPosti);
                dt.Columns.Add("Prezzo Biglietto in €", typeof(int));
                dt.Rows[0][dt.Columns["Prezzo Biglietto in €"]] = q * 30;
                dtCloned = dt.Clone();
                dtCloned.Columns[2].DataType = typeof(string);
                foreach (DataRow row in dt.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                dtCloned.Rows[0][dtCloned.Columns["DataPartita"]] = Convert.ToDateTime(dtCloned.Rows[0][dtCloned.Columns["DataPartita"]].ToString(), CultureInfo.CurrentCulture).ToString("dd-MMM-yy");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Errore nella creazione del carrello");
                return null;
            }
            return dtCloned;
        }

        //Inserimento della prenotazione
        public bool RegistraPrenotazione(Utente u, DataTable dt)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Connessioni.Insert("INSERT INTO Prenotazione (TotaleBiglietto, NumeroBiglietti, NumeroPosti, CodicePartita) " +
                        "values ('" + row["Prezzo Biglietto in €"] + "', '" + row["N° Biglietti"] + "', '" + row["N° Posti"] + "', '" + row["Codice"] + "')");
                    int ID = 0;
                    using (var result = Connessioni.Select("SELECT LAST_INSERT_ID() as id FROM Prenotazione"))
                        while (result.Read())
                            ID = result.GetInt32("id");
                    Connessioni.Insert("INSERT INTO Effetuazione (IDUtente, IDPrenotazione) values ('" + u.getEmail() + "', '" + ID + "')");
                }
                return true;

            }

            catch (Exception)
            {
                Connessioni.RollbackTransax();
                Console.WriteLine("Errore nella transazione(Prenotazioni)");
                return false;
            }

        }
        public bool EliminaPrenotazione(int idp)
        {
            try
            {
                Connessioni.Delete("DELETE FROM Effetuazione WHERE IDPrenotazione =" + idp);
                Connessioni.Delete("DELETE FROM Prenotazione WHERE ID =" + idp);

            }
            catch (Exception)
            {
                Connessioni.RollbackTransax();
                Console.WriteLine("Errore nell'eliminazione della prenotazione");
                return false;
            }
            return true;
        }
        //Funzione che ritorna la tabella delle prenotazioni
        public DataTable ListaPrenotazioni(string s)
        {
            var dt = new DataTable();
            string querry = string.Empty;
            try
            {
                querry = "SELECT Prenotazione.ID as ID_Prenotazione, Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, Stadio.Citta," +
                    "Prenotazione.NumeroBiglietti, Prenotazione.NumeroPosti, Prenotazione.DataOraAcquisto ,Prenotazione.TotaleBiglietto" +
                    " FROM(((Prenotazione INNER JOIN Effetuazione ON Prenotazione.ID= Effetuazione.IDPrenotazione)" +
                    " INNER JOIN Partita ON Prenotazione.CodicePartita = Partita.Codice) INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID)" +
                    " WHERE IDUtente = '" + s + "'";
                var adpt = new MySqlDataAdapter(querry, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "ListaPrenotazioni";
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella creazione delle prenotazioni");
                return null;
            }
            return dt;



        }

        //Funzione ritorna la tabella degli utenti censiti con il numero di biglietti acquistati
        public DataTable ListaUtenti()
        {
            var dt = new DataTable();
            string query = string.Empty;
            try
            {
                query = "SELECT utente.Nome,utente.Cognome,utente.Email,utente.DataNascita, SUM(prenotazione.NumeroBiglietti) as BigliettiAcquistati FROM utente inner join effetuazione on utente.Email=effetuazione.IDUtente inner join prenotazione on prenotazione.ID=effetuazione.IDPrenotazione group by Nome,Cognome";
                var adpt = new MySqlDataAdapter(query, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "ListaUtenti";
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella creazione della lista utenti");
                return null;
            }
            return dt;
        }

        //Funzione ritorna tabella
        public DataTable StoricoBiglietti()
        {
            var dt = new DataTable();
            string query = string.Empty;
            try
            {
                query = "SELECT prenotazione.DataOraAcquisto, prenotazione.NumeroBiglietti, partita.Incontro, partita.DataPartita, stadio.Nome as Stadio,stadio.Citta, Effetuazione.IDUtente as Email_Utente FROM partita inner join stadio on partita.IDStadio=stadio.ID inner join prenotazione on partita.Codice=prenotazione.CodicePartita inner join effetuazione on prenotazione.ID=effetuazione.IDPrenotazione order by DataOraAcquisto";
                var adpt = new MySqlDataAdapter(query, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "StoricoBiglietti";
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella crezione della lista biglietti venduti.");
                return null;
            }
            return dt;
        }
        //Funzione ritorna Posti totali e posti occupati per la partita passata alla stessa
        public Tuple<int, int> GetPosti(int CodicePartita)
        {
            var PostiTotali = fun.TotalePostiStadio(CodicePartita);
            var PostiOccupati = fun.PostiOccupati(CodicePartita);
            return Tuple.Create(PostiTotali, PostiOccupati);
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        //Funzioni di appoggio al server
        FunzioniServer fun = new FunzioniServer();
        public Utente LogInUtente(string User, string Password)
        {
            /*Torna True se riesce ad accedere, False se prova ad accedere con credenziali non valide*/

            //Esegui e incapsula query
            List<Utente> l = fun.SelectUtenti();
            foreach (var x in l)
                if (x.getEmail() == User && x.getPsw() == Password)
                    return x;
            return null;
        }
        public Admin LogInAdmin(string User, string Password)
        {
            /*Torna True se riesce ad accedere, False se prova ad accedere con credenziali non valide*/

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
        public DataTable ListaPartite()
        {
            /*Torna la tabella delle partite e instazia gli oggetti per la classe partita*/
            var dt = new DataTable();
            string querry = string.Empty;
            try
            {
                querry = "SELECT Partita.Codice,Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, Stadio.Citta FROM Partita INNER JOIN Stadio ON Partita.IDStadio = Stadio.ID";
                var adpt = new MySqlDataAdapter(querry, Connessioni.getconn());
                adpt.Fill(dt);
                dt.TableName = "ListaPrtite";
            }
            catch (Exception)
            {
                Console.WriteLine("Errore nella creazione delle partite");
                return null;
            }
            return dt;

        }
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
                            DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateResult);
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
        public DataTable CarrelloTabelle(int v, decimal q)
        {
            /*Torna la tabella delle partita selezionata per il biglietto*/
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
                dt.Rows[0][dt.Columns["N° Posti"]] = string.Join(",", fun.AssegnaPosti(v, Decimal.ToInt32(q)));
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
            catch (Exception)
            {
                Console.WriteLine("Errore nella creazione del carrello");
                return null;
            }
            return dtCloned;
        }
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
        public DataTable ListaPrenotazioni(string s)
        {
            /*Torna la tabella delle partite e instazia gli oggetti per la classe partita*/
            var dt = new DataTable();
            string querry = string.Empty;
            try
            {
                querry = "SELECT Partita.Incontro,Partita.DataPartita,Partita.OraInizioPartita, Stadio.Nome as Luogo, Stadio.Citta," +
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

        public DataTable ListaUtenti()
        {
            /*Torna la tabella degli utenti censiti con il numero di biglietti acquistati */
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
                Console.WriteLine("Errore nella creazione della lists utenti");
                return null;
            }
            return dt;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_SqLite_ArtikelDb.Models;

namespace Web_SqLite_ArtikelDb.Controllers
{
    public class HomeController : Controller
    {
        // Konstruktor (für mehrere Zugriffe)
        public HomeController() { }

        [HttpGet]
        public IActionResult Index()
        {
            // Artikelliste
            List<Artikel> artikelListe = new List<Artikel>();

            // 1. Connection-String
            string connStr = "Data Source =./ Artikel.db; ";

            // 2. SQL-Connection
            SqliteConnection conn = new SqliteConnection(connStr);

            // 3. SQL-Command
            SqliteCommand cmdSql = new SqliteCommand("Select * From Artikel;", conn);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen
            var dr = cmdSql.ExecuteReader();
            while (dr.Read())
            {
                Artikel artikel = new Artikel
                {
                    artikelId = (int)(long)dr[0],  // "int" = 32bit, "long" = 64bit
                    bezeichnung = dr[1].ToString(),
                    preis = (double)dr[2],
                    //vorhanden = true
                    vorhanden = dr["vorhanden"] == DBNull.Value ? null : (int)(long)dr["vorhanden"] == 1 ? true : false
                };
                artikelListe.Add(artikel);

            }

            // 6. Verbindung schließen
            conn.Close();

            return View(artikelListe);
        }

        [HttpPost]
        public IActionResult Index(List<Artikel> artikelListe)
        {
            return View(artikelListe);
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            return View(new AddArticle { vorhanden = false });
        }

        [HttpPost]
        public IActionResult AddArticle(AddArticle addArticle)
        {
            // 1. Connection-String
            string connStr = "Data Source =./ Artikel.db; ";

            // 2. SQL-Connection
            SqliteConnection conn = new SqliteConnection(connStr);

            // 3. SQL-Command (insert-Statement)
            int vorhanden = addArticle.vorhanden == true ? 1 : 0;
            double preis = addArticle.preis;
            SqliteCommand cmdSqlInsert = new SqliteCommand($"INSERT INTO Artikel ('Bezeichnung','Preis','vorhanden')" +
                $" VALUES('{addArticle.bezeichnung}','{preis}','{vorhanden}');", conn);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen
            int ok = cmdSqlInsert.ExecuteNonQuery();
            if (ok != 1) { }

            // 6. Verbindung schließen
            conn.Close();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteArticle(int id)
        {
            // 1. Connection-String
            string connStr = "Data Source =./ Artikel.db; ";

            // 2. SQL-Connection
            SqliteConnection conn = new SqliteConnection(connStr);

            // 3. SQL-Command (delete-Statement)  

            SqliteCommand cmdSqlSelect = new SqliteCommand($"SELECT * FROM Artikel WHERE AId = {id};", conn);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen (nur eine Zeile)
            var dr = cmdSqlSelect.ExecuteReader();
            dr.Read();
            Article delArticle = new Article
            {
                artikelId = (int)(long)dr[0],
                bezeichnung = dr[1].ToString(),
                preis = (double)dr[2],
                vorhanden = true
            };


            // 6. Verbindung schließen
            conn.Close();

            return View(delArticle);
        }

        [HttpPost]
        public IActionResult DeleteArticle(Article delArticle)
        {
            // 1. Connection-String
            string connStr = "Data Source =./ Artikel.db; ";

            // 2. SQL-Connection
            SqliteConnection conn = new SqliteConnection(connStr);

            // 3. SQL-Command (delete-Statement)  

            SqliteCommand cmdSqlDelete = new SqliteCommand($"DELETE FROM Artikel WHERE AId = {delArticle.artikelId};", conn);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen
            int ok = cmdSqlDelete.ExecuteNonQuery();
            if (ok != 0) { }

            // 6. Verbindung schließen
            conn.Close();

            return RedirectToAction("Index");
        }

    }
}

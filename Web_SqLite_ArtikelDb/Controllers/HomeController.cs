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
                    artikelId = (int)(long)dr[0],
                    bezeichnung = dr[1].ToString(),
                    preis = (double)dr[2],
                    vorhanden = true
                };
                artikelListe.Add(artikel);

            }
            conn.Close();

            return View(artikelListe);
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddArticle(Artikel artikel)
        {
            return View();
        }
    }
}

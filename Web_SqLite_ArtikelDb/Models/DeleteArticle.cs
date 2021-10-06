using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_SqLite_ArtikelDb.Models
{
    public class Article
    {
        public int artikelId { get; set; }

        public string bezeichnung { get; set; }

        public double preis { get; set; }

        public bool? vorhanden { get; set; }
    }
}

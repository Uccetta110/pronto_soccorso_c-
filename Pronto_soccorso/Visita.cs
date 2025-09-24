using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronto_soccorso
{
    internal class Visita
    {
        protected DateTime DataOra { get; private set; }
        protected string Codice { get; private set; }
        protected string Descizione { get; private set; }
       
        public Visita(DateTime dataOra, string codice, string descizione)
        {
            this.DataOra = dataOra;
            this.Codice = codice;
            this.Descizione = descizione;
        }
        public void VisualizzaVisita()
        {
            Console.Write($"Data e Ora: {DataOra}, Codice: {Codice}, Descrizione: {Descizione}");
        }

        public void scriviVisita(StreamWriter writer)
        {
            writer.Write($";{DataOra}|{Codice}|{Descizione}");
        }
    }
}

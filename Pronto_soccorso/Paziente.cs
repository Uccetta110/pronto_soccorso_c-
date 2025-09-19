using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronto_soccorso
{
    internal class Paziente
    {
        protected string Nome { get; private set; }
        protected string Cognome { get; private set; }
        protected string CodiceFiscale { get; private set; }
        protected DateTime DataNascita { get; private set; }
        protected List<Visita> Visite { get; private set; }
        protected DateTime DataAmmissione { get; set; }
        protected bool Visitato { get; set; }
        protected int Severita { get; set; }

        public Paziente(string nome, string cognome, string codiceFiscale, DateTime dataNascita, int severità)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
            DataNascita = dataNascita;
            Visite = new List<Visita>();
            DataAmmissione = DateTime.Now;
            Visitato = false;
            Severita = severità;
        }

        public Paziente(string nome, string cognome, string codiceFiscale, DateTime dataNascita, DateTime dataAmmissione, int severità, List<Visita> visite) 
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
            DataNascita = dataNascita;
            Visite = new List<Visita>();
            DataAmmissione = dataAmmissione;
            Severita = severità;
            Visite = visite;
        }
        public void AggiungiVisita(Visita visita)
        {
            Visite.Add(visita);
        }
        public void VisualizzaPazientePublic()
        {
            Console.Write($"Nome: {Nome}, Cognome: {Cognome}, Severità: {Severita}, Codice Fiscale: {CodiceFiscale}, \n| Data di Nascita: {DataNascita.ToShortDateString()} \n| Data di Ammissione: {DataAmmissione}, Visitato: {Visitato}");
        }

        public void VisualizzaPazienteMod()
        {
            Console.Write($"{Nome} {Cognome} Severità: {Severita}");
        }
        public int GetSeverita()
        {
            return Severita;
        }
        
        public void VisualizzaVisite()
        {
            Console.WriteLine($"--- Visite di {Nome} {Cognome} ---");
            foreach (var visita in Visite)
            {
                visita.VisualizzaVisita();
            }
        }

        public void scriviPaziente(StreamWriter writer)
        {
            writer.WriteLine($"{Severita},{Nome},{Cognome},{CodiceFiscale},{DataNascita.ToShortDateString()},{DataAmmissione},{Visitato}");
        }

        public void scriviPazienteVisitato(StreamWriter writer)
        {
            writer.WriteLine($"{Severita},{Nome},{Cognome},{CodiceFiscale},{DataNascita.ToShortDateString()},{DataAmmissione},{Visite}");
        }
    }
}

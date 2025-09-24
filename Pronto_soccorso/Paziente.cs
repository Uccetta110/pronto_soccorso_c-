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

        public Paziente(string nome, string cognome, string codiceFiscale, DateTime dataNascita, List<Visita> visite) 
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
            DataNascita = dataNascita;
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
            writer.WriteLine($"{Nome},{Cognome},{CodiceFiscale},{DataNa scita.ToShortDateString()},");
            foreach (var visita in Visite)
            {
                visita.scriviVisita(writer);
            }       
        }

        public void VisitaPaziente()
        {
            bool exit = false;
            Visita visita = null;
            while (!exit)
            {
                int r = -1;
                string descrizione, codice;
                DateTime DataOra;
                Console.WriteLine("========================== VISITA PAZIENTE =========================");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| Inserisci la descrizione della visita                             |");
                descrizione = Console.ReadLine().Trim();
                DataOra = DateTime.Now;
                codice = CodiceFiscale + DataOra.ToString("yyyyMMddHHmmss");
                Visita v_temp = new Visita(DataOra, codice, descrizione);
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("=====================================================================");
                Console.WriteLine("| Visita:                                            |");
                Console.Write("| ");
                v_temp.VisualizzaVisita();
                Console.Write("\n");
                Console.WriteLine("| [1] Si                                                            |");
                Console.WriteLine("| [2] No                                                            |");
                {
                    string r_temp = Console.ReadLine().Trim();
                    if (int.TryParse(r_temp, out int r_parsed))
                        r = Convert.ToInt32(r_temp);
                }
                Console.Clear();
                switch (r)
                {
                    case 1:
                        visita = v_temp;
                        exit = true;
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("| Scelta non disponibile                                            |");
                        Console.ReadKey();
                        break;
                }
            }
            
            Visite.Add(visita);
        }

        public void VisitaEffetuata()
        {
            Visitato = true;
        }

        public void ModificaPaziente()
        {

        }
    }
}

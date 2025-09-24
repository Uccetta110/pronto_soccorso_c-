using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronto_soccorso
{
    using static Program;
    internal class Codice
    {
        
        protected string Colore { get; private set; }
        protected string Descrizione { get; private set; }
        protected List<Paziente> Pazienti { get; }
        
        public Codice(string colore, string descrizione)
        {
            Colore = colore;
            Descrizione = descrizione;
            Pazienti = new List<Paziente>();
        }
        public void AggiungiPaziente(Paziente paziente)
        {
            Pazienti.Add(paziente);
        }
        public int VisualizzaPazienti()
        {
            int num = 1;
            foreach (var paziente in Pazienti)
            {
                Console.WriteLine("| ");
                Console.Write($"| [{num}] ");
                paziente.VisualizzaPazientePublic();
                Console.WriteLine();
                Console.WriteLine("| ");
                num++;
            }
            return Pazienti.Count();
        }

        public void OrdinaPazientiPerSeverita()
        {
            Pazienti.Sort((p1, p2) => p1.GetSeverita().CompareTo(p2.GetSeverita()));
        }

        public void scriviPazienti(StreamWriter writer)
        {
            foreach (var paziente in Pazienti)
            {
                paziente.scriviPaziente(writer);
            }   
        }
        public void VisitaEffetuata(int n)
        {
            Pazienti[n].VisitaEffetuata();
        }

        public Paziente GetPaziente(int n)
        {
            return Pazienti[n];
        }

        public void VisitaPaziente(int n, Visita visita)
        {
            Pazienti[n].AggiungiVisita(visita);
            Program.VisitaEffettuata(n, this);
        }
        
        public void RimuoviPaziente(int n)
        {
            Pazienti.RemoveAt(n);
        }

        public void ModificaPazieni(int n)
        {
            n--;
            while (true)
            {   
                int r = -1;
                Console.Clear();
                Console.Write("====================== ");
                Pazienti[n].VisualizzaPazienteMod();
                Console.Write(" ======================");
                Console.WriteLine();
                Console.Write("| ");
                Pazienti[n].VisualizzaPazientePublic(); 
                Console.WriteLine();
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [1] Cura paziente                                                 |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [2] Modifica paziente                                             |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [3] Visualizza visite                                             |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [0] Esci dal programma                                            |");
                Console.WriteLine("|                                                                   |");
                {
                    string r_temp = Console.ReadLine().Trim();
                    if (int.TryParse(r_temp, out int r_parsed))
                        r = Convert.ToInt32(r_temp);
                }
                switch (r)
                {
                    case 0:
                        return;
                        break;
                    case 1:
                        Pazienti[n].VisitaPaziente();
                        // Dopo aver creato la visita, rimuovi il paziente dalla lista
                        Program.VisitaEffettuata(n, this);
                        return;
                        break;
                    case 2:
                        Pazienti[n].ModificaPaziente();
                        break;
                    case 3:
                        Pazienti[n].VisualizzaVisite();
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Input non valido, riprova");
                        Console.ReadKey();
                        break;

                }
            }
        }

    }
}

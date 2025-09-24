using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;           

namespace Pronto_soccorso
{
    class Program
    {
        // Rendi i campi e i metodi non statici statici, oppure crea un'istanza di Program
        protected static List<Paziente> Pazienti_visitati = new List<Paziente>();
        protected static Codice Rosso = new Codice("Rosso", "Critico");
        protected static Codice Giallo = new Codice("Giallo", "Medio critico");
        protected static Codice Verde = new Codice("Verde", "Poco Critico");
        

        static void Main(string[] args)
        {
            LeggiCsv();
            int r = 100000;
            while (true)
            {
                Console.WriteLine("========================== PRONTO SOCCORSO ==========================");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [1] Ammetti Paziente                                              |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [2] Visualizza Codice ROSSO                                       |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [3] Visualizza Codice GIALLO                                      |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [4] Visualizza Codice VERDE                                       |");
                Console.WriteLine("|                                                                   |");
                Console.WriteLine("| [0] Esci dal programma                                            |");
                Console.WriteLine("|                                                                   |");
                {
                    string r_temp = Console.ReadLine().Trim();
                    if (int.TryParse(r_temp, out int r_parsed))
                        r = Convert.ToInt32(r_temp);
                }
                int count = -1;
                int num = -1;
                Console.Clear();
                switch (r)
                {
                    case 0:
                        exitProgram();
                        return;
                    case 1:
                        AmmettiPaziente();
                        SalvaInCsv();
                        break;
                    case 2:
                        Console.WriteLine("====================== VISUALIZZA CODICE ROSSO ======================");
                        count = Rosso.VisualizzaPazienti();
                        while (num < 1 || num > count)
                        {
                            Console.WriteLine("| Inserisci il numero del paziente da modificare (0 per uscire)    |");
                            string num_temp = Console.ReadLine().Trim();
                            if (int.TryParse(num_temp, out int num_parsed))
                                num = Convert.ToInt32(num_temp);
                            if (num == 0)
                                break;
                        }
                        if (num == 0)       
                            break;
                        Rosso.ModificaPazieni(num);
                                break;
                    case 3:
                        Console.WriteLine("===================== VISUALIZZA CODICE GIALLO ======================");
                        count = Giallo.VisualizzaPazienti();
                        while (num < 1 || num > count)
                        {
                            Console.WriteLine("| Inserisci il numero del paziente da modificare (0 per uscire)    |");
                            string num_temp = Console.ReadLine().Trim();
                            if (int.TryParse(num_temp, out int num_parsed))
                                num = Convert.ToInt32(num_temp);
                            if (num == 0)
                                break;
                        }
                        if (num == 0)
                            break;
                        Giallo.ModificaPazieni(num);
                        break;
                    case 4:
                        Console.WriteLine("====================== VISUALIZZA CODICE VERDE ======================");
                        count = Verde.VisualizzaPazienti();
                        while (num < 1 || num > count)
                        {
                            Console.WriteLine("| Inserisci il numero del paziente da modificare (0 per uscire)    |");
                            string num_temp = Console.ReadLine().Trim();
                            if (int.TryParse(num_temp, out int num_parsed))
                                num = Convert.ToInt32(num_temp);
                            if (num == 0)
                                break;
                        }
                        if (num == 0)
                            break;
                        Verde.ModificaPazieni(num);
                        break;

                    default:

                        Console.WriteLine("| Scelta non disponibile                                            |");
                        Console.ReadKey();
                        break;

                }

            }
            void exitProgram()
            {
                SalvaInCsv();
                Environment.Exit(0);
            }

            void SalvaInCsv()
            {
                // Implementa la logica per salvare i dati in un file CSV
                string directoryPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
                string filePathCodici = directoryPath + "codici.csv";
                string filePathPazienti = directoryPath + "pazienti.csv";

                // Salva tutti i pazienti per codice (ROSSO, GIALLO, VERDE) in codici.csv
                using (var writer = new StreamWriter(filePathCodici))
                {
                    writer.WriteLine("Severità,Nome,Cognome,Codice Fiscale,Data di Nascita,Data di Ammissione,Visitato");
                    foreach (var codice in new[] { Rosso, Giallo, Verde })
                    {
                        codice.scriviPazienti(writer);
                    }
                }

                // Salva tutti i pazienti visitati in pazienti.csv
                using (var writer = new StreamWriter(filePathPazienti))
                {
                    writer.WriteLine("Nome,Cognome,Codice Fiscale,Data di Nascita,Visite");
                    foreach (Paziente paziente in Pazienti_visitati)
                    {
                        paziente.scriviPazienteVisitato(writer);
                    }
                }
            }

            void LeggiCsv()
            {
                string directoryPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
                string filePathCodici = directoryPath + "codici.csv";
                string filePathPazienti = directoryPath + "pazienti.csv";
                if (File.Exists(filePathCodici))
                {
                    using (var reader = new StreamReader(filePathCodici))
                    {
                        string headerLine = reader.ReadLine(); // Leggi l'intestazione
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            // Assumi che i valori siano nell'ordine: Severità, Nome, Cognome, Codice Fiscale, Data di Nascita, Data di Ammissione, Visitato
                            int severita = int.Parse(values[0]);
                            string nome = values[1];
                            string cognome = values[2];
                            string codiceFiscale = values[3];
                            DateTime dataNascita = DateTime.Parse(values[4]);
                            bool visitato = bool.Parse(values[6]);
                            Paziente paziente = new Paziente(nome, cognome, codiceFiscale, dataNascita, severita);
                            // Imposta le proprietà aggiuntive se necessario
                            // paziente.DataAmmissione = dataAmmissione; // Se hai un metodo per impostare questa proprietà
                            // paziente.Visitato = visitato; // Se hai un metodo per impostare questa proprietà
                            if (severita >= 8 && severita <= 10)
                                Rosso.AggiungiPaziente(paziente);
                            else if (severita >= 4 && severita <= 7)
                                Giallo.AggiungiPaziente(paziente);
                            else if (severita >= 1 && severita <= 3)
                                Verde.AggiungiPaziente(paziente);
                        }
                    }
                }
                if (File.Exists(filePathPazienti))
                {
                    using (var reader = new StreamReader(filePathPazienti))
                    {
                        string headerLine = reader.ReadLine(); // Leggi l'intestazione
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            // Assumi che i valori siano nell'ordine: Severità, Nome, Cognome, Codice Fiscale, Data di Nascita, Data di Ammissione, Visitato
                            string nome = values[0];
                            string cognome = values[1];
                            string codiceFiscale = values[2];
                            DateTime dataNascita = DateTime.Parse(values[3]);
                                List<Visita> visite = ParseVisite(values[4]); // Implementa un metodo per convertire la stringa in una lista di visite
                            Paziente paziente = new Paziente(nome, cognome, codiceFiscale, dataNascita, visite);
                        }
                    }
                }
            }

            void AmmettiPaziente()
            {
                bool exit = false;
                Paziente paziente = null;
                while (!exit)
                {
                    string nome, cognome, codiceFiscale;
                    int severità = -1;
                    DateTime dataNascita;
                    Console.WriteLine("========================== AMMETTI PAZIENTE =========================");
                    Console.WriteLine("|                                                                   |");
                    Console.WriteLine("| Inserisci il nome del paziente                                    |");
                    nome = Console.ReadLine().Trim();
                    Console.WriteLine("| Inserisci il cognome del paziente                                 |");
                    cognome = Console.ReadLine().Trim();
                    Console.WriteLine("| Inserisci il codice fiscale del paziente                          |");
                    codiceFiscale = Console.ReadLine().Trim();
                    Console.WriteLine("| Inserisci la data di nascta del paziente (\"MM/dd/yyyy\")         |");
                    string dataNascitaInput = Console.ReadLine().Trim();
                    while (!DateTime.TryParseExact(dataNascitaInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascita))
                    {
                        Console.WriteLine("Data non valida. Riprova.");
                        Console.WriteLine("| Inserisci la data di nascta del paziente (\"MM/dd/yyyy\")         |");
                        dataNascitaInput = Console.ReadLine().Trim();
                    }
                    while (severità < 1 || severità > 10)
                    {
                        Console.WriteLine("| Inserisci la severità del paziente (1-3 verde 4-7 giallo 8-10 rosso)  |");
                        severità = Convert.ToInt32(Console.ReadLine().Trim());
                        if (severità < 1 || severità > 10)
                            Console.WriteLine("Severità non valida. Riprova.");
                    }

                    Paziente p_temp = new Paziente(nome, cognome, codiceFiscale, dataNascita, severità);
                    Console.WriteLine("|                                                                   |");
                    Console.WriteLine("=====================================================================");
                    Console.WriteLine("| Paziente da ammettere:                                            |");
                    Console.Write("| ");
                    p_temp.VisualizzaPazientePublic();
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
                            paziente = p_temp;
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
                if (paziente.GetSeverita() >= 8 && paziente.GetSeverita() <= 10)
                {
                    Rosso.AggiungiPaziente(paziente);       
                    Console.WriteLine("Paziente aggiunto al codice ROSSO");
                    Rosso.OrdinaPazientiPerSeverita();
                }
                else if (paziente.GetSeverita() >= 4 && paziente.GetSeverita() <= 7)
                {
                    Giallo.AggiungiPaziente(paziente);
                    Console.WriteLine("Paziente aggiunto al codice GIALLO");
                    Giallo.OrdinaPazientiPerSeverita();
                }
                else if (paziente.GetSeverita() >= 1 && paziente.GetSeverita() <= 3)
                {
                    Verde.AggiungiPaziente(paziente);
                    Console.WriteLine("Paziente aggiunto al codice VERDE");
                    Verde.OrdinaPazientiPerSeverita();
                }
                else
                {
                    Console.WriteLine("Severità non valida. Paziente non aggiunto.");
                }
            }

            List<Visita> ParseVisite(string visiteString)
            {
                var visite = new List<Visita>();
                if (string.IsNullOrWhiteSpace(visiteString))
                    return visite;
                var visiteEntries = visiteString.Split(';');
                foreach (var entry in visiteEntries)
                {
                    var campi = entry.Split('|');
                    if (campi.Length == 3 &&
                        DateTime.TryParse(campi[0], out DateTime dataOra))
                    {
                        var codice = campi[1];
                        var descrizione = campi[2];
                        // Assumendo che Visita abbia un costruttore appropriato
                        visite.Add(new Visita(dataOra, codice, descrizione));
                    }
                }
                return visite;
            }


        }
        public static void VisitaEffettuata(int n, Codice codice)
        {
            codice.VisitaEffetuata(n);
            Pazienti_visitati.Add(codice.GetPaziente(n));
            codice.RimuoviPaziente(n);
        }



    }
}

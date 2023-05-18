// Program_TEST_Datenverarbeitung.cs (zu Zulassungsaufgabe 23S)
// Test des Namensraums 'Logik'
// Konsolenanwendung

#nullable disable

using System;
using EasyBankingBackOffice.Datenhaltung.Transfer;
using EasyBankingBackOffice.Datenverarbeitung.Logik;
using EasyBankingBackOffice.Datenverarbeitung.Transfer;
using static Testing.TestSupport;

namespace EasyBankingBackOffice.TestLogik
{
    class Program
    {

        /// <summary>
        /// private Methode zum Vergleich zweier double-Zahlen
        /// </summary>
        /// <param name="d1">erste Zahl</param>
        /// <param name="d2">zweite Zahl</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung</param>
        /// <remarks>erlaubte Abweichung muss kleiner 10^-10 sein</remarks>
        private static void CompareAndPrintDoubleTolerance(double d1, double d2, ConsoleColor trueColor = GoodColor, ConsoleColor falseColor = BadColor, double tolerance = 1E-10)
        {
            ColorPrint(d1.ToString(), Math.Abs(d1 - d2) < tolerance ? trueColor : falseColor);
        }

        /// <summary>
        /// Methode zum Vergleich einer Zeile der Processingtabelle mit Vorgaben
        /// </summary>
        /// <param name="zeile">zu untersuchende Zeile der Processingtabelle</param>
        /// <param name="einheitenAP">Vorgabe für Spalte 'Einheiten AP'</param>
        /// <param name="mitarbeiterProEinheit">Vorgabe für Spalte 'Benötigte Mitarbeiter pro Einheit'</param>
        /// <param name="benötigteMitarbeiter">Vorgabe für Spalte 'Benötigte Mitarbeiter'</param>
        private static void VergleicheProcessingZeile(Processingtabelle.Eintrag zeile, double einheitenAP, double mitarbeiterProEinheit, double benötigteMitarbeiter)
        {
            Console.WriteLine(zeile.Bezeichnung);
            CompareAndPrintDoubleTolerance(zeile.EinheitenAP, einheitenAP);
            CompareAndPrintDoubleTolerance(zeile.MitarbeiterProEinheit, mitarbeiterProEinheit);
            CompareAndPrintDoubleTolerance(zeile.BenötigteMitarbeiter, benötigteMitarbeiter, tolerance: 1E-2);
        }

        /// <summary>
        /// Methode zum Vergleich einer Zeile der Auslastungstabelle mit Vorgaben
        /// </summary>
        /// <param name="zeile">zu untersuchende Zeile der Auslastungstabelle</param>
        /// <param name="vorperiode">Vorgabe für Spalte 'Vorperiode'</param>
        /// <param name="aktuellePeriode">Vorgabe für Spalte 'aktuelle Periode'</param>
        private static void VergleicheAuslastungsZeile(Auslastungstabelle.Eintrag zeile, double vorperiode, double aktuellePeriode)
        {
            Console.WriteLine(zeile.Bezeichnung);
            CompareAndPrintDoubleTolerance(zeile.Vorperiode, vorperiode, tolerance: 1E-2);
            CompareAndPrintDoubleTolerance(zeile.AktuellePeriode, aktuellePeriode, tolerance: 1E-2);
        }

        /// <summary>
        /// Hauptmethode
        /// </summary>
        static void Main()
        {
            Console.WriteLine("\n\n--- Klasse Processingtabelle.Eintrag ---\n");

            // vorgegebene Werte
            const string ProcessingEintragBezeichnung = "Bezeichnung";
            const double ProcessingEintragEinheitenAP = 6.0;
            const double ProcessingEintragMitarbeiterProEinheit = 7.0;
            const double ProcessingEintragbenötigteMitarbeiter = 8.0;

            // Objekt anlegen
            Processingtabelle.Eintrag processingEintrag = new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                                        ProcessingEintragEinheitenAP,
                                                                                        ProcessingEintragMitarbeiterProEinheit,
                                                                                        ProcessingEintragbenötigteMitarbeiter);

            // Test auf korrekte Werte der Eigenschaften
            PL(); CompareAndPrint(processingEintrag.Bezeichnung, ProcessingEintragBezeichnung);
            PL(); CompareAndPrint(processingEintrag.EinheitenAP, ProcessingEintragEinheitenAP);
            PL(); CompareAndPrint(processingEintrag.MitarbeiterProEinheit, ProcessingEintragMitarbeiterProEinheit);
            PL(); CompareAndPrint(processingEintrag.BenötigteMitarbeiter, ProcessingEintragbenötigteMitarbeiter);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            PL(); NonwriteableProperties<Processingtabelle.Eintrag>();

            // Test der Konstruktor-Plausibilitätsprüfungen
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(null,
                                                                 ProcessingEintragEinheitenAP,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 0.0,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 ProcessingEintragEinheitenAP,
                                                                 0.0,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 ProcessingEintragEinheitenAP,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 0.0));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(" ",
                                                                 ProcessingEintragEinheitenAP,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 -10.0,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 ProcessingEintragEinheitenAP,
                                                                 -10.0,
                                                                 ProcessingEintragbenötigteMitarbeiter));
            PL(); ProvokeException(() => new Processingtabelle.Eintrag(ProcessingEintragBezeichnung,
                                                                 ProcessingEintragEinheitenAP,
                                                                 ProcessingEintragMitarbeiterProEinheit,
                                                                 -10.0));

            Console.WriteLine("\n\n--- Klasse Processingtabelle ---\n");

            // Test auf öffentliche Nicht-Änderbarkeit der Eigenschaften
            PL(); IsNull(typeof(Processingtabelle).GetProperty("Tabelle").GetSetMethod());
            PL(); IsNull(typeof(Processingtabelle).GetProperty("BenötigteBackofficeMitarbeiter").GetSetMethod());

            // Instanziierung und initiale Werte
            Processingtabelle processingtabelle = new Processingtabelle();
            PL(); CompareAndPrint(processingtabelle.BenötigteBackofficeMitarbeiter, 0.0);
            PL(); CompareAndPrint(processingtabelle.Tabelle.Length, 0);

            // Test der Plausibilitätsprüfungen der Methode NeuerEintrag
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag(null, 1.0, 2.0));
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag(" ", 1.0, 2.0));
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag("Bezeichnung", 0.0, 2.0));
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag("Bezeichnung", -1.0, 2.0));
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag("Bezeichnung", 1.0, 0.0));
            PL(); ProvokeException(() => processingtabelle.NeuerEintrag("Bezeichnung", 1.0, -2.0));

            // Test auf Korrektheit der eingespeicherten und errechneten Werte
            const int max = 10;
            var nummern = Enumerable.Range(1, max);
            string[] bezeichnungen = nummern.Select(x => "Eintrag " + x.ToString()).ToArray();
            double[] einheitenAP = nummern.Select(x => Convert.ToDouble(x)).ToArray();
            double[] mitarbeiterProEinheit = nummern.Select(x => x * .1).ToArray();
            double[] benötigteMitarbeiter = einheitenAP.Zip(mitarbeiterProEinheit, (e, mpe) => e * mpe).ToArray();
            double summe = benötigteMitarbeiter.Sum();

            for (int i = 0; i < max; i++)
                processingtabelle.NeuerEintrag(bezeichnungen[i], einheitenAP[i], mitarbeiterProEinheit[i]);

            Processingtabelle.Eintrag[] liste = processingtabelle.Tabelle;
            PL(); CompareAndPrint(liste.Length, max);
            PL(); for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"[{i}]");
                CompareAndPrint(liste[i].Bezeichnung, bezeichnungen[i]);
                CompareAndPrintDouble(liste[i].EinheitenAP, einheitenAP[i]);
                CompareAndPrintDouble(liste[i].MitarbeiterProEinheit, mitarbeiterProEinheit[i]);
                CompareAndPrintDouble(liste[i].BenötigteMitarbeiter, benötigteMitarbeiter[i]);
            }
            PL(); Console.Write("Summe: ");
            CompareAndPrintDouble(processingtabelle.BenötigteBackofficeMitarbeiter, summe);

            // Test auf Unveränderlichkeit der Ausgabetabelle
            processingtabelle.Tabelle[0] = null;
            PL(); IsNull(processingtabelle.Tabelle[0], BadColor, GoodColor);

            Console.WriteLine("\n\n--- Klasse Auslastungstabelle.Eintrag ---\n");

            // vorgegebene Werte
            const string auslastungsEintragBezeichnung = "Bezeichnung";
            const double auslastungsEintragVorperiode = 6.0;
            const double auslastungsEintragAktuellePeriode = 7.0;

            // Objekt anlegen
            Auslastungstabelle.Eintrag auslastungsEintrag = new Auslastungstabelle.Eintrag(auslastungsEintragBezeichnung,
                                                                                           auslastungsEintragVorperiode,
                                                                                           auslastungsEintragAktuellePeriode);

            // Test auf korrekte Werte der Eigenschaften
            PL(); CompareAndPrint(auslastungsEintrag.Bezeichnung, auslastungsEintragBezeichnung);
            PL(); CompareAndPrint(auslastungsEintrag.Vorperiode, auslastungsEintragVorperiode);
            PL(); CompareAndPrint(auslastungsEintrag.AktuellePeriode, auslastungsEintragAktuellePeriode);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            PL(); NonwriteableProperties<Auslastungstabelle.Eintrag>();

            // Test der Konstruktor-Plausibilitätsprüfungen
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(null,
                                                                  auslastungsEintragVorperiode,
                                                                  auslastungsEintragAktuellePeriode));
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(auslastungsEintragBezeichnung,
                                                                  0.0,
                                                                  auslastungsEintragAktuellePeriode));
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(auslastungsEintragBezeichnung,
                                                                  auslastungsEintragVorperiode,
                                                                  0.0));
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(" ",
                                                                  auslastungsEintragVorperiode,
                                                                  auslastungsEintragAktuellePeriode));
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(auslastungsEintragBezeichnung,
                                                                  -10.0,
                                                                  auslastungsEintragAktuellePeriode));
            PL(); ProvokeException(() => new Auslastungstabelle.Eintrag(auslastungsEintragBezeichnung,
                                                                  auslastungsEintragVorperiode,
                                                                  -10.0));

            Console.WriteLine("\n\n--- Klasse Auslastungstabelle ---\n");

            // Test auf öffentliche Nicht-Änderbarkeit der Eigenschaften
            PL(); IsNull(typeof(Auslastungstabelle).GetProperty("Tabelle").GetSetMethod());

            // Instanziierung und initiale Werte
            Auslastungstabelle auslastungstabelle = new Auslastungstabelle();
            PL(); CompareAndPrint(auslastungstabelle.Tabelle.Length, 0);

            // Test der Plausibilitätsprüfungen der Methode NeuerEintrag
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag(null, 1.0, 2.0));
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag(" ", 1.0, 2.0));
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag("Bezeichnung", 0.0, 2.0));
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag("Bezeichnung", -1.0, 2.0));
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag("Bezeichnung", 1.0, 0.0));
            PL(); ProvokeException(() => auslastungstabelle.NeuerEintrag("Bezeichnung", 1.0, -2.0));

            // Test auf Korrektheit der eingespeicherten und errechneten Werte
            double[] vorperioden = nummern.Select(x => Convert.ToDouble(x)).ToArray();
            double[] aktuellePerioden = nummern.Select(x => x * 2.0).ToArray();

            for (int i = 0; i < max; i++)
                auslastungstabelle.NeuerEintrag(bezeichnungen[i], vorperioden[i], aktuellePerioden[i]);

            Auslastungstabelle.Eintrag[] liste2 = auslastungstabelle.Tabelle;
            PL(); CompareAndPrint(liste2.Length, max);
            PL(); for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"[{i}]");
                CompareAndPrint(liste2[i].Bezeichnung, bezeichnungen[i]);
                CompareAndPrintDouble(liste2[i].Vorperiode, vorperioden[i]);
                CompareAndPrintDouble(liste2[i].AktuellePeriode, aktuellePerioden[i]);
            }

            // Test auf Unveränderlichkeit der Ausgabetabelle
            auslastungstabelle.Tabelle[0] = null;
            PL(); IsNull(auslastungstabelle.Tabelle[0], BadColor, GoodColor);

            Console.WriteLine("\n\n--- Methode Berechnung.Processingtabelle ---\n");

            Einheiten eVP = new Einheiten(154.62, 5.42, 585.0, .00004, .00005, .00006, 15.0);
            Einheiten eAP = new Einheiten(.00001, .00002, .00003, 87.0, 525.0, 5.0, .00007);
            Mitarbeiter mitarbeiterAP = new Mitarbeiter(570, 500, 400, 150);
            Processingdaten processingdaten = new Processingdaten(.5, 10.02, .05, 1.0, .03, .5, .05, .04, .02);

            // Test der Plausibilitätsprüfungen

            PL(); ProvokeException(() => Berechnung.Processingtabelle(null, eAP, mitarbeiterAP, processingdaten));
            PL(); ProvokeException(() => Berechnung.Processingtabelle(eVP, null, mitarbeiterAP, processingdaten));
            PL(); ProvokeException(() => Berechnung.Processingtabelle(eVP, eAP, null, processingdaten));
            PL(); ProvokeException(() => Berechnung.Processingtabelle(eVP, eAP, mitarbeiterAP, null));

            // Erstellung der Berechnungen

            Processingtabelle processingtabelle2 = Berechnung.Processingtabelle(eVP, eAP, mitarbeiterAP, processingdaten);
            PL(); IsNull(processingtabelle2, BadColor, GoodColor);

            // Prüfung der Berechnungen

            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[0], 154.62, .5, 77.31);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[1], 5.42, 10.02, 54.31);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[2], 585.0, .05, 29.25);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[3], 87.0, 1.0, 87.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[4], 1470.0, .03, 44.1);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[5], 150.0, .5, 75.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[6], 525.0, .05, 26.25);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[7], 5.0, .04, .2);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[8], 15.0, .02, .3);
            PL(); CompareAndPrintDoubleTolerance(processingtabelle2.BenötigteBackofficeMitarbeiter, 393.72, tolerance: 1E-2);

            Console.WriteLine("\n\n--- Methode Berechnung.Auslastungstabelle ---\n");

            Indizes indizesVP = new Indizes(.99, 1.0);
            Indizes indizesAP = new Indizes(1.0, .99);

            // Test der Plausibilitätsprüfungen

            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(0.0, 393.35, indizesVP, indizesAP, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(-10.0, 393.35, indizesVP, indizesAP, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 0.0, indizesVP, indizesAP, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, -10.0, indizesVP, indizesAP, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, null, indizesAP, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, null, 400.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, indizesAP, 0.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, indizesAP, -10.0, 400.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, indizesAP, 400.0, 0.0));
            PL(); ProvokeException(() => Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, indizesAP, 400.0, -10.0));

            // Erstellen der Berechnungen

            Auslastungstabelle auslastungstabelle2 = Berechnung.Auslastungstabelle(393.58, 393.35, indizesVP, indizesAP, 400.0, 400.0);
            PL(); IsNull(auslastungstabelle2, BadColor, GoodColor);

            // Prüfung der Berechnungen

            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[0], 393.58, 393.35);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[1], 397.56, 397.32);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[2], 400.0, 400.0);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[3], .99, .99);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[4], .99, 1.0);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[5], 1.0, .99);

            Console.WriteLine("\n\n--- Methode Berechnung.BerechneTabellen ---\n");

            // Test der Plausibilitätsprüfungen

            PL(); ProvokeException(() => Berechnung.BerechneTabellen(0, out _, out _));
            PL(); ProvokeException(() => Berechnung.BerechneTabellen(1, out _, out _));
            PL(); ProvokeException(() => Berechnung.BerechneTabellen(2, out _, out _));
            PL(); ProvokeException(() => Berechnung.BerechneTabellen(4, out _, out _));

            // Erstellen der Berechnungen

            Berechnung.BerechneTabellen(3, out processingtabelle2, out auslastungstabelle2);
            PL(); IsNull(processingtabelle2, BadColor, GoodColor);
            PL(); IsNull(auslastungstabelle2, BadColor, GoodColor);

            // Prüfung der Berechnungen

            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[0], 2.0, 1.0, 2.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[1], 2.0, 1.0, 2.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[2], 2.0, 1.0, 2.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[3], 3.0, 1.0, 3.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[4], 9.0, 1.0, 9.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[5], 3.0, 1.0, 3.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[6], 3.0, 1.0, 3.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[7], 3.0, 1.0, 3.0);
            PL(); VergleicheProcessingZeile(processingtabelle2.Tabelle[8], 2.0, 1.0, 2.0);
            PL(); CompareAndPrintDoubleTolerance(processingtabelle2.BenötigteBackofficeMitarbeiter, 29.0, tolerance: 1E-2);

            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[0], 18.0, 29.0);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[1], 4.5, 3.22222222223);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[2], 2.0, 3.0);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[3], 2.25, 1.07407407407);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[4], 2.0, 3.0);
            PL(); VergleicheAuslastungsZeile(auslastungstabelle2.Tabelle[5], 2.0, 3.0);

            Console.WriteLine("\n\n--- ERGEBNIS ---\n");

            PrintResult();

            Console.ReadKey();
        }
    }
}

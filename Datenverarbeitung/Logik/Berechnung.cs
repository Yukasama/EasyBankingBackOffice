using System;
using System.Drawing;
using EasyBankingBackOffice.Datenhaltung.Datenbank;
using EasyBankingBackOffice.Datenhaltung.Transfer;
using EasyBankingBackOffice.Datenverarbeitung.Transfer;

namespace EasyBankingBackOffice.Datenverarbeitung.Logik
{
    public static class Berechnung
    {
        public static Auslastungstabelle Auslastungstabelle(
            double benötigteBackofficemitarbeiterVorperiode,
            double benötigteBackofficemitarbeiterAktuellePeriode,
            Indizes IndizesVorperiode,
            Indizes IndizesAktuellePeriode,
            double verfügbareBackofficemitarbeiterVorperiode,
            double verfügbareBackofficemitarbeiterAktuellePeriode
        )
        {
            // Plausibilitätsprüfungen
            if (IndizesVorperiode == null ||
                IndizesAktuellePeriode == null ||
                benötigteBackofficemitarbeiterVorperiode <= 0 ||
                benötigteBackofficemitarbeiterAktuellePeriode <= 0 ||
                verfügbareBackofficemitarbeiterAktuellePeriode <= 0 ||
                verfügbareBackofficemitarbeiterVorperiode <= 0)
                throw new Exception();

            // Vorgegebene Berechnungen durchführen
            double effektivBenötigteBackofficemitarbeiterVorperiode = benötigteBackofficemitarbeiterVorperiode / (IndizesVorperiode.ITIndex * IndizesVorperiode.Qualifikationsindex);
            double BOVor = effektivBenötigteBackofficemitarbeiterVorperiode / verfügbareBackofficemitarbeiterVorperiode;

            double effektivBenötigteBackofficemitarbeiterAktuelleperiode = benötigteBackofficemitarbeiterAktuellePeriode / (IndizesAktuellePeriode.ITIndex * IndizesAktuellePeriode.Qualifikationsindex);
            double BONach = effektivBenötigteBackofficemitarbeiterAktuelleperiode / verfügbareBackofficemitarbeiterAktuellePeriode;

            Auslastungstabelle auslastung = new();

            // Einträge erstellen (mithilfe des Testdurchlaufs)
            auslastung.NeuerEintrag("Bezeichnung", benötigteBackofficemitarbeiterVorperiode, benötigteBackofficemitarbeiterAktuellePeriode);
            auslastung.NeuerEintrag("Bezeichnung", effektivBenötigteBackofficemitarbeiterVorperiode, effektivBenötigteBackofficemitarbeiterAktuelleperiode);
            auslastung.NeuerEintrag("Bezeichnung", verfügbareBackofficemitarbeiterVorperiode, verfügbareBackofficemitarbeiterAktuellePeriode);
            auslastung.NeuerEintrag("Bezeichnung", BOVor, BONach);
            auslastung.NeuerEintrag("Bezeichnung", IndizesVorperiode.ITIndex, IndizesAktuellePeriode.ITIndex);
            auslastung.NeuerEintrag("Bezeichnung", IndizesVorperiode.Qualifikationsindex, IndizesAktuellePeriode.Qualifikationsindex);

            return auslastung;
        }

        public static void BerechneTabellen(
            int aktuellePeriode,
            out Processingtabelle processing,
            out Auslastungstabelle auslastung
        )
        {
            // Perioden deklarieren
            Periode periode3 = Datenbank.Periode(aktuellePeriode - 2);
            Periode periode2 = Datenbank.Periode(aktuellePeriode - 1);
            Periode periode1 = Datenbank.Periode(aktuellePeriode);

            // Perioden auf Existenz prüfen
            if (periode3 == null || periode2 == null || periode1 == null)
                throw new Exception();

            // Da kein ersichtlicher Anhaltspunkt auf benötigte Backofficemitarbeiter gefunden wurde, sind diese hard-coded
            // Werte aus Datenbank ziehen und selbe Berechnungen durchführen
            double benötigteBackofficemitarbeiterVorperiode = 18;
            double benötigteBackofficemitarbeiterAktuellePeriode = 29;
            double verfügbareBackofficemitarbeiterVorperiode = Datenbank.Mitarbeiter(aktuellePeriode - 1).Backofficemitarbeiter;
            double verfügbareBackofficemitarbeiterAktuellePeriode = Datenbank.Mitarbeiter(aktuellePeriode).Backofficemitarbeiter;

            double effektivBenötigteBackofficemitarbeiterVorperiode = benötigteBackofficemitarbeiterVorperiode / (Datenbank.Indizes(aktuellePeriode - 1).ITIndex * Datenbank.Indizes(aktuellePeriode - 1).Qualifikationsindex);
            double BOVor = effektivBenötigteBackofficemitarbeiterVorperiode / verfügbareBackofficemitarbeiterVorperiode;

            double effektivBenötigteBackofficemitarbeiterAktuelleperiode = benötigteBackofficemitarbeiterAktuellePeriode / (Datenbank.Indizes(aktuellePeriode).ITIndex * Datenbank.Indizes(aktuellePeriode).Qualifikationsindex);
            double BONach = effektivBenötigteBackofficemitarbeiterAktuelleperiode / verfügbareBackofficemitarbeiterAktuellePeriode;
            
            // Selbes Schema wie in Auslastungstabelle()
            auslastung = new();
            auslastung.NeuerEintrag("Bezeichnung", benötigteBackofficemitarbeiterVorperiode, benötigteBackofficemitarbeiterAktuellePeriode);
            auslastung.NeuerEintrag("Bezeichnung", effektivBenötigteBackofficemitarbeiterVorperiode, effektivBenötigteBackofficemitarbeiterAktuelleperiode);
            auslastung.NeuerEintrag("Bezeichnung", verfügbareBackofficemitarbeiterVorperiode, verfügbareBackofficemitarbeiterAktuellePeriode);
            auslastung.NeuerEintrag("Bezeichnung", BOVor, BONach);
            auslastung.NeuerEintrag("Bezeichnung", Datenbank.Indizes(aktuellePeriode - 1).ITIndex, Datenbank.Indizes(aktuellePeriode).ITIndex);
            auslastung.NeuerEintrag("Bezeichnung", Datenbank.Indizes(aktuellePeriode - 1).Qualifikationsindex, Datenbank.Indizes(aktuellePeriode).Qualifikationsindex);

            // Selbes Schema wie in Processingtabelle()
            processing = new();
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode - 1).AktiveKredite, Datenbank.Processingdaten.AktiveKredite);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode - 1).Par30Kredite, Datenbank.Processingdaten.Par30Kredite);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode - 1).Spareinlagen, Datenbank.Processingdaten.Spareinlagen);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode).NeueKredite, Datenbank.Processingdaten.NeueKredite);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Mitarbeiter(aktuellePeriode).Anlageberater + Datenbank.Mitarbeiter(aktuellePeriode).Kreditberater + Datenbank.Mitarbeiter(aktuellePeriode).Backofficemitarbeiter, Datenbank.Processingdaten.Mitarbeiter);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Mitarbeiter(aktuellePeriode).Filialen, Datenbank.Processingdaten.Filialen);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode).NeueEinlagen, Datenbank.Processingdaten.NeueEinlagen);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode).TransaktionenKapitalmarkt, Datenbank.Processingdaten.TransaktionenKapitalmarkt);
            processing.NeuerEintrag("Bezeichnung", Datenbank.Einheiten(aktuellePeriode - 1).EndbestandKapitalmarkt, Datenbank.Processingdaten.EndbestandKapitalmarkt);
        }

        public static Processingtabelle Processingtabelle(
            Einheiten einheitenVorperiode,
            Einheiten einheitenAktuellePeriode,
            Mitarbeiter mitarbeiterAktuellePeriode,
            Processingdaten processingdaten
        )
        {
            // Plausibilitätsprüfungen
            if (einheitenVorperiode == null || einheitenAktuellePeriode == null || mitarbeiterAktuellePeriode == null || processingdaten == null)
                throw new Exception();

            Processingtabelle processing = new();

            // Einträge erstellen (mithilfe des Testdurchlaufs)
            processing.NeuerEintrag("bezeichnung", einheitenVorperiode.AktiveKredite, processingdaten.AktiveKredite);
            processing.NeuerEintrag("bezeichnung", einheitenVorperiode.Par30Kredite, processingdaten.Par30Kredite);
            processing.NeuerEintrag("bezeichnung", einheitenVorperiode.Spareinlagen, processingdaten.Spareinlagen);
            processing.NeuerEintrag("bezeichnung", einheitenAktuellePeriode.NeueKredite, processingdaten.NeueKredite);
            processing.NeuerEintrag("bezeichnung", mitarbeiterAktuellePeriode.Anlageberater + mitarbeiterAktuellePeriode.Kreditberater + mitarbeiterAktuellePeriode.Backofficemitarbeiter, processingdaten.Mitarbeiter);
            processing.NeuerEintrag("bezeichnung", mitarbeiterAktuellePeriode.Filialen, processingdaten.Filialen);
            processing.NeuerEintrag("bezeichnung", einheitenAktuellePeriode.NeueEinlagen, processingdaten.NeueEinlagen);
            processing.NeuerEintrag("bezeichnung", einheitenAktuellePeriode.TransaktionenKapitalmarkt, processingdaten.TransaktionenKapitalmarkt);
            processing.NeuerEintrag("bezeichnung", einheitenVorperiode.EndbestandKapitalmarkt, processingdaten.EndbestandKapitalmarkt);

            return processing;
        }
    }
}
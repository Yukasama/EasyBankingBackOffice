using System;
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
            if (IndizesVorperiode == null ||
                IndizesAktuellePeriode == null ||
                benötigteBackofficemitarbeiterVorperiode < 0 ||
                benötigteBackofficemitarbeiterAktuellePeriode < 0 ||
                verfügbareBackofficemitarbeiterAktuellePeriode < 0 ||
                verfügbareBackofficemitarbeiterVorperiode < 0)
                throw new Exception();

            double effektivBenötigteBackofficemitarbeiterVorperiode = benötigteBackofficemitarbeiterVorperiode / (IndizesVorperiode.ITIndex * IndizesVorperiode.Qualifikationsindex);
            double BOVor = effektivBenötigteBackofficemitarbeiterVorperiode / verfügbareBackofficemitarbeiterVorperiode;
            double result1 = BOVor - effektivBenötigteBackofficemitarbeiterVorperiode;

            double effektivBenötigteBackofficemitarbeiterAktuelleperiode = benötigteBackofficemitarbeiterAktuellePeriode / (IndizesAktuellePeriode.ITIndex * IndizesAktuellePeriode.Qualifikationsindex);
            double BONach = effektivBenötigteBackofficemitarbeiterAktuelleperiode / verfügbareBackofficemitarbeiterAktuellePeriode;
            double result2 = BONach - effektivBenötigteBackofficemitarbeiterAktuelleperiode;

            Auslastungstabelle aus = new();

            aus.NeuerEintrag("bezeichnung", result1, result2);

            return aus;
        }

        public static void BerechneTabellen(
            int aktuellePeriode,
            out Processingtabelle processing,
            out Auslastungstabelle auslastung
        )
        {
            Periode periode3 = Datenbank.Periode(aktuellePeriode - 2);
            Periode periode2 = Datenbank.Periode(aktuellePeriode - 1);
            Periode periode1 = Datenbank.Periode(aktuellePeriode);

            if (periode3 == null || periode2 == null || periode1 == null)
                throw new Exception();

            auslastung = Auslastungstabelle();
        }

        public static Processingtabelle Processingtabelle(
            Einheiten einheitenVorperiode,
            Einheiten einheitenAktuellePeriode,
            Mitarbeiter mitarbeiterAktuellePeriode,
            Processingdaten processingdaten
        )
        {
            if (einheitenVorperiode == null || einheitenAktuellePeriode == null || mitarbeiterAktuellePeriode == null || processingdaten == null)
                throw new Exception();

            return new Processingtabelle();
        }
    }
}
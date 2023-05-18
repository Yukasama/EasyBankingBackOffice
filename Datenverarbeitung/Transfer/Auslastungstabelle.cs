namespace EasyBankingBackOffice.Datenverarbeitung.Transfer
{
    public class Auslastungstabelle
    {
        // Private Liste, für simple Erweiterung der Tabelle
        private List<Eintrag> _tabelle;

        // Konvertierung zu Array im Getter
        public Eintrag[] Tabelle { get => _tabelle.ToArray(); }

        public Auslastungstabelle()
        {
            // Initialisierung neuer Tabelle
            _tabelle = new List<Eintrag>();
        }

        public void NeuerEintrag(
            string bezeichnung,
            double vorperiode,
            double aktuellePeriode
        )
        {
            // Plausibilitätsprüfungen
            if (vorperiode <= 0 || aktuellePeriode <= 0 || String.IsNullOrWhiteSpace(bezeichnung))
                throw new Exception();

            // Der Tabelle neuen Eintrag hinzufügen (Parameter)
            _tabelle.Add(new Eintrag(bezeichnung, vorperiode, aktuellePeriode));
        }

        public class Eintrag
        {
            // Eigenschaften für Konstruktor
            public double AktuellePeriode { get; }
            public string Bezeichnung { get; }
            public double Vorperiode { get; }

            public Eintrag(
                string bezeichnung,
                double vorperiode,
                double aktuellePeriode
            )
            {
                // Plausibilitätsprüfungen
                if (vorperiode <= 0 || aktuellePeriode <= 0 || String.IsNullOrWhiteSpace(bezeichnung))
                    throw new Exception();

                Bezeichnung = bezeichnung;
                Vorperiode = vorperiode;
                AktuellePeriode = aktuellePeriode;
            }
        }
    }


}
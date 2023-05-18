namespace EasyBankingBackOffice.Datenverarbeitung.Transfer
{
    public class Processingtabelle
    {
        // Private Liste, für simple Erweiterung der Tabelle
        private List<Eintrag> _tabelle;

        public double BenötigteBackofficeMitarbeiter { get; private set; }

        // Konvertierung zu Array im Getter
        public Eintrag[] Tabelle { get => _tabelle.ToArray(); }

        public Processingtabelle()
        {
            // Initialisierung neuer Tabelle
            _tabelle = new List<Eintrag>();
        }

        public void NeuerEintrag(
            string bezeichnung,
            double einheiten,
            double mitarbeiterProEinheit
        )
        {
            // Plausibilitätsprüfungen
            if (einheiten <= 0 || mitarbeiterProEinheit <= 0 || String.IsNullOrWhiteSpace(bezeichnung))
                throw new Exception();

            // Benötigte Mitarbeiter durch Multiplikation der Parameter ausrechnen
            double benötigteMitarbeiter = einheiten * mitarbeiterProEinheit;

            // Eigenschaft BenötigteBackofficeMitarbeiter um benötigteMitarbeiter erhöhen
            BenötigteBackofficeMitarbeiter += benötigteMitarbeiter;

            // Der Tabelle neuen Eintrag hinzufügen (Parameter)
            _tabelle.Add(new Eintrag(bezeichnung, einheiten, mitarbeiterProEinheit, benötigteMitarbeiter));
        }

        public class Eintrag
        {
            // Eigenschaften für Konstruktor
            public double BenötigteMitarbeiter { get; }
            public string Bezeichnung { get; }
            public double EinheitenAP { get; }
            public double MitarbeiterProEinheit { get; }

            public Eintrag(
                string bezeichnung,
                double einheitenAP,
                double mitarbeiterProEinheit,
                double benötigteMitarbeiter
            )
            {
                // Plausibilitätsprüfungen
                if (String.IsNullOrWhiteSpace(bezeichnung) || einheitenAP <= 0 || mitarbeiterProEinheit <= 0 || benötigteMitarbeiter <= 0)
                    throw new Exception();

                BenötigteMitarbeiter = benötigteMitarbeiter;
                Bezeichnung = bezeichnung;
                EinheitenAP = einheitenAP;
                MitarbeiterProEinheit = mitarbeiterProEinheit;
            }
        }

    }
}
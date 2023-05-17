namespace EasyBankingBackOffice.Datenverarbeitung.Transfer
{
    public class Processingtabelle
    {
        private List<Eintrag> _tabelle;
        public double BenötigteBackofficeMitarbeiter { get; }
        public Eintrag[] Tabelle { get => _tabelle.ToArray(); }

        public Processingtabelle()
        {
            _tabelle = new List<Eintrag>();
        }

        public void NeuerEintrag(
            string bezeichnung,
            double einheiten,
            double mitarbeiterProEinheit
        )
        {
            if (einheiten < 0 || mitarbeiterProEinheit < 0 || String.IsNullOrWhiteSpace(bezeichnung))
                throw new Exception();

            _tabelle.Add(new Eintrag(bezeichnung, einheiten, mitarbeiterProEinheit, BenötigteBackofficeMitarbeiter));
        }

        public class Eintrag
        {
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
                if (String.IsNullOrWhiteSpace(bezeichnung) || einheitenAP < 0 || mitarbeiterProEinheit < 0 || benötigteMitarbeiter < 0)
                    throw new Exception();

                BenötigteMitarbeiter = benötigteMitarbeiter;
                Bezeichnung = bezeichnung;
                EinheitenAP = einheitenAP;
                MitarbeiterProEinheit = mitarbeiterProEinheit;
            }
        }

    }
}
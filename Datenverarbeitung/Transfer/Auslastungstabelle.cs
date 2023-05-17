namespace EasyBankingBackOffice.Datenverarbeitung.Transfer
{
    public class Auslastungstabelle
    {
        public Eintrag[] Tabelle { get; }

        public Auslastungstabelle()
        {
            Tabelle = new Eintrag[] { };
        }

        public void NeuerEintrag(
            string bezeichnung,
            double vorperiode,
            double aktuellePeriode
        )
        {
            if (vorperiode < 0 || aktuellePeriode < 0 || String.IsNullOrWhiteSpace(bezeichnung))
                throw new Exception();
        }

        public class Eintrag
        {
            public double AktuellePeriode { get; }
            public string Bezeichnung { get; }
            public double Vorperiode { get; }

            public Eintrag(
                string bezeichnung,
                double vorperiode,
                double aktuellePeriode
            )
            {
                if (vorperiode < 0 || aktuellePeriode < 0 || String.IsNullOrWhiteSpace(bezeichnung))
                    throw new Exception();
            }
        }
    }


}
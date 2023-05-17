// Datenbank_DUMMY.cs (zu Zulassungsaufgabe 23S)

using System;

using EasyBankingBackOffice.Datenhaltung.Transfer;

namespace EasyBankingBackOffice.Datenhaltung.Datenbank
{
    /// <summary>
    /// Dies ist ein Dummy für die statische Klasse zum gekapselten Zugriff auf die Datenbank.
    /// </summary>
    public static class Datenbank
    {
        public static bool IstGeladen { get; private set; } = false;

        public static int[] PeriodenIDs => new int[] { 1, 2, 3 };

        public static Processingdaten Processingdaten => new Processingdaten(1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0);

        public static void DatenbankAuslesen(string pfadZurDatenbank) => IstGeladen = true;

        public static Periode Periode(int periodenID)
        {
            switch (periodenID)
            {
                case 1:
                    return new Periode(1, new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
                case 2:
                    return new Periode(2, new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
                case 3:
                    return new Periode(3, new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
                default:
                    throw new Exception();
            }
        }

        public static Einheiten Einheiten(int periodenID)
        {
            switch (periodenID)
            {
                case 1:
                    return new Einheiten(1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0);
                case 2:
                    return new Einheiten(2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0);
                case 3:
                    return new Einheiten(3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0);
                default:
                    throw new Exception();
            }
        }

        public static Indizes Indizes(int periodenID)
        {
            switch(periodenID)
            {
                case 1:
                    return new Indizes(1.0, 1.0);
                case 2:
                    return new Indizes(2.0, 2.0);
                case 3:
                    return new Indizes(3.0, 3.0);
                default:
                    throw new Exception();
            }
        }

        public static Mitarbeiter Mitarbeiter(int periodenID)
        {
            switch(periodenID)
            {
                case 1:
                    return new Mitarbeiter(1, 1, 1, 1);
                case 2:
                    return new Mitarbeiter(2, 2, 2, 2);
                case 3:
                    return new Mitarbeiter(3, 3, 3, 3);
                default:
                    throw new Exception();
            }
        }
    }
}

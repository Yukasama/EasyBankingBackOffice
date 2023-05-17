// Einheiten_DUMMY.cs (zu Zulassungsaufgabe 23S)

namespace EasyBankingBackOffice.Datenhaltung.Transfer
{
    public record Einheiten(double AktiveKredite,
                            double Par30Kredite,
                            double Spareinlagen,
                            double NeueKredite,
                            double NeueEinlagen,
                            double TransaktionenKapitalmarkt,
                            double EndbestandKapitalmarkt);
}
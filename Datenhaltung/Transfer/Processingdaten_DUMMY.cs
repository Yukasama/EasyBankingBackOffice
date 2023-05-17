// Processingdaten_DUMMY.cs (zu Zulassungsaufgabe 23S)

namespace EasyBankingBackOffice.Datenhaltung.Transfer
{
    public record Processingdaten(double AktiveKredite,
                                  double Par30Kredite,
                                  double Spareinlagen,
                                  double NeueKredite,
                                  double Mitarbeiter,
                                  double Filialen,
                                  double NeueEinlagen,
                                  double TransaktionenKapitalmarkt,
                                  double EndbestandKapitalmarkt);
}
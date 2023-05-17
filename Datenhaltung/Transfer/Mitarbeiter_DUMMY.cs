// Mitarbeiter_DUMMY.cs (zu Zulassungsaufgabe 23S)

namespace EasyBankingBackOffice.Datenhaltung.Transfer
{
    public record Mitarbeiter(int Kreditberater,
                              int Anlageberater,
                              int Backofficemitarbeiter,
                              int Filialen);
}
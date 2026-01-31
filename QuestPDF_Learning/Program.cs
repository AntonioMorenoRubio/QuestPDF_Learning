using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF_Learning.Ejercicio1_TiquetCompra;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

Ejercicio1_TicketCompra();

void Ejercicio1_TicketCompra()
{
    TicketModel ticket = TicketDataSource.GenerateTicketData();
    TicketDocument ticketDocument = new(ticket);

    Document.Create(ticketDocument.Compose)
        .ShowInCompanion();
}
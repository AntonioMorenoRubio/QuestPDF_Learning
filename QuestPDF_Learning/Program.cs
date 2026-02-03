using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF_Learning.Ejercicio1_TiquetCompra;
using QuestPDF_Learning.Ejercicio2_HojaMonstruo;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

// Ejercicio1_TicketCompra();
Ejercicio2_PaginaMonstruo();

void Ejercicio1_TicketCompra()
{
    TicketModel ticket = TicketDataSource.GenerateTicketData();
    TicketDocument ticketDocument = new(ticket);

    Document.Create(ticketDocument.Compose)
        .ShowInCompanion();
}

void Ejercicio2_PaginaMonstruo()
{
    MonstruoModel monstruo = MonstruoDataSource.ObtenerAbaloth();
    MonstruoDocument monstruoDocument = new(monstruo);

    Document.Create(monstruoDocument.Compose)
        .ShowInCompanion();
}
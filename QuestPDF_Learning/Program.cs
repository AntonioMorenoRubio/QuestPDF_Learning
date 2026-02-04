using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF_Learning.Ejercicio1_TiquetCompra;
using QuestPDF_Learning.Ejercicio2_HojaMonstruo;
using QuestPDF_Learning.Ejercicio3_BestiarioGURPS;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

// Ejercicio1_TicketCompra();
// Ejercicio2_PaginaMonstruo();
// Ejercicio3_MonstruoGurps();
Ejercicio4_BestiarioGurps();

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

void Ejercicio3_MonstruoGurps()
{
    // Desde archivo
    var grifo = MarkdownCriaturaGURPSParser.ParsearDesdeArchivo("CriaturasGURPS/Grifo.md");

    CriaturaGURPSDocument gurpsDocument = new(grifo);

    Document.Create(gurpsDocument.Compose)
        .ShowInCompanion();
}

void Ejercicio4_BestiarioGurps()
{
    string[] archivos = Directory.GetFiles("CriaturasGURPS", "*.md");
    List<CriaturaGURPS> criaturas = new List<CriaturaGURPS>();

    foreach (var archivo in archivos)
    {
        var criatura = MarkdownCriaturaGURPSParser.ParsearDesdeArchivo(archivo);
        criaturas.Add(criatura);
    }

    List<CriaturaGURPSDocument> documents = new();
    foreach (var criatura in criaturas)
    {
        documents.Add(new CriaturaGURPSDocument(criatura));
    }

    Document.Merge(documents).GeneratePdfAndShow();
}
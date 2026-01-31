using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF_Learning.Ejercicio1_TiquetCompra;

public class TicketDocument : IDocument
{
    public TicketModel Ticket { get; }

    public TicketDocument(TicketModel ticket)
    {
        Ticket = ticket;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(80, 100, Unit.Millimetre);
            page.Margin(10);


            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    public void ComposeHeader(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(columna =>
                {
                    columna.Item()
                    .AlignCenter()
                    .Text("=============================")
                    .SemiBold();

                    columna.Item()
                    .AlignCenter()
                    .Text(Ticket.NombreTienda)
                    .SemiBold();

                    columna.Item()
                    .AlignCenter()
                    .Text("=============================")
                    .SemiBold();
                });
            });

            col.Item().Row(row =>
            {
                row.RelativeItem()
                    .AlignLeft()
                    .Text($"Fecha: {Ticket.Fecha.Day}/{Ticket.Fecha.Month:D2}/{Ticket.Fecha.Year}")
                    .FontSize(11)
                    .SemiBold();

                row.RelativeItem()
                    .AlignRight()
                    .Text("Ticket: " + Ticket.NumeroTicket)
                    .FontSize(11)
                    .SemiBold();
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Text("----------------------------------------------");

            col.Item().Row(row =>
            {
                row.RelativeItem().Text("Producto").AlignLeft();
                row.RelativeItem().Text("Cant.").AlignCenter();
                row.RelativeItem().Text("Precio").AlignEnd();
            });

            col.Item().Text("----------------------------------------------");

            foreach (LineaProducto linea in Ticket.Productos)
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem().Text($"{linea.Nombre}").AlignLeft();
                    if (linea.Unidad is not "ud")
                        row.RelativeItem().Text($"{linea.Cantidad}{linea.Unidad}").AlignCenter();
                    else
                        row.RelativeItem().Text($"{linea.Cantidad}").AlignCenter();

                    row.RelativeItem().Text($"{(linea.PrecioTotal * linea.Cantidad):c2}").AlignEnd();
                });
            }

            col.Item().Text("----------------------------------------------");
        });
    }

    private void ComposeFooter(IContainer container)
    {
        decimal precioTotal = Ticket.Productos.Sum(x => x.PrecioTotal * x.Cantidad);
        decimal precioIVA = Ticket.PorcentajeIVA * precioTotal;

        container.Column(col =>
        {
            col.Item()
            .Text($"Subtotal: {precioTotal:c}")
            .AlignEnd()
            .SemiBold();

            col.Item()
            .Text($"IVA ({Ticket.PorcentajeIVA:P0}): {precioIVA:c}")
            .AlignEnd()
            .SemiBold();

            col.Item().Text("----------------------------------------------");

            col.Item()
            .Text($"TOTAL: {(precioTotal + precioIVA):c}")
            .AlignEnd()
            .SemiBold();

            col.Item().Text("");

            col.Item()
            .Text("¡Gracias por su compra!")
            .AlignCenter()
            .SemiBold();
        });
    }
}
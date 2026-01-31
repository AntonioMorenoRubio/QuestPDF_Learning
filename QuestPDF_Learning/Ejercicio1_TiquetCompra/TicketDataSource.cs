namespace QuestPDF_Learning.Ejercicio1_TiquetCompra
{
    public static class TicketDataSource
    {
        private static Random random = new();

        public static TicketModel GenerateTicketData()
        {

            return new TicketModel
            {
                NombreTienda = "SUPERMERCADO PÉREZ",
                Fecha = DateTime.Now,
                NumeroTicket = random.Next(1, 10_000),
                Productos = new List<LineaProducto>
                {
                    new() { Nombre = "Pan integral", Cantidad = 2, Unidad = "ud", PrecioTotal = 2.40m },
                    new() { Nombre = "Leche entera", Cantidad = 1, Unidad = "ud", PrecioTotal = 1.20m },
                    new() { Nombre = "Tomates", Cantidad = 1.5m, Unidad = "kg", PrecioTotal = 3.75m },
                    new() { Nombre = "Aceite oliva", Cantidad = 1, Unidad = "ud", PrecioTotal = 12.50m }
                },
                PorcentajeIVA = 0.21m
            };
        }
    }
}
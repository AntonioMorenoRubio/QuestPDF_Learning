namespace QuestPDF_Learning.Ejercicio1_TiquetCompra
{
    public class TicketModel
    {
        public required string NombreTienda { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroTicket { get; set; }
        public required List<LineaProducto> Productos { get; set; }
        public decimal PorcentajeIVA { get; set; }
    }

    public class LineaProducto
    {
        public required string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public required string Unidad { get; set; } // "ud", "kg", "l", etc.
        public decimal PrecioTotal { get; set; }
    }
}

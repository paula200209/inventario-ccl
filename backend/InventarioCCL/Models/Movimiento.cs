namespace InventarioCCL.Models;

public class Movimiento
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
    public string Tipo { get; set; } = string.Empty; // entrada | salida
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string? Observacion { get; set; }
}

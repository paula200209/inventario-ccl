namespace InventarioCCL.DTOs;

public class MovimientoDto
{
    public int ProductoId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public string? Observacion { get; set; }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioCCL.Data;
using InventarioCCL.DTOs;
using InventarioCCL.Models;

namespace InventarioCCL.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("inventario")]
    public async Task<IActionResult> GetInventario()
    {
        var productos = await _context.Productos
            .OrderByDescending(p => p.Precio)
            .ThenBy(p => p.Nombre)
            .ToListAsync();

        return Ok(productos);
    }

    [HttpPost("movimiento")]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoDto dto)
    {
        if (dto.Cantidad <= 0)
            return BadRequest(new { mensaje = "La cantidad debe ser mayor a 0" });

        if (dto.Tipo != "entrada" && dto.Tipo != "salida")
            return BadRequest(new { mensaje = "El tipo debe ser 'entrada' o 'salida'" });

        var producto = await _context.Productos.FindAsync(dto.ProductoId);
        if (producto == null)
            return NotFound(new { mensaje = "Producto no encontrado" });

        if (dto.Tipo == "salida" && producto.Cantidad < dto.Cantidad)
            return BadRequest(new { mensaje = "Stock insuficiente" });

        producto.Cantidad += dto.Tipo == "entrada" ? dto.Cantidad : -dto.Cantidad;

        var movimiento = new Movimiento
        {
            ProductoId = dto.ProductoId,
            Tipo = dto.Tipo,
            Cantidad = dto.Cantidad,
            Observacion = dto.Observacion,
            Fecha = DateTime.UtcNow
        };

        _context.Movimientos.Add(movimiento);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Movimiento registrado", producto });
    }
}

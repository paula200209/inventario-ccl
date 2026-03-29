import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductosService } from '../../services/productos.service';

@Component({
  selector: 'app-movimiento',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './movimiento.component.html',
  styleUrl: './movimiento.component.scss'
})
export class MovimientoComponent implements OnInit {
  productos: any[] = [];
  productoId: number | null = null;
  tipo = '';
  cantidad: number | null = null;
  observacion = '';
  mensaje = '';
  error = '';
  cargando = false;

  constructor(
    private productosService: ProductosService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productosService.getInventario().subscribe({
      next: (data) => this.productos = data,
      error: () => this.error = 'Error al cargar productos'
    });
  }

  registrar(): void {
    if (!this.productoId || !this.tipo || !this.cantidad) {
      this.error = 'Por favor completa todos los campos obligatorios';
      return;
    }

    if (this.cantidad <= 0) {
      this.error = 'La cantidad debe ser mayor a 0';
      return;
    }

    this.cargando = true;
    this.error = '';
    this.mensaje = '';

    const data = {
      productoId: this.productoId,
      tipo: this.tipo,
      cantidad: this.cantidad,
      observacion: this.observacion
    };

    this.productosService.registrarMovimiento(data).subscribe({
      next: (res) => {
        this.mensaje = `Movimiento registrado. Stock actual: ${res.producto.cantidad}`;
        this.cargando = false;
        this.resetForm();
      },
      error: (err) => {
        this.error = err.error?.mensaje || 'Error al registrar el movimiento';
        this.cargando = false;
      }
    });
  }

  resetForm(): void {
    this.productoId = null;
    this.tipo = '';
    this.cantidad = null;
    this.observacion = '';
  }

  volver(): void {
    this.router.navigate(['/inventario']);
  }
}
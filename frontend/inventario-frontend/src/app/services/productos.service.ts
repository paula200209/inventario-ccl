import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  private apiUrl = 'http://localhost:5000/productos';

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });
  }

  getInventario(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/inventario`, {
      headers: this.getHeaders()
    });
  }

  registrarMovimiento(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/movimiento`, data, {
      headers: this.getHeaders()
    });
  }
}
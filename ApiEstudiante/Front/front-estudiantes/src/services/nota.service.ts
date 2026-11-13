import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Nota } from '../models/nota.model';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class NotaService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Nota`;

  obtenerNotas(): Observable<any> {
    return this.http.get(`${this.apiUrl}/ConsultarNotas`);
  }

  registrarNota(nota: Omit<Nota, 'id'>): Observable<any> {
    return this.http.post(`${this.apiUrl}/RegistrarNota`, nota);
  }

  actualizarNota(nota: Nota): Observable<any> {
    return this.http.put(`${this.apiUrl}/ActualizarNota`, nota);
  }

  eliminarNota(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/EliminarNota/${id}`);
  }
}

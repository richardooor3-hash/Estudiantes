import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profesor } from '../models/profesor.model';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProfesorService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Profesor`;

  obtenerProfesores(): Observable<any> {
    return this.http.get(`${this.apiUrl}/ConsultarProfesores`);
  }

  registrarProfesor(profesor: Omit<Profesor, 'id'>): Observable<any> {
    return this.http.post(`${this.apiUrl}/RegistrarProfesor`, profesor);
  }

  actualizarProfesor(profesor: Profesor): Observable<any> {
    return this.http.put(`${this.apiUrl}/ActualizarProfesor`, profesor);
  }

  eliminarProfesor(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/EliminarProfesor/${id}`);
  }
}

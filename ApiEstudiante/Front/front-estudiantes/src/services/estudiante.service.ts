import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estudiante } from '../models/estudiante.model';
import { ApiResponse } from '../models/api-response.model';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class EstudianteService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Estudiante`;

  obtenerEstudiantes(): Observable<ApiResponse<Estudiante[]>> {
    return this.http.get<ApiResponse<Estudiante[]>>(`${this.apiUrl}/ConsultarEstudiantes`);
  }

  registrarEstudiante(estudiante: Estudiante): Observable<ApiResponse<Estudiante>> {
    return this.http.post<ApiResponse<Estudiante>>(`${this.apiUrl}/RegistrarEstudiante`, estudiante);
  }

  actualizarEstudiante(id: number, estudiante: Estudiante): Observable<ApiResponse<Estudiante>> {
    return this.http.put<ApiResponse<Estudiante>>(`${this.apiUrl}/ActualizarEstudiante/`, estudiante);
  }

  eliminarEstudiante(id: number): Observable<ApiResponse<Estudiante>> {
    return this.http.delete<ApiResponse<Estudiante>>(`${this.apiUrl}/EliminarEstudiante/${id}`);
  }
}

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ProfesorService } from '../../services/profesor.service';
import { Profesor } from '../../models/profesor.model';
import { TablaGenericaComponent, ColumnaTabla } from '../../shared/tabla-generica/tabla-generica.component';

@Component({
  selector: 'app-profesores',
  standalone: true,
  imports: [CommonModule, RouterLink, TablaGenericaComponent],
  templateUrl: './profesores.component.html'
})
export class ProfesoresComponent implements OnInit {
  private profesorService = inject(ProfesorService);
  private router = inject(Router);

  profesores: Profesor[] = [];
  mensajeExito = '';
  mensajeError = '';

  columnasTabla: ColumnaTabla[] = [
    { clave: 'id', titulo: 'ID' },
    { clave: 'nombre', titulo: 'Nombre' }
  ];

  ngOnInit(): void {
    const msg = history.state?.mensajeExito;
    if (msg) {
      this.mostrarExito(msg);
      history.replaceState({}, '');
    }
    this.cargarProfesores();
  }

  cargarProfesores(): void {
    this.profesorService.obtenerProfesores().subscribe({
      next: (resp: any) => this.profesores = resp.model ?? [],
      error: () => this.mostrarError('No se pudieron cargar los datos de los profesores.')
    });
  }

  editar(profesor: Profesor): void {
    this.router.navigate(['/profesores/editar', profesor.id]);
  }

  eliminar(id: number): void {
    this.profesorService.eliminarProfesor(id).subscribe({
      next: (resp: any) => {
        this.mostrarExito(resp?.mensaje || 'Profesor eliminado con éxito.');
        this.cargarProfesores();
      },
      error: (err) => {
        const textoError = err.error?.mensaje || 'No se pudo eliminar el profesor. Intenta de nuevo.';
        this.mostrarError(textoError);
      } 
    });
  }

  private mostrarExito(msg: string): void {
    this.mensajeExito = msg;
    this.mensajeError = '';
    setTimeout(() => this.mensajeExito = '', 3500);
  }

  private mostrarError(msg: string): void {
    this.mensajeError = msg;
    this.mensajeExito = '';
    setTimeout(() => this.mensajeError = '', 4500);
  }
}

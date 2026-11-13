import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
import { Estudiante } from '../../models/estudiante.model';
import { TablaGenericaComponent, ColumnaTabla } from '../../shared/tabla-generica/tabla-generica.component';

@Component({
  selector: 'app-estudiantes',
  standalone: true,
  imports: [CommonModule,RouterLink,ReactiveFormsModule,TablaGenericaComponent],
  templateUrl: './estudiantes.component.html'
})
export class EstudiantesComponent implements OnInit {
  private fb = inject(FormBuilder);
  private estudianteService = inject(EstudianteService);
  private router = inject(Router);

  estudiantes: Estudiante[] = [];
  columnasTabla: ColumnaTabla[] = [
    { clave: 'id', titulo: 'ID' },
    { clave: 'nombre', titulo: 'Nombre' }
  ];
  mensajeExito: string = '';
  mensajeError: string = '';
  editandoId: number | null = null;

  estudianteForm = this.fb.group({
    nombre: ['', Validators.required]
  });

  ngOnInit(): void {
        const mensajeNavegacion = history.state?.mensajeExito;
    if (mensajeNavegacion) {
      this.mostrarExito(mensajeNavegacion);
      history.replaceState({}, '');
    }
    this.cargarEstudiantes();
  }

cargarEstudiantes() {
  this.estudianteService.obtenerEstudiantes().subscribe({
    next: (respuesta: any) => {
      this.estudiantes = respuesta.model ?? [];
    },
    error: () => this.mostrarError('No se pudieron cargar los datos de los estudiantes.')
  });
}


  guardar() {
    if (this.estudianteForm.invalid) return;

    const nombre = this.estudianteForm.value.nombre!;

    if (this.editandoId) {
      this.estudianteService.actualizarEstudiante(this.editandoId, { id: this.editandoId, nombre }).subscribe({
        next: (respuesta: any) => {
          const texto = respuesta?.mensaje || 'Estudiante actualizado con éxito.';
          this.mostrarExito(texto);
          this.cancelarEdicion();
          this.cargarEstudiantes();
        },
         error: (err) => {
          const textoError = err.error?.mensaje || 'No se pudo crear el estudiante. Intenta de nuevo.';
          this.mostrarError(textoError);
        }
      });
    } else {
      this.estudianteService.registrarEstudiante({ nombre }).subscribe({
        next: (respuesta: any) => {
          const texto = respuesta?.mensaje || 'Estudiante creado con éxito.';
          this.mostrarExito(texto);
          this.estudianteForm.reset();
          this.cargarEstudiantes();
        },
        error: (err) => {
          const textoError = err.error?.mensaje || 'No se pudo crear el estudiante. Intenta de nuevo.';
          this.mostrarError(textoError);
        }
      });
    }
  }

  editar(estudiante: Estudiante) {
    this.router.navigate(['/estudiantes/editar', estudiante.id]);
  }

  eliminar(id: number) {
    this.estudianteService.eliminarEstudiante(id).subscribe({
      next: (respuesta: any) => {
        const texto = respuesta?.mensaje || 'Estudiante eliminado con éxito.';
        this.mostrarExito(texto);
        this.cargarEstudiantes();
      },
      error: (err) => {
        const textoError = err.error?.mensaje || 'No se pudo eliminar el estudiante. Intenta de nuevo.';
        this.mostrarError(textoError);
      }
    });
  }

  cancelarEdicion() {
    this.editandoId = null;
    this.estudianteForm.reset();
  }

  private mostrarExito(msg: string) {
    this.mensajeExito = msg;
    this.mensajeError = '';
    setTimeout(() => this.mensajeExito = '', 3500);
  }

  private mostrarError(msg: string) {
    this.mensajeError = msg;
    this.mensajeExito = '';
    setTimeout(() => this.mensajeError = '', 4500);
  }
}
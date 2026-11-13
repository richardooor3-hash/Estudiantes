import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EstudianteService } from '../../services/estudiante.service';
import { Estudiante } from '../../models/estudiante.model';

@Component({
  selector: 'app-estudiante-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './estudiante-form.component.html'
})
export class EstudianteFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private estudianteService = inject(EstudianteService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  estudianteId: number | null = null;
  guardando: boolean = false;
  mensajeError: string = '';

  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.minLength(3)]]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.estudianteId = Number(idParam);
      this.cargarDatosEstudiante(this.estudianteId);
    }
  }

  get esEdicion(): boolean {
    return this.estudianteId !== null;
  }

  private cargarDatosEstudiante(id: number): void {
    this.estudianteService.obtenerEstudiantes().subscribe({
      next: (resp: any) => {
        const lista: Estudiante[] = resp.model ?? [];
        const encontrado = lista.find(e => e.id === id);
        if (encontrado) {
          this.form.patchValue({ nombre: encontrado.nombre });
        } else {
          this.mensajeError = 'No se encontró el estudiante seleccionado.';
        }
      },
      error: () => {
        this.mensajeError = 'No se pudieron cargar los datos del estudiante.';
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid || this.guardando) return;

    this.guardando = true;
    const nombre = this.form.value.nombre!.trim();

    if (this.esEdicion) {
      this.estudianteService.actualizarEstudiante(this.estudianteId!, { id: this.estudianteId!, nombre }).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Estudiante actualizado con éxito.';
          this.router.navigate(['/estudiantes'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo actualizar el estudiante. Intenta de nuevo.';
        }
      });
    } else {
      this.estudianteService.registrarEstudiante({ nombre }).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Estudiante creado con éxito.';
          this.router.navigate(['/estudiantes'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo guardar el estudiante. Intenta de nuevo.';
        }
      });
    }
  }
}
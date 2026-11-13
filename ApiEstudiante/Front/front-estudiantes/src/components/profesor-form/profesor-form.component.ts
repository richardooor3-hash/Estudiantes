import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProfesorService } from '../../services/profesor.service';
import { Profesor } from '../../models/profesor.model';

@Component({
  selector: 'app-profesor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './profesor-form.component.html'
})
export class ProfesorFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private profesorService = inject(ProfesorService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  profesorId: number | null = null;
  guardando = false;
  mensajeError = '';

  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.minLength(3)]]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.profesorId = Number(idParam);
      this.cargarProfesor(this.profesorId);
    }
  }

  get esEdicion(): boolean {
    return this.profesorId !== null;
  }

  private cargarProfesor(id: number): void {
    this.profesorService.obtenerProfesores().subscribe({
      next: (resp: any) => {
        const lista: Profesor[] = resp.model ?? [];
        const encontrado = lista.find(p => p.id === id);
        if (encontrado) {
          this.form.patchValue({ nombre: encontrado.nombre });
        } else {
          this.mensajeError = 'No se encontró el profesor seleccionado.';
        }
      },
      error: () => this.mensajeError = 'No se pudieron cargar los datos del profesor.'
    });
  }

  guardar(): void {
    if (this.form.invalid || this.guardando) return;

    this.guardando = true;
    const nombre = this.form.value.nombre!.trim();

    if (this.esEdicion) {
      this.profesorService.actualizarProfesor({ id: this.profesorId!, nombre }).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Profesor actualizado con éxito.';
          this.router.navigate(['/profesores'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo actualizar el profesor. Intenta de nuevo.';
        }
      });
    } else {
      this.profesorService.registrarProfesor({ nombre }).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Profesor creado con éxito.';
          this.router.navigate(['/profesores'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo guardar el profesor. Intenta de nuevo.';
        }
      });
    }
  }
}

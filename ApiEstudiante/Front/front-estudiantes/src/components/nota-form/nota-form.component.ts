import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotaService } from '../../services/nota.service';
import { ProfesorService } from '../../services/profesor.service';
import { EstudianteService } from '../../services/estudiante.service';
import { Nota } from '../../models/nota.model';
import { Profesor } from '../../models/profesor.model';
import { Estudiante } from '../../models/estudiante.model';

@Component({
  selector: 'app-nota-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './nota-form.component.html'
})
export class NotaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private notaService = inject(NotaService);
  private profesorService = inject(ProfesorService);
  private estudianteService = inject(EstudianteService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  notaId: number | null = null;
  profesores: Profesor[] = [];
  estudiantes: Estudiante[] = [];
  guardando = false;
  mensajeError = '';

  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.minLength(2)]],
    idProfesor: [0, [Validators.required, Validators.min(1)]],
    idEstudiante: [0, [Validators.required, Validators.min(1)]],
    valor: [null as number | null, [Validators.required, Validators.min(0), Validators.max(5)]]
  });

  ngOnInit(): void {
    this.cargarCatalogos();

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.notaId = Number(idParam);
      this.cargarNota(this.notaId);
    }
  }

  get esEdicion(): boolean {
    return this.notaId !== null;
  }

  private cargarCatalogos(): void {
    this.profesorService.obtenerProfesores().subscribe({
      next: (resp: any) => this.profesores = resp.model ?? []
    });
    this.estudianteService.obtenerEstudiantes().subscribe({
      next: (resp: any) => this.estudiantes = resp.model ?? []
    });
  }

  private cargarNota(id: number): void {
    this.notaService.obtenerNotas().subscribe({
      next: (resp: any) => {
        const lista: Nota[] = resp.model ?? [];
        const encontrada = lista.find(n => n.id === id);
        if (encontrada) {
          this.form.patchValue({
            nombre: encontrada.nombre,
            idProfesor: encontrada.idProfesor,
            idEstudiante: encontrada.idEstudiante,
            valor: encontrada.valor
          });
        } else {
          this.mensajeError = 'No se encontró la nota seleccionada.';
        }
      },
      error: () => this.mensajeError = 'No se pudieron cargar los datos de la nota.'
    });
  }

  guardar(): void {
    if (this.form.invalid || this.guardando) return;

    this.guardando = true;
    const payload: Nota = {
      nombre: this.form.value.nombre!.trim(),
      idProfesor: Number(this.form.value.idProfesor),
      idEstudiante: Number(this.form.value.idEstudiante),
      valor: Number(this.form.value.valor)
    };

    if (this.esEdicion) {
      this.notaService.actualizarNota({ ...payload, id: this.notaId! }).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Nota actualizada con éxito.';
          this.router.navigate(['/notas'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo actualizar la nota. Intenta de nuevo.';
        }
      });
    } else {
      this.notaService.registrarNota(payload).subscribe({
        next: (resp: any) => {
          const msg = resp?.mensaje || 'Nota creada con éxito.';
          this.router.navigate(['/notas'], { state: { mensajeExito: msg } });
        },
        error: (err) => {
          this.guardando = false;
          this.mensajeError = err.error?.mensaje || 'No se pudo guardar la nota. Intenta de nuevo.';
        }
      });
    }
  }
}
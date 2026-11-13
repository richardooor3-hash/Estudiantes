import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { NotaService } from '../../services/nota.service';
import { Nota } from '../../models/nota.model';
import { TablaGenericaComponent, ColumnaTabla } from '../../shared/tabla-generica/tabla-generica.component';

@Component({
  selector: 'app-notas',
  standalone: true,
  imports: [CommonModule, RouterLink, TablaGenericaComponent],
  templateUrl: './notas.component.html'
})
export class NotasComponent implements OnInit {
  private notaService = inject(NotaService);
  private router = inject(Router);

  notas: Nota[] = [];
  mensajeExito = '';
  mensajeError = '';

 columnasTabla: ColumnaTabla[] = [
  { clave: 'id', titulo: 'ID' },
  { clave: 'nombre', titulo: 'Concepto' },
  { clave: 'nombreProfesor', titulo: 'Profesor' },
  { clave: 'nombreEstudiante', titulo: 'Estudiante' },
  { clave: 'valor', titulo: 'Valor', esDecimal: true }
];

  ngOnInit(): void {
    const msg = history.state?.mensajeExito;
    if (msg) {
      this.mostrarExito(msg);
      history.replaceState({}, '');
    }
    this.cargarNotas();
  }

  cargarNotas(): void {
    this.notaService.obtenerNotas().subscribe({
      next: (resp: any) => this.notas = resp.model ?? [],
      error: () => this.mostrarError('No se pudieron cargar los datos de las notas.')
    });
  }

  editar(nota: Nota): void {
    this.router.navigate(['/notas/editar', nota.id]);
  }

  eliminar(id: number): void {
    this.notaService.eliminarNota(id).subscribe({
      next: (resp: any) => {
        this.mostrarExito(resp?.mensaje || 'Nota eliminada con éxito.');
        this.cargarNotas();
      },
      error: () => this.mostrarError('No se pudo eliminar la nota. Intenta de nuevo.')
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

import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-formulario-nombre',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './formulario-nombre.component.html'
})
export class FormularioNombreComponent implements OnChanges {
  private fb = inject(FormBuilder);

  @Input() etiqueta: string = 'Nombre';
  @Input() placeholder: string = 'Ingrese el nombre';
  @Input() nombreInicial: string | null = null;
  @Input() modoEdicion: boolean = false;

  @Output() onGuardar = new EventEmitter<string>();
  @Output() onCancelar = new EventEmitter<void>();

  form = this.fb.group({
    nombre: ['', Validators.required]
  });

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['nombreInicial']) {
      if (this.nombreInicial) {
        this.form.patchValue({ nombre: this.nombreInicial });
      } else {
        this.form.reset();
      }
    }
  }

  enviar(): void {
    if (this.form.invalid) return;
    this.onGuardar.emit(this.form.value.nombre!.trim());
    if (!this.modoEdicion) {
      this.form.reset();
    }
  }

  cancelar(): void {
    this.form.reset();
    this.onCancelar.emit();
  }
}

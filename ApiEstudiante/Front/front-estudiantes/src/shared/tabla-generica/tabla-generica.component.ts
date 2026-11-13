import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface ColumnaTabla {
  clave: string;
  titulo: string;
  esDecimal?: boolean;
}

@Component({
  selector: 'app-tabla-generica',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tabla-generica.component.html'
})
export class TablaGenericaComponent implements OnChanges {
  @Input() datos: any[] = [];
  @Input() columnas: ColumnaTabla[] = [];
  @Input() itemsPorPagina: number = 5;

  @Output() onEditar = new EventEmitter<any>();
  @Output() onEliminar = new EventEmitter<number>();

  paginaActual: number = 1;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['datos'] || changes['itemsPorPagina']) {
      if (this.paginaActual > this.totalPaginas && this.totalPaginas > 0) {
        this.paginaActual = this.totalPaginas;
      } else if (this.totalPaginas === 0) {
        this.paginaActual = 1;
      }
    }
  }

  get datosPaginados(): any[] {
    const inicio = (this.paginaActual - 1) * this.itemsPorPagina;
    return this.datos.slice(inicio, inicio + this.itemsPorPagina);
  }

  get totalPaginas(): number {
    return Math.ceil(this.datos.length / this.itemsPorPagina);
  }

  get paginasArray(): number[] {
    return Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
  }

  cambiarPagina(pagina: number): void {
    if (pagina >= 1 && pagina <= this.totalPaginas) {
      this.paginaActual = pagina;
    }
  }
}
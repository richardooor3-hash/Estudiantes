export interface Nota {
  id?: number;
  nombre: string;
  idProfesor: number;
  nombreProfesor?: string;
  idEstudiante: number;
  nombreEstudiante?: string;
  valor: number;
}
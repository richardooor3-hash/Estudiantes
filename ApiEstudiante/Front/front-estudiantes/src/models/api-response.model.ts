export interface ApiResponse<T> {
  status: number;
  descripcion: string;
  mensaje: string;
  model: T;
}
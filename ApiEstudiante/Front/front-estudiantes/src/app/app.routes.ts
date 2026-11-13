import { Routes } from '@angular/router';
import { EstudiantesComponent } from '../components/estudiantes/estudiantes.component';
import { EstudianteFormComponent } from '../components/estudiante-form/estudiante-form.component';
import { ProfesoresComponent } from '../components/profesores/profesores.component';
import { ProfesorFormComponent } from '../components/profesor-form/profesor-form.component';
import { NotasComponent } from '../components/notas/notas.component';
import { NotaFormComponent } from '../components/nota-form/nota-form.component';

export const routes: Routes = [
  { path: 'estudiantes', component: EstudiantesComponent },
  { path: 'estudiantes/nuevo', component: EstudianteFormComponent },
  { path: 'estudiantes/editar/:id', component: EstudianteFormComponent },

  { path: 'profesores', component: ProfesoresComponent },
  { path: 'profesores/nuevo', component: ProfesorFormComponent },
  { path: 'profesores/editar/:id', component: ProfesorFormComponent },

  { path: 'notas', component: NotasComponent },
  { path: 'notas/nuevo', component: NotaFormComponent },
  { path: 'notas/editar/:id', component: NotaFormComponent },

  { path: '', redirectTo: 'estudiantes', pathMatch: 'full' }
];

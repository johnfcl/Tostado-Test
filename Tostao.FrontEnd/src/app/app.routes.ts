// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { DocumentosListComponent } from './components/navbar/documentos-list/documentos-list.component';

export const routes: Routes = [
  { 
    path: 'documentos', 
    component: DocumentosListComponent 
  },
  { 
    path: '', 
    redirectTo: '/documentos', 
    pathMatch: 'full' 
  },
  { 
    path: '**', 
    redirectTo: '/documentos' 
  }
];
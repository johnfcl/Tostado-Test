// src/app/app.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterModule],
  template: `
    <div style="padding: 20px;">
      <h1>Gestión Documental Tostao - Dev</h1>
      <nav style="margin-bottom: 20px;">
        <a routerLink="/documentos" style="margin-right: 15px; text-decoration: none; color: #007bff;">
          📋 Documentos
        </a>
        <a routerLink="/" style="margin-right: 15px; text-decoration: none; color: #007bff;">
          🏠 Inicio
        </a>
      </nav>
      
      <router-outlet></router-outlet>
    </div>
  `
})
export class AppComponent {
  title = 'Gestion Documental Tostao';
}
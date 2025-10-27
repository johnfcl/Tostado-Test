import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DocumentosService, FiltroDocumentos, DocumentoCreateDto } from '../../../services/documento.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-documentos-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './documentos-list.component.html',
  styleUrls: ['./documentos-list.component.scss']
})
export class DocumentosListComponent implements OnInit {
  documentos: any[] = [];
  loading: boolean = false;
  error: string = '';
  success: string = '';
  apiConnected: boolean = false;
  
  searchTerm: string = '';
  selectedTipo: string = '';
  selectedEstado: string = '';
  
  documentoEditando: any = null;
  modoEdicion: boolean = false;
  
  nuevoDocumento: DocumentoCreateDto = {
    titulo: '',
    autor: '',
    tipo: '',
    estado: 'Registrado',
    descripcion: '',
    rutaArchivo: ''
  };

  pageNumber: number = 1;
  pageSize: number = environment.pageSize;
  appName: string = environment.appName;
  enableSearch: boolean = environment.enableSearch;
  
  totalDocumentos: number = 0;
  
  // TIPOS CORREGIDOS - según tu base de datos
  tipos = ['PDF', 'DOC', 'IMG', 'XLS', 'TXT'];
  estados = ['Registrado', 'Pendiente', 'Validado', 'Archivado'];

  constructor(private documentosService: DocumentosService) {}

  ngOnInit() {
    this.checkApiConnection();
  }

  checkApiConnection() {
    this.loading = true;
    this.documentosService.checkHealth().subscribe({
      next: () => {
        this.apiConnected = true;
        this.cargarDocumentos();
      },
      error: (error) => {
        this.apiConnected = false;
        this.error = 'No se puede conectar con la API.';
        this.loading = false;
      }
    });
  }

  cargarDocumentos() {
    this.loading = true;
    this.error = '';
    
    const filtro: FiltroDocumentos = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize
    };
    
    this.documentosService.getDocumentos(filtro).subscribe({
      next: (data: any[]) => {
        this.documentos = data;
        this.totalDocumentos = data.length;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = error.message;
        this.loading = false;
      }
    });
  }

  buscarDocumentos() {
    if (!this.enableSearch) return;
    
    this.loading = true;
    this.error = '';
    
    const filtro: FiltroDocumentos = {
      autor: this.searchTerm,
      tipo: this.selectedTipo,
      estado: this.selectedEstado
    };
    
    this.documentosService.searchDocumentos(filtro).subscribe({
      next: (data: any[]) => {
        this.documentos = data;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = error.message;
        this.loading = false;
      }
    });
  }

  onSearchChange() {
    if (!this.enableSearch) return;
    
    if (this.searchTerm || this.selectedTipo || this.selectedEstado) {
      this.buscarDocumentos();
    } else {
      this.cargarDocumentos();
    }
  }

  loadDocumentos() {
    this.cargarDocumentos();
  }

  crearNuevoDocumento() {
    if (!this.nuevoDocumento.titulo || !this.nuevoDocumento.autor || !this.nuevoDocumento.tipo) {
      this.error = 'Título, autor y tipo son obligatorios';
      return;
    }

    this.loading = true;
    this.error = '';
    
    this.documentosService.crearDocumento(this.nuevoDocumento).subscribe({
      next: () => {
        this.cargarDocumentos();
        this.nuevoDocumento = {
          titulo: '',
          autor: '',
          tipo: '',
          estado: 'Registrado',
          descripcion: '',
          rutaArchivo: ''
        };
        this.success = 'Documento creado correctamente';
        this.loading = false;
        setTimeout(() => this.success = '', 3000);
      },
      error: (error: any) => {
        this.error = error.message;
        this.loading = false;
      }
    });
  }

  editarDocumento(documento: any) {
    this.documentoEditando = { ...documento };
    this.modoEdicion = true;
  }

  cancelarEdicion() {
    this.documentoEditando = null;
    this.modoEdicion = false;
  }

  guardarEdicion() {
    if (!this.documentoEditando) return;

    if (!this.documentoEditando.titulo || !this.documentoEditando.autor || !this.documentoEditando.tipo) {
      this.error = 'Título, autor y tipo son obligatorios';
      return;
    }

    this.loading = true;
    this.error = '';
    
    const documentoActualizado: DocumentoCreateDto = {
      titulo: this.documentoEditando.titulo,
      autor: this.documentoEditando.autor,
      tipo: this.documentoEditando.tipo,
      estado: this.documentoEditando.estado,
      descripcion: this.documentoEditando.descripcion || '',
      rutaArchivo: this.documentoEditando.rutaArchivo || ''
    };

    this.documentosService.actualizarDocumento(this.documentoEditando.id, documentoActualizado).subscribe({
      next: () => {
        this.cargarDocumentos();
        this.documentoEditando = null;
        this.modoEdicion = false;
        this.success = 'Documento actualizado correctamente';
        this.loading = false;
        setTimeout(() => this.success = '', 3000);
      },
      error: (error: any) => {
        this.error = error.message;
        this.loading = false;
      }
    });
  }

  eliminarDocumento(documento: any) {
    if (confirm(`¿Estás seguro de que quieres eliminar el documento "${documento.titulo}"?`)) {
      this.loading = true;
      this.error = '';
      
      this.documentosService.eliminarDocumento(documento.id).subscribe({
        next: () => {
          this.cargarDocumentos();
          this.success = 'Documento eliminado correctamente';
          this.loading = false;
          setTimeout(() => this.success = '', 3000);
        },
        error: (error: any) => {
          this.error = error.message;
          this.loading = false;
        }
      });
    }
  }

  verDetalles(documento: any) {
    // Implementar funcionalidad de ver detalles
    console.log('Ver detalles del documento:', documento);
    // Aquí puedes abrir un modal o navegar a una página de detalles
    alert(`Detalles del documento:\n\nTítulo: ${documento.titulo}\nAutor: ${documento.autor}\nTipo: ${documento.tipo}\nEstado: ${documento.estado}\nDescripción: ${documento.descripcion || 'Sin descripción'}`);
  }

  // ACTUALIZAR LOS ICONOS PARA LOS NUEVOS TIPOS
  getTipoIcon(tipo: string): string {
    const iconos: { [key: string]: string } = {
      'PDF': 'bi bi-file-pdf',
      'DOC': 'bi bi-file-word',
      'IMG': 'bi bi-file-image',
      'XLS': 'bi bi-file-excel',
      'TXT': 'bi bi-file-text'
    };
    return iconos[tipo] || 'bi bi-file';
  }

  getEstadoBadgeClass(estado: string): string {
    const clases: { [key: string]: string } = {
      'Registrado': 'bg-primary',
      'Pendiente': 'bg-warning text-dark',
      'Validado': 'bg-success',
      'Archivado': 'bg-secondary'
    };
    return clases[estado] || 'bg-secondary';
  }

  limpiarFiltros() {
    this.searchTerm = '';
    this.selectedTipo = '';
    this.selectedEstado = '';
    this.cargarDocumentos();
  }
}
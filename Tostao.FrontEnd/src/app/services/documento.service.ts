import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';

export interface FiltroDocumentos {
  autor?: string;
  tipo?: string;
  estado?: string;
  pageNumber?: number;
  pageSize?: number;
}

export interface DocumentoCreateDto {
  titulo: string;
  autor: string;
  tipo: string;
  estado: string;
  descripcion?: string;
  rutaArchivo?: string;
}

export interface DocumentoReadDto {
  id: string;
  titulo: string;
  autor: string;
  tipo: string;
  estado: string;
  fechaRegistro: Date;
  fechaValidacion?: Date;
  rutaArchivo?: string;
  descripcion?: string;
}

@Injectable({
  providedIn: 'root'
})
export class DocumentosService {
  private apiUrl = `${environment.apiUrl}/Documentos`;

  constructor(private http: HttpClient) { }

  getDocumentos(filtro?: FiltroDocumentos): Observable<DocumentoReadDto[]> {
    let params = new HttpParams();
    
    if (filtro) {
      if (filtro.pageNumber) params = params.set('pageNumber', filtro.pageNumber.toString());
      if (filtro.pageSize) params = params.set('pageSize', filtro.pageSize.toString());
    }
    
    return this.http.get<DocumentoReadDto[]>(this.apiUrl, { params })
      .pipe(
        catchError(error => {
          return throwError(() => new Error('No se pudieron cargar los documentos.'));
        })
      );
  }

  searchDocumentos(filtro: FiltroDocumentos): Observable<DocumentoReadDto[]> {
    let params = new HttpParams();
    
    if (filtro.autor) params = params.set('autor', filtro.autor);
    if (filtro.tipo) params = params.set('tipo', filtro.tipo);
    if (filtro.estado) params = params.set('estado', filtro.estado);

    return this.http.get<DocumentoReadDto[]>(`${this.apiUrl}/buscar`, { params })
      .pipe(
        catchError(error => {
          return throwError(() => new Error('Error en la búsqueda de documentos.'));
        })
      );
  }

  getDocumentoById(id: string): Observable<DocumentoReadDto> {
    return this.http.get<DocumentoReadDto>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => {
          return throwError(() => new Error('No se pudo cargar el documento.'));
        })
      );
  }

  crearDocumento(documento: DocumentoCreateDto): Observable<DocumentoReadDto> {
    return this.http.post<DocumentoReadDto>(this.apiUrl, documento)
      .pipe(
        catchError(error => {
          const errorMessage = error.error?.error || error.message || 'No se pudo crear el documento.';
          return throwError(() => new Error(errorMessage));
        })
      );
  }

  actualizarDocumento(id: string, documento: DocumentoCreateDto): Observable<DocumentoReadDto> {
    return this.http.put<DocumentoReadDto>(`${this.apiUrl}/${id}`, documento)
      .pipe(
        catchError(error => {
          const errorMessage = error.error?.error || error.message || 'No se pudo actualizar el documento.';
          return throwError(() => new Error(errorMessage));
        })
      );
  }

  eliminarDocumento(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => {
          return throwError(() => new Error('No se pudo eliminar el documento.'));
        })
      );
  }

  checkHealth(): Observable<boolean> {
    return this.http.get<any>(`${this.apiUrl}/checkHealth`).pipe(
      catchError(error => {
        return throwError(() => new Error('API no disponible.'));
      })
    );
  }
}
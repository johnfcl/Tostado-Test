export interface Documento {
  id: string;
  titulo: string;
  autor: string;
  tipo: string;
  estado: string;
  fechaRegistro: string;
  fechaValidacion?: string;
  rutaArchivo?: string;
}

export interface DocumentoCreate {
  titulo: string;
  autor: string;
  tipo: string;
  rutaArchivo?: string;
}

export interface DocumentoSearchParams {
  autor?: string;
  tipo?: string;
  estado?: string;
  pageNumber?: number;
  pageSize?: number;
}
#  Tostao-Test

## Descripción
Tostao-Test es una aplicación de gestión documental desarrollada como prueba técnica.  
El sistema permite registrar, validar, archivar y auditar documentos dentro de una organización, asegurando trazabilidad completa de los cambios.  
Su arquitectura está basada en .NET 8, Angular 17 y SQL Server 2019, aplicando principios de separación por capas y buenas prácticas de desarrollo.

---

## Estructura del Proyecto
Tostao.DocumentManagement/  
│  
├── src/  
│   ├── Tostao.Api/  
│   ├── Tostao.Application/  
│   ├── Tostao.Domain/  
│   ├── Tostao.Infrastructure/  
│   └── Tostao.Test/  
│  
├── TOSTAO_FRONTEND/ (Angular)  
└── sql/  

---

## Ejecución

### Backend (.NET 8)
1. Ubicarse en la carpeta del API:

2. cd src/Tostao.Api

2. Ejecutar el proyecto:


dotnet run

3. Servidor disponible en:  
**http://localhost:7078**

---

### Frontend (Angular 17+)
1. Ir al directorio del frontend:


cd TOSTAO_FRONTEND

2. Instalar dependencias:


npm install

3. Ejecutar la aplicación:


ng serve -o

4. Aplicación disponible en:  
**http://localhost:4200**

---

## Funcionalidades Principales
CRUD completo de documentos.  
Estados disponibles: **Registrado**, **Pendiente**, **Validado**, **Archivado**.  
Registro de auditoría mediante la tabla **LogCambios**.  
Pruebas unitarias implementadas con **MSTest** y **Moq**.  
Validaciones automáticas, manejo de errores y servicios desacoplados.  

---

## Pruebas Unitarias
Ubicación: `/Tostao.Test/DocumentosControllerTests.cs`

Ejecución:


dotnet test


---

## Requisitos del Entorno
- .NET SDK 8.0 o superior  
- Node.js 18 o superior  
- Angular CLI 17 o superior  
- SQL Server 2019 o superior  

---

## Base de Datos
Nombre: **GetionDocumentalTostaoBD**  
Incluye las tablas:
- Documentos  

El modelo soporta los estados del flujo documental:  
**Registrado**, **Pendiente**, **Validado**, **Archivado**  

---

## Autor
**John Fredy Culma Leal**  
Desarrollador Fullstack  
2025





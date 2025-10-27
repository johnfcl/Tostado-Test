# Tostado-Test
## Estructura del proyecto

Tostao.DocumentManagement/
│
├── src/
│ ├── Tostao.Api/
│ ├── Tostao.Application/
│ ├── Tostao.Domain/
│ └── Tostao.Infrastructure/
│ └── Tostao.Test/
│
├── TOSTAO_FRONTEND/ (Angular)
└── sql/

##  Ejecución

### Backend (.NET 8)
```bash
cd src/Tostao.Api
dotnet run
Servidor API en: http://localhost:7078

#### Frontend (Angular 17+) 
cd TOSTAO_FRONTEND
npm install
ng serve -o
Aplicación en: http://localhost:4200

##### Funcionalidades
•	CRUD de documentos.
•	Estados: Registrado, Pendiente, Validado, Archivado.
•	Auditoría con tabla LogCambios.
•	Pruebas unitarias MSTest + Moq.
•	Validaciones automáticas y manejo de errores.
Tests
Ubicación: /Tostao.Test/DocumentosControllerTests.cs
Ejecución:
dotnet test
###### Requisitos
•	.NET SDK 8.0+
•	Node.js 18+
•	SQL Server 2019+
•	Angular CLI 17+
 Autor
John Fredy Culma Leal


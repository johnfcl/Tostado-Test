# 🟤 Tostao-Test

## Descripción
Tostao-Test es una aplicación de gestión documental desarrollada como prueba técnica.  
El sistema permite registrar, validar, archivar y auditar documentos, garantizando control, trazabilidad y consistencia de los datos.  
Está construido con .NET 8, Angular 17 y SQL Server 2019, siguiendo una arquitectura modular por capas.

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

## 🧩 Instrucciones de Instalación y Configuración

### Requisitos previos
Asegúrate de tener instalados los siguientes componentes:
- .NET SDK 8.0 o superior  
- Node.js 18 o superior  
- Angular CLI 17 o superior  
- SQL Server 2019 o superior  
- Visual Studio 2022 o Visual Studio Code  

---

### Configuración del entorno

1. **Clonar el repositorio**

2.  git clone https://github.com/johnfcl/Tostado-Test.git

cd Tostao-Test


2. **Base de datos**
- Abrir SQL Server Management Studio (SSMS).
- Ejecutar los scripts SQL ubicados en la carpeta `/sql/`.
- Verifica que la base de datos creada se llame:  
  **GetionDocumentalTostaoBD**
- Confirma que existan las tablas: **Documentos** y **LogCambios**.

3. **Configurar la cadena de conexión**
- En el proyecto `Tostao.Api`, abre el archivo `appsettings.json`.
- Ajusta la cadena de conexión:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GetionDocumentalTostaoBD;Trusted_Connection=True;TrustServerCertificate=True;"
  }
  ```

4. **Compilar el proyecto**


dotnet build


---

## 🚀 Ejecución

### Backend (.NET 8)
1. Navegar hasta la carpeta de la API:


cd src/Tostao.Api

2. Ejecutar el servidor:

dotnet run

3. API disponible en:  
**http://localhost:7078**

---

### Frontend (Angular 17+)
1. Ir al directorio del frontend:


cd TOSTAO_FRONTEND

2. Instalar dependencias:


npm install

3. Ejecutar el proyecto:


ng serve -o

4. Aplicación disponible en:  
**http://localhost:4200**

---

## ⚙️ Funcionalidades
Gestión completa de documentos (CRUD).  
Estados del documento: **Registrado**, **Pendiente**, **Validado**, **Archivado**.  
Auditoría completa mediante tabla **LogCambios**.  
Validaciones automáticas y control de errores global.  
Implementación de pruebas unitarias con **MSTest** y **Moq**.  
Servicios desacoplados y arquitectura limpia por capas.

---

## 🧪 Pruebas Unitarias
Ubicación: `/Tostao.Test/DocumentosControllerTests.cs`

Para ejecutar las pruebas:


dotnet test


Las pruebas validan los servicios del controlador de documentos, asegurando que las operaciones CRUD, filtros y validaciones funcionen correctamente.

---

## 🗄️ Base de Datos
**Nombre:** GetionDocumentalTostaoBD  
**Tablas principales:**
- **Documentos**: almacena la información general y el estado de cada documento.  
- **LogCambios**: registra las operaciones de actualización y auditoría.

**Estados válidos del flujo documental:**
Registrado → Pendiente → Validado → Archivado

---

## 🧠 Arquitectura General
El proyecto sigue un enfoque por capas:  
- **Tostao.Api**: Controladores y endpoints REST.  
- **Tostao.Application**: Casos de uso, DTOs y lógica de aplicación.  
- **Tostao.Domain**: Entidades y reglas de negocio.  
- **Tostao.Infrastructure**: Persistencia, contexto de base de datos y repositorios.  
- **Tostao.Test**: Pruebas unitarias automatizadas.

---

## 👤 Autor
**John Fredy Culma Leal**  
Desarrollador Fullstack  
2025

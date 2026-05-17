# inventario prueba de desarrollo

Este proyecto es una solución backend para un sistema de Gestión de Inventario, desarrollado como parte de una prueba técnica. Incluye operaciones CRUD completas para productos y un sistema de autenticación basado en JWT (JSON Web Tokens).

## Tecnologías Utilizadas
- NET 8** (ASP.NET Core Web API
- MySQL / MariaDB** (Persistencia de datos)
- JWT (JSON Web Tokens)** (Seguridad y Roles)

## 🛠️ Configuración y Ejecución

1. Clonar el repositorio https://github.com/marceloVO/inventario.git

2. configurar cadena de string para bd abrir archivo appsettings.json y editar
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=inventario;User=TU_USUARIO;Password=TU_CONTRASENA;"
    }
3. Generar las tablas con el comando dotnet ef database update o crearlas directo de
     CREATE DATABASE Inventario;
      GO
      USE Inventario;
      GO
      
      -- Tabla de Productos
      CREATE TABLE Producto (
          Id INT PRIMARY KEY IDENTITY(1,1),
          Nombre NVARCHAR(100) NOT NULL,
          Descripcion NVARCHAR(255),
          Stock INT NOT NULL DEFAULT 0,
          Precio DECIMAL(18, 2) NOT NULL,
          FechaCreacion DATETIME DEFAULT GETDATE()
      );
      
      
      
      -- Tabla de Usuarios para Autenticación
      CREATE TABLE Usuario (
          Id INT PRIMARY KEY IDENTITY(1,1),
          NombreUsuario NVARCHAR(50) UNIQUE NOT NULL,
          PasswordHash NVARCHAR(MAX) NOT NULL,
          Rol NVARCHAR(20) DEFAULT 'User'
      );
4. luego ejectuar el comando para restaurar los paquetes de NuGet e iniciar los servicios, dotnet restore / dotnet run
5. la api implementa un manejo de asincronismo async/await exponiendo los siguientes endpoints
   Auth:
     POST /api/Auth/login - Autentica al usuario contra la base de datos y genera el token de acceso JWT.
     POST /api/Auth/register - Registra un nuevo usuario aplicando un hashing seguro a la contraseña.
   Products:
     GET /api/Products - Lista de manera asíncrona todos los productos registrados
     POST /api/Products - Crea un nuevo producto en el sistema
     GET /api/Products/{id} - Obtiene el detalle de un producto específico por su ID.
     PUT /api/Products/{id} - Modifica y actualiza los datos de un producto existente.
     DELETE /api/Products/{id} - Realiza la eliminación física del producto especificado.
6. Con la api corriendo en Swagger UI utilizando la url local de la consola
   ej: http://localhost:5152/swagger/index.html donde se tendra que crear un usuario y logear para poder adquirir
   un token valido, el cual se tiene que copiar y en el boton Authorize que tiene un icono de candado pegar el token
   y confirmar, de esta manera se podra utilizar los endpoints de producto.



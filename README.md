# inventario prueba de desarrollo

Este proyecto es una solución backend para un sistema de Gestión de Inventario, desarrollado como parte de una prueba técnica. Incluye operaciones CRUD completas para productos y un sistema de autenticación basado en JWT (JSON Web Tokens).

## Tecnologías Utilizadas
- NET 8** (ASP.NET Core Web API
- MySQL / MariaDB** (Persistencia de datos)
- JWT (JSON Web Tokens)** (Seguridad y Roles)

## 🛠️ Configuración y Ejecución

1. Clonar el repositorio https://github.com/marceloVO/inventario.git

2. configurar cadena de string para bd abrir archivo appsettings.json y editar<br>
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=inventario;User=TU_USUARIO;Password=TU_CONTRASENA;"
    }
3. Generar las tablas con el comando dotnet ef database update o crearlas directo de<br>
     CREATE DATABASE Inventario;
      GO
      USE Inventario;
      GO
      
      -- Tabla de Productos<br>
      CREATE TABLE Producto (<br>
          Id INT PRIMARY KEY IDENTITY(1,1),<br>
          Nombre NVARCHAR(100) NOT NULL,<br>
          Descripcion NVARCHAR(255),<br>
          Stock INT NOT NULL DEFAULT 0,<br>
          Precio DECIMAL(18, 2) NOT NULL,<br>
          FechaCreacion DATETIME DEFAULT GETDATE()<br>
      );
      
      
      
      -- Tabla de Usuarios para Autenticación<br>
      CREATE TABLE Usuario (<br>
          Id INT PRIMARY KEY IDENTITY(1,1),<br>
          NombreUsuario NVARCHAR(50) UNIQUE NOT NULL,<br>
          PasswordHash NVARCHAR(MAX) NOT NULL,<br>
          Rol NVARCHAR(20) DEFAULT 'User'<br>
      );
4. luego ejectuar el comando para restaurar los paquetes de NuGet e iniciar los servicios, dotnet restore / dotnet run
5. la api implementa un manejo de asincronismo async/await exponiendo los siguientes endpoints <br>
   Auth: <br>
     POST /api/Auth/login - Autentica al usuario contra la base de datos y genera el token de acceso JWT.<br>
     POST /api/Auth/register - Registra un nuevo usuario aplicando un hashing seguro a la contraseña.<br>
   Products:<br>
     GET /api/Products - Lista de manera asíncrona todos los productos registrados<br>
     POST /api/Products - Crea un nuevo producto en el sistema<br>
     GET /api/Products/{id} - Obtiene el detalle de un producto específico por su ID.<br>
     PUT /api/Products/{id} - Modifica y actualiza los datos de un producto existente.<br>
     DELETE /api/Products/{id} - Realiza la eliminación física del producto especificado.<br>
6. Con la api corriendo en Swagger UI utilizando la url local de la consola<br>
   ej: http://localhost:5152/swagger/index.html donde se tendra que crear un usuario y logear para poder adquirir
   un token valido, el cual se tiene que copiar y en el boton Authorize que tiene un icono de candado pegar el token
   y confirmar, de esta manera se podra utilizar los endpoints de producto.



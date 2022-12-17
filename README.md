# SistemaDeVentas
POS - Sistema de ventas para pequeñas empresas en Chile.


# Crear modelos de clases, recuerda abrir la consola de administrador de paquetes.

Generar todas las clases en base a la existenncia de la base de datos

- Scaffold-DbContext "Server=.\;Database=SalesSystemDB;Integrated Security=SSPI;Trust Server Certificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Genera clase de una tabla en particular

- Scaffold-DbContext "Server=.\;Database=SalesSystemDB;Integrated Security=SSPI;Trust Server Certificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables "Your-Table-Name" -ContextDir Data -Context "SalesSystemDbContext'
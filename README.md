# Sistema de Gestión de Inventario CCL

Un sistema web completo para la gestión de inventario de pijamas, con control de entrada y salida de stock, autenticación de usuarios segura con JWT y una interfaz moderna y responsiva.


## Requisitos Previos

### Backend
- **.NET 10 SDK** - [Descargar](https://dotnet.microsoft.com/download/dotnet/10.0)
- **PostgreSQL 14+** - [Descargar](https://www.postgresql.org/download/)

### Frontend
- **Node.js 18+** - [Descargar](https://nodejs.org/)
- **Angular CLI** - `npm install -g @angular/cli`

## Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/paula200209/inventario-ccl.git
cd inventario-ccl
```

### 2. Configurar la Base de Datos

```bash
sudo -u postgres psql -c "CREATE DATABASE inventario_ccl;"
sudo cp database_setup.sql /tmp/database_setup.sql
sudo -u postgres psql -d inventario_ccl -f /tmp/database_setup.sql
```

**Credenciales iniciales:**
- Username: `admin`
- Password: `Admin123!`

### 3. Configurar el Backend

```bash
cd backend/InventarioCCL
dotnet restore
```

**Editar appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=inventario_ccl;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "ClaveSecretaMuySeguraParaJWTYProyectoInventarioCCL",
    "Issuer": "InventarioCCL",
    "Audience": "InventarioCCL"
  }
}
```

> Al iniciar, el sistema hashea automáticamente la contraseña del usuario `admin` si aún no está cifrada.

### 4. Configurar el Frontend

```bash
cd frontend/inventario-frontend
npm install
```

## Ejecución

### Backend (desde /backend/InventarioCCL)

```bash
dotnet run
```

Backend estará disponible en: `http://localhost:5082`

### Frontend (desde /frontend/inventario-frontend)

```bash
ng serve
```

Frontend estará disponible en: `http://localhost:4200`

## API Endpoints

### Autenticación
- **POST** `/auth/login` - Iniciar sesión
  ```json
  {
    "username": "admin",
    "password": "Admin123!"
  }
  ```

### Productos
- **GET** `/productos/inventario` - Consultar inventario actual
- **POST** `/productos/movimiento` - Registrar entrada o salida
  ```json
  {
    "productoId": 1,
    "tipo": "entrada",
    "cantidad": 10,
    "observacion": "Ingreso de mercancía nueva"
  }
  ```

> Todos los endpoints de productos requieren el header: `Authorization: Bearer {token}`



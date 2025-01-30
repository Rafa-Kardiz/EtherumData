# Ethereum Data API

**Ethereum Data API** es una API web desarrollada con .NET Core 8 que permite a los usuarios identificar transacciones de Ethereum mediante un hash, así como obtener un listado de todas las transacciones almacenadas en la base de datos.

## Tabla de Contenidos

1. [Clonar el Repositorio y Ejecutar la Aplicación](#clonar-el-repositorio-y-ejecutar-la-aplicación)
2. [Configurar la Base de Datos](#configurar-la-base-de-datos)
3. [Generar y Utilizar el Token JWT o API Key](#generar-y-utilizar-el-token-jwt-o-api-key)
4. [Endpoints y Ejecución](#endpoints-y-ejecución)
5. [Pruebas con Postman](#pruebas-con-postman)

## Clonar el Repositorio y Ejecutar la Aplicación

Sigue estos pasos para clonar el repositorio y ejecutar la aplicación:

1. Clona el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/Rafa-Kardiz/EtherumData.git
   ```

2. Navega al directorio del proyecto:
   ```bash
   cd EtherumData
   ```

3. Restaura las dependencias necesarias:
   ```bash
   dotnet restore
   ```

4. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```

   Al ejecutar la aplicación, se abrirá una página web automáticamente y la URL del navegador será algo similar a `http://localhost:hostdefault`. Esta es la URL que usaremos para realizar las pruebas.

## Configurar la Base de Datos

Dentro de los archivos descargados encontrarás un archivo SQL con el script necesario para crear la base de datos y las tablas requeridas para la aplicación.

### Pasos para Configurar la Base de Datos:

1. **Instalar MySQL**: Si aún no tienes MySQL instalado, puedes descargarlo desde [aquí](https://dev.mysql.com/downloads/installer/).
   
2. Una vez instalado MySQL, puedes utilizar una herramienta como **DBeaver** para conectar y administrar la base de datos.

3. Abre el archivo `etherum_data_sql.sql` con la herramienta de tu elección y ejecuta el script para crear la base de datos.

## Generar y Utilizar el Token JWT o API Key

### Generación del Token JWT

Para obtener un token JWT, realiza una solicitud al endpoint de login:

**POST /api/auth/login**

**Cuerpo de la solicitud:**
```json
{
    "User": "admin",
    "Password": "admin"
}
```

**Respuesta:**
```json
{
    "token": "jwt_token_generado"
}
```

### Uso del Token JWT

Para realizar solicitudes a los endpoints protegidos, añade el token JWT en el encabezado de autorización.

**Ejemplo de solicitud:**

**GET /api/transactions**

**Encabezado:**
```bash
Authorization: Bearer jwt_token_aqui
```

## Endpoints y Ejecución

### 1. Autenticación y Generación de JWT

**POST /api/auth/login**

**Cuerpo de la solicitud:**
```json
{
    "User": "admin",
    "Password": "admin"
}
```

**Respuesta exitosa:**
```json
{
    "token": "jwt_token_generado"
}
```

**Respuesta de error (sin autorización):**
Mandara un error de sin autorización el cual se identifica con el codigo de estatus http 401

### 2. Recuperar Información de una Transacción por Hash

**GET /api/transaction/{hashTransaccion}**

**Respuesta exitosa:**
```json
{
    "id": "0x9c1d06829aa0fa9e94c3e915ce8081498984460e5d8faef84b4d58c9ce1a86fe",
    "value": 0,
    "gasUsed": 500000,
    "blockNumber": 7597594,
    "fromAddress": "0x5912be8eaf246b14cc80ae455d55ad3ca44cd7d6",
    "toAdress": "0x5e2c8ef3035feec3056864512aaf8f4dc88caee3"
}
```

**Respuesta de error (Hash no encontrado):**

    mostrara un error similar a este: "Transacción no encontrada"
    ademas de mandar un estado http 400 de que la petición fallo

    

### 3. Obtener el Listado de Transacciones Guardadas

**GET /api/transactions**

**Encabezado:**
```bash
Authorization: Bearer jwt_token_aqui
```

**Respuesta exitosa:**
```json
[
    {
        "id": "0x9c1d06829aa0fa9e94c3e915ce8081498984460e5d8faef84b4d58c9ce1a86fe",
        "value": 0,
        "gasUsed": 500000,
        "blockNumber": 7597594,
        "fromAddress": "0x5912be8eaf246b14cc80ae455d55ad3ca44cd7d6",
        "toAdress": "0x5e2c8ef3035feec3056864512aaf8f4dc88caee3"
    },
    {
        "id": "0xf4d16b2c939853b8b486c0de06efb5114de2c08cba63940cf926683244b8800b",
        "value": 50000000000000000,
        "gasUsed": 21000,
        "blockNumber": 7597594,
        "fromAddress": "0x52f1984cd3e46e1214db222d3ff63712e7aceedd",
        "toAdress": "0x21c98c69f93e504f4beaad639fc6ca65bbf31bb2"
    }
]
```

**Respuesta de error (Token no proporcionado):**
Mandara un error de sin autorización el cual se identifica con el codigo de estatus http 401

## Pruebas con Postman

Dentro del repositorio encontrarás un archivo llamado **"Pruebas Ethereum Data API.postman_collection.json"**. Este archivo contiene todas las pruebas preconfiguradas para facilitar su ejecución en **Postman**.

### Pasos para realizar las pruebas:

1. **Instalar Postman** si aún no lo tienes.
2. Abre **Postman**.
3. En el menú, haz clic en **File > Import** y selecciona el archivo descargado.
4. También puedes importar el archivo presionando **Ctrl + O**.
5. Una vez importado, encontrarás la colección **Pruebas Ethereum Data API** en la barra lateral derecha de Postman, con todas las pruebas del aplicativo.

### Consideraciones:

- **URL de la aplicación**: Asegúrate de cambiar la URL de las pruebas de `https://localhost:7056` por la URL generada por tu aplicación en ejecución (`http://localhost:{puerto}`).
- **Token de autorización**: En las pruebas relacionadas con el listado de transacciones, asegúrate de generar un token de autenticación usando el endpoint de login y reemplázalo en el encabezado de la solicitud en Postman.

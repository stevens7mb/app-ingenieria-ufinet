# APP INGENIERÍA UFINET




# FUNCIONALIDADES IMPORTANTES

## Creación de modelos a partir de tablas creadas
Ejecutar el comando en la raíz del proyecto

dotnet ef dbcontext scaffold "Server=SQL8020.site4now.net;Database=db_a73217_ufinet2025;User Id=db_a73217_ufinet2025_admin;Password=Corposer%1" Microsoft.EntityFrameworkCore.SqlServer --context DataContext --output-dir Data --context-dir Data --force

Para actualizar tablas en específico
dotnet ef dbcontext scaffold "Server=SQL8020.site4now.net;Database=db_a73217_ufinet2025;User Id=db_a73217_ufinet2025_admin;Password=Corposer%1" Microsoft.EntityFrameworkCore.SqlServer --context DataContext --output-dir Data --context-dir Data --table ServiceDeskTicketFile --table ServiceDeskTicketFileTypeStatus

## 🗺 Importación de polígonos municipales a SQL Server

Este proyecto utiliza polígonos geográficos oficiales de municipios de Guatemala (nivel 2) basados en los datos de GADM.

### 📁 Archivo fuente

Descargar el archivo desde: [https://gadm.org/download_country.html](https://gadm.org/download_country.html)

Ejemplo usado:
```
gadm41_GTM_2.json
```

### 🔧 Requisitos

- [Miniconda](https://docs.conda.io/en/latest/miniconda.html) instalado
- Paquete `gdal` instalado:

  ```bash
  conda install -c conda-forge gdal
  ```

- SQL Server con soporte para tipos espaciales habilitado

---

### 🖥 Comando para importar

```bash
ogr2ogr -f "MSSQLSpatial" ^
  "MSSQL:server=SQL8020.site4now.net;database=db_a73217_ufinet2025;uid=db_a73217_ufinet2025_admin;pwd=Corposer%1" ^
  "C:\Users\smartin.SISTRAN\Downloads\gadm41_GTM_2.json" ^
  -nln MunicipalityShape ^
  -a_srs "EPSG:4326"
```

#### Opcional: Sobrescribir tabla existente

```bash
  ... -overwrite
```

---

### 🔎 Verificar conexión

Puedes probar la conexión con:

```bash
ogrinfo "MSSQL:server=SQL8020.site4now.net;database=db_a73217_ufinet2025;uid=db_a73217_ufinet2025_admin;pwd=Corposer%1"
```

---

### 🗃 Tabla generada

El comando anterior crea una tabla llamada `MunicipalityShape` que incluye geometría en el campo `geom` (tipo `geometry`) y atributos como nombre, código, etc., dependiendo del contenido del archivo JSON.

---

### 📌 Notas

- Se espera que el archivo esté en proyección **WGS 84 (EPSG:4326)**.
- La geometría será almacenada en SQL Server usando el tipo `geometry`.

---

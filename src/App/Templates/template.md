{{header}}
{{status_codes}}
{{for enum_codes}}
{{for endpoints}}
{{for dtos}}

### Códigos de estados
También conocidos como `EntryStatus`
- DRAFT => Borrador
- PUBLISHED => Publicado
- SCHEDULED => Programado para publicarse
- ARCHIVED => Archivado o expirado

### Códigos de tipos archivo
También conocidos como `DocumentTypes`
- EXCEL => Microsoft Excel
- WORD => Microsoft Word
- POWERPOINT => Microsoft Powerpoint
- PDF => Adobe PDF
- IMAGE => Imágenes (varios formatos)

### Códigos de acciones
- to_publish => Publicar uno o varios documentos
- to_schedule => Programar uno o varios documentos
- to_draft => Devolver a estado borrador uno o varios documentos
- to_archive => Archivar uno o varios documentos
- see_interactions => Ver interacciones de un único documento

---
## Endpoints

### Configuración
**[GET]**: `api/documents/company/configuration`
- Opciones y acciones de la pantalla de listado de documentación
- Recibe como parámetros de entrada: Nada
- Devuelve como salida: [DTO](#dto_configuracion_documentos)

---
## DTOs

### DTO_configuracion_documentos
- Status: **StatusDto**[]; // Estados que existen
- Collectives: **CollectiveDto**[]; // Colectivos disponibles activos

#### StatusDto
- Code: string; // Código del estado
- Actions: KeyValuePair<string, string>[]; // Acciones permitidas en ese estado

#### CollectiveDto
- ID: int; // Identificador del colectivo
- Name; string; // Nombre del colectivo
- NumEmployees: int; // Número de empleados en el colectivo

#### Ejemplo
```json
{
  "Status": [{
    "Code": "DRAFT",
    "Actions": [{
      "Key": "to_publish", "Value": "Publicar...",
    }, {
      "Key": "to_schedule", "Value": "Programar...",
    }]
  },{
    "Code": "ARCHIVED",
    "Actions": [{
      "Key": "to_publish", "Value": "Publicar...",
    }]
  }],
  "Collectives": [{
    "ID": 1, "Name": "Colectivo 1", "NumEmployees": 10
  },{
    "ID": 2, "Name": "Colectivo 2", "NumEmployees": 27
  },{
    "ID": 3, "Name": "Colectivo 3", "NumEmployees": 63
  }]
}
```

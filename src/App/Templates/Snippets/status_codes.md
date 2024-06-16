## Común

### Códigos de respuesta de endpoints
- Para operaciones de lectura
    - 200 - OK + DTO de salida correspondiente
    - 400 - BadRequest + Mensaje de error
    - 401 - Unathorized
    - 404 - NotFound + Cuando se solicita un identificador concreto
- Para operaciones de escritura
    - 200 - OK
    - 201 - Created + Identificador o identificadores creados
    - 400 - BadRequest + Mensaje de error
    - 401 - Unathorized
    - 404 - NotFound + En operaciones PUT/POST sobre un identificador

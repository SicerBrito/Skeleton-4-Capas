# Documentación Skeleton-4-Capas
Esqueleto de los proyectos 4 capas de NetCore


## Migraciones
Crear

    dotnet ef migrations add InitialCreate --project ./Persistencia/ --startup-project ./API/ --output-dir ./Data/Migrations/

Actualizar

    dotnet ef database update --project ./Persistencia/ --startup-project ./API/  



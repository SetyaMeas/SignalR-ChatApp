### How to run migration

- locate on root dir

1. Make migrations

```bash
dotnet ef migrations add InitialCreate --startup-project ./src/API/ \
        --project ./src/Infrastructure/ --output-dir "Persistence/Migrations"
```

2. Update Database

```bash
dotnet ef database update --startup-project ./src/API/ --project ./src/Infrastructure/
```

# Web Application - SQL Server + Bootstrap

This is a web application that uses SQL Server for its database and is styled using Bootstrap Lux Theme.

Frontend UI  
- Bootstrap Theme: https://bootswatch.com/lux/  
- Icons: https://icons.getbootstrap.com/

## Database: SQL Server

This project requires a local SQL Server database to function properly.

### Requirements  
1. Install SQL Server  
2. Create a local database

## Configuration

### Update Connection String

1. Open appsettings.json  
2. Find the "DefaultConnection" key  
3. Replace the Server and database connection details with your local configuration.

## Create / Update Database

Using Package Manager Console  
1. Go to Tools > NuGet Package Manager > Package Manager Console  
2. Run the following command:  
    update-database  

This will apply any pending migrations to your database.

## Create New Migration

To generate a new migration based on your model changes:  
1. Open Package Manager Console  
2. Run the following command:  
    add-migration MigrationName  

Example:  
    add-migration FreshAddTables  

## View Applied Migrations

To view applied migrations:  
1. Navigate to:  
    Tables > dbo.__EFMigrationsHistory  
2. Run:  
    SELECT TOP 1000 * FROM __EFMigrationsHistory

## Notes

- Ensure SQL Server service is running.  
- Run Visual Studio as Administrator when applying migrations if you face permission issues.  
- For production environments, consider using SQL authentication instead of Trusted_Connection=True.

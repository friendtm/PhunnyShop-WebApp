README FILE

Template Bootstrap Utilizado: https://bootswatch.com/lux/
Icons: https://icons.getbootstrap.com/


 * #### SQL Server
 * Para a utilização desta Webapp é necessário uma base de dados SQL Server.
 * Download SQL Server e criar uma base de dados local.
 * As informações de conexão à BD são introduzidas em 'appsettings.json' na linha 'DefaultConnection'.

 * #### CRIAR/ATUALIZAR BASE DE DADOS
 * 1º - Tools > Manage NuGet Packages > Console
 * 2º - Na Consola: update-database
 * 
 * #### CRIAR MIGRAÇÃO
 * 1º - Tools > Manage NuGet Packages > Console
 * 2º - Na Consola: add-migration NomeParaMigração (ex: FreshAddTables )
 * 
 * #### MIGRAÇÕES APLICADAS
 * Tables > EFMigrationsHistory > Select Top 1000
 * 
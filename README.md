# WalletApp

This project was created to solve the test task that will help me to get the job of my dream.\
In this application you can manage your cards and transactions.

## What's inside

### Web Api
Web Api project contains all necessary endpoints to use the backend application.\
It can be used if you want to create your own frontend application.

## Installation

### What must be installed
- .NET 7
- .C# 11
- Postgre SQL Server
    - Created database

### How to install and run
1) Download/Clone the solution from repository
    - If download make sure to have the solution directory unarchived
2) Open WalletApp.Startup project in terminal
3) Run command:\
   `dotnet user-secrets init`\
   `dotnet user-secrets set "ConnectionStrings:WalletAppPostgreSqlServer" "<your_connection_string>"`  
   where:
    - `<your_connection_string>` - Connection String to your Postgre SQL Server database
   

   `dotnet user-secrets set "Auth:Token:Secret" "<your_secret_key>"`\
   `dotnet user-secrets set "Auth:Token:Issuer" "<your_issuer>"`\
   `dotnet user-secrets set "Auth:Token:Audience" "<your_audience>"`\
   where:
   - `<your_secret_key>` - Secret jwt key
   - `<your_secret_issuer>` - Issuer
   - `<your_secret_audience>` - Audience

4) Be sure your database is run
5) Run command:  
   `dotnet run`

## What technologies were used

- .NET 7 + ASP.NET Core
- Entity Framework Core + Identity
- CQRS
- Logging
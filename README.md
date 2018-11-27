### Create Project and Swagger
https://gitlab.com/jiw.surachat/labfix.netcore.stringapi.git

### Install Package
```
$ cd labfix.netcore.api.mssql
$ dotnet add . package Microsoft.AspNetCore.Mvc.TagHelpers
$ dotnet add . package Microsoft.EntityFrameworkCore
$ dotnet add . package NewtonSoft.Json
$ dotnet add . package Microsoft.EntityFrameworkCore.SqlServer
$ dotnet add . package Microsoft.EntityFrameworkCore.Tools
$ dotnet add . package Microsoft.EntityFrameworkCore.SqlServer.Design
$ dotnet add . package Microsoft.EntityFrameworkCore.Tools.DotNet
$ dotnet add . package Microsoft.EntityFrameworkCore.Design

$ dotnet restore
```

### Initial and create Models via EF
The following example scaffolds all schemas and tables and puts the new files in the Models folder.
```
$ dotnet ef dbcontext scaffold "Server=localhost;User ID=web;Password=Zxcv123!;Database=NORTHWND;" Microsoft.EntityFrameworkCore.SqlServer -o Models

$ dotnet build
```

OR

The following example scaffolds only selected tables and creates the context in a separate folder with a specified name:
```
$ dotnet ef dbcontext scaffold "Server=localhost;User ID=web;Password=Zxcv123!;Database=NORTHWND;" Microsoft.EntityFrameworkCore.SqlServer -o Models -t Blog -t Post --context-dir Context -c BlogContext

$ dotnet build
```

### Add connection
1. Add below into file 'appsettings.json'
```
  "ConnectionStrings": {
    "NORTHWND": "Data Source=localhost;Initial Catalog=NORTHWND;User ID=web;Password=Zxcv123!;packet size=4096;Connection Timeout=5000;MultipleActiveResultSets=True"
  },
```
2. edit "Startup.cs" Addline
```
using labfix.netcore.api.mssql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
```
```
public Startup(IConfiguration configuration){
    Configuration = configuration;
}

public IConfiguration Configuration {get;}
```
And
```
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add this.
            var sqlConnectionString = Configuration.GetConnectionString("NORTHWND");
            services.AddDbContext<NORTHWNDContext> (Options => Options.UseSqlServer(sqlConnectionString));
        }
```
"NORTHWNDContext" is class from Models.

3. Comment Models/NORTHWNDContext.cs
```

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=localhost;User ID=web;Password=Zxcv123!;Database=NORTHWND;");
//             }
//         }
```

## Reference

### Entity Framework Core tools reference - .NET CLI
https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

### Getting Started with EF Core on ASP.NET Core with an Existing Database
https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db

### Work with SQL Server LocalDB and ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/sql?view=aspnetcore-2.1

### RESTFUL WEB API USING ASP.NET CORE 2.0 WITH MSSQL(using Dapper)
https://medium.com/@maheshi.gunarathne1994/web-api-using-asp-net-core-2-0-and-entity-framework-core-with-mssql-59d30f33ff64
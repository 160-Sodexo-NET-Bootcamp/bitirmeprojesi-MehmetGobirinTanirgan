<div align="center">
<h1>Sodexo .NET Bootcamp Graduation Project</h1>
</div>

[Project Details](https://github.com/Semra4141/BitirmeProjesiSodexo/files/8022593/Sodexo.Net.Ornek.Bitirme.Projesi.v2.1.pdf)

### Built with

- Back-End:
  - [ASP .NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0) / [Visual Studio 2019](https://visualstudio.microsoft.com/vs/)
  - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
  - [JWT](https://jwt.io/)
  - [Fluent Validation](https://fluentvalidation.net/)
  - [AutoMapper](https://automapper.org/)
  - [Autofac](https://autofac.org/)
  - [Hangfire](https://www.hangfire.io/)
  - [RabbitMQ](https://www.rabbitmq.com/)
  - [MailKit](http://www.mimekit.net/docs/html/Introduction.htm)
  - [SeriLog](https://serilog.net/)
- Database & Cloud:
  - [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - [MySQL](https://www.mysql.com/)
  - [Cloudinary](https://cloudinary.com/)

### Installing / Getting started

1. For running the applications, u have to add/update user secrets of applications or just add into appsettings.json files.

- PCS.Api settings:

```
{
  "AppDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  },
  "JwtSettings": {
    "JwtKey": "zX3IwD2l1KNhS1hecwD3aYwTYXOJb1p3gtCe7RDvD51gu07YhIGqwDxilp1FCZq",
    "Issuer": "http://localhost",
    "Audience": "http://localhost"
  },
  "CloudinarySettings": {
    "CloudName": "dt107fl3n",
    "ApiKey": "458983837838198",
    "ApiSecret": "tllZnI8TQAmlitCm1jEfFMKu9to"
  },
  "RabbitMqSettings": {
    "Hostname": "localhost",
    "Username": "guest",
    "Password": "guest"
  },
  "RedisSettings": {
    "ConStr": "localhost:6379",
    "InstanceName": "MgtPcs"
  }
}
```

- PCS.HangfireApi settings:

```
{
  "AppDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  },
  "HangfireDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  }
}
```


- PCS.EmailWorkerService settings:

```
{
  "AppDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  },
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "DefaultCredentials": false,
    "Port": 465,
    "EnableSsl": true,
    "EmailAddress": "pcs.net.bootcamp.project@gmail.com",
    "Password": "1a2b3c4d."
  }
}
```

- PCS.AccountUnlockWorkerService settings:

```
{
  "AppDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  },
  "HangfireDbSettings": {
    "Provider": "MsSql",
    "MsSqlConStr": "YOUR_CONNECTION_STR",
    "MySqlConStr": "YOUR_CONNECTION_STR"
  },
  "RabbitMqSettings": {
    "Hostname": "localhost",
    "Username": "guest",
    "Password": "guest"
  }
}
```

2. For database creations, u can find backups and script files in the database folder, just use one of them.

